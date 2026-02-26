using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying an <see cref="sbyte" /> value.</summary>
    public class SByteEvent : UnityEvent<sbyte>
    {
    }


    /// <summary>ScriptableObject variable holding an <see cref="sbyte" /> value. Supports value clamping.</summary>

    [CreateAssetMenu(
        fileName = "SByteVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "sbyte",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    public class SByteVariable : BaseVariable<sbyte, SByteEvent>
    {
        public override bool Clampable => true;


        protected override sbyte ClampValue(sbyte value)
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
    }
}