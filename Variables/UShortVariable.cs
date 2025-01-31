﻿using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class UShortEvent : UnityEvent<ushort>
    {
    }


    [CreateAssetMenu(
        fileName = "UnsignedShortVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "ushort",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 18)]
    public class UShortVariable : BaseVariable<ushort, UShortEvent>
    {
        public override bool Clampable => true;


        protected override ushort ClampValue(ushort value)
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