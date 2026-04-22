using UnityEngine;
using UnityEngine.UI;


namespace ScriptableObjectArchitecture.SOSXR
{
    [RequireComponent(typeof(Slider))]
    public class Vector3VariableToTMPSlider : MonoBehaviour
    {
        [SerializeField] private Vector3Variable m_variable;
        [SerializeField] private bool m_setSliderOnStart = true;

        [SerializeField] private Axis m_axis = Axis.Z;
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
            if (m_axis == Axis.X)
            {
                _slider.value = m_variable.Value.x;
            }
            else if (m_axis == Axis.Y)
            {
                _slider.value = m_variable.Value.y;
            }
            else if (m_axis == Axis.Z)
            {
                _slider.value = m_variable.Value.z;
            }
        }
    }
}
