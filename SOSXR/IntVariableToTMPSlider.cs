using UnityEngine;
using UnityEngine.UI;


namespace ScriptableObjectArchitecture.SOSXR
{
    [RequireComponent(typeof(Slider))]
    public class IntVariableToTMPSlider : MonoBehaviour
    {
        [SerializeField] private IntVariable m_variable;
        [SerializeField] private bool m_setSliderOnStart = true;
        private Slider _slider;


        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }


        private void Start()
        {
            if (m_setSliderOnStart)
            {
                SetSliderValue();
            }
        }


        [ContextMenu(nameof(SetSliderValue))]
        private void SetSliderValue()
        {
            _slider.value = m_variable.Value;
        }
    }
}
