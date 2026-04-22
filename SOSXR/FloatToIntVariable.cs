using UnityEngine;


namespace ScriptableObjectArchitecture.SOSXR
{
    public class FloatToIntVariable : MonoBehaviour
    {
        [SerializeField] private IntVariable m_variable;


        [ContextMenu(nameof(FloatToInt))]
        public void FloatToInt(float input)
        {
            m_variable.Value = Mathf.RoundToInt(input);
        }
    }
}
