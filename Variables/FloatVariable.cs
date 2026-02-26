using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="float" /> value.</summary>
    public class FloatEvent : UnityEvent<float>
    {
    }


    /// <summary>
    ///     ScriptableObject variable holding a <see cref="float" /> value.
    ///     Supports value clamping and uses <see cref="UnityEngine.Mathf.Epsilon" /> for equality comparison.
    /// </summary>

    [CreateAssetMenu(
        fileName = "FloatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "float",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 3)]
    public class FloatVariable : BaseVariable<float, FloatEvent>
    {
        public override bool Clampable => true;


        protected override float ClampValue(float value)
        {
            if (value.CompareTo(MinClampValue) < 0)
            {
                return MinClampValue;
            }

            if (value.CompareTo(MaxClampValue) > 0)
            {
                return MaxClampValue;
            }

            return value;
        }


        protected override bool AreValuesEqual(float a, float b)
        {
            return Mathf.Abs(a - b) < Mathf.Epsilon;
        }
    }
}