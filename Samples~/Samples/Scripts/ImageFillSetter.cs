using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjectArchitecture.Examples
{
    public class ImageFillSetter : MonoBehaviour
    {
        [SerializeField] private FloatReference _variable;
        [SerializeField] private FloatReference _maxValue;
        [SerializeField] private Image _imageTarget;


        private void Update()
        {
            _imageTarget.fillAmount = Mathf.Clamp01(_variable.Value / _maxValue.Value);
        }
    }
}