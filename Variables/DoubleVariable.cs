﻿using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class DoubleEvent : UnityEvent<double>
    {
    }


    [CreateAssetMenu(
        fileName = "DoubleVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "double",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    public class DoubleVariable : BaseVariable<double, DoubleEvent>
    {
        public override bool Clampable => true;


        protected override double ClampValue(double value)
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