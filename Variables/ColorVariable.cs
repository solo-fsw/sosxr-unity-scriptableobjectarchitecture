using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.Color" /> value.</summary>
    public class ColorEvent : UnityEvent<Color>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.Color" /> value.</summary>

    [CreateAssetMenu(
        fileName = "ColorVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Color",
        order = 120)]
    public class ColorVariable : BaseVariable<Color, ColorEvent>
    {
    }
}