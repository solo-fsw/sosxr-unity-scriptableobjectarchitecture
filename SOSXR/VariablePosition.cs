using UnityEngine;


namespace ScriptableObjectArchitecture.SOSXR
{
    public class VariablePosition : MonoBehaviour
    {
        [SerializeField] private Vector3Variable m_variable;
        [SerializeField] private bool m_useLocalSpace = true;
        [SerializeField] private bool m_isAdditive = false;
        private Transform _transform;


        private void Awake()
        {
            _transform = gameObject.transform;
        }


        private void OnEnable()
        {
            m_variable.AddListener(SetTransform);
        }


        private void Start()
        {
            SetTransformPositionToValue();
        }


        [ContextMenu(nameof(SetTransformPositionToValue))]
        private void SetTransformPositionToValue()
        {
            SetTransform(m_variable.Value);
        }


        private void SetTransform(Vector3 newPosition)
        {
            if (m_useLocalSpace)
            {
                if (m_isAdditive)
                {
                    _transform.localPosition += newPosition;
                }
                else
                {
                    _transform.localPosition = newPosition;
                }
            }
            else
            {
                if (m_isAdditive)
                {
                    _transform.position += newPosition;
                }
                else
                {
                    _transform.position = newPosition;
                }
            }

            //this.Verbose($"Set new position to {_transform.position} in World Space, and {_transform.localPosition} in Local Space");
        }


        private void OnDisable()
        {
            m_variable.RemoveListener(SetTransform);
        }
    }
}
