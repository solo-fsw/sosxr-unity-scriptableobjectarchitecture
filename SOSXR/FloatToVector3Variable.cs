using UnityEngine;


namespace ScriptableObjectArchitecture.SOSXR
{
    public class FloatToVector3Variable : MonoBehaviour
    {
        [SerializeField] private Vector3Variable m_vector3Variable;
        [SerializeField] private Axis m_axis = Axis.Z;


        [ContextMenu(nameof(FloatToVector3))]
        public void FloatToVector3(float input)
        {
            if (m_axis == Axis.X)
            {
                m_vector3Variable.Value = new Vector3(input, m_vector3Variable.Value.y, m_vector3Variable.Value.z);
            }
            else if (m_axis == Axis.Y)
            {
                m_vector3Variable.Value = new Vector3(m_vector3Variable.Value.x, input, m_vector3Variable.Value.z);
            }
            else if (m_axis == Axis.Z)
            {
                m_vector3Variable.Value = new Vector3(m_vector3Variable.Value.x, m_vector3Variable.Value.y, input);
            }
        }
    }
}
