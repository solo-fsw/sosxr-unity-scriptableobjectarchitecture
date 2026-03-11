using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.Color32" /> value.</summary>
    public class Color32Event : UnityEvent<Color32>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.Color32" /> value.</summary>

    [CreateAssetMenu(
        fileName = "Color32Variable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Color32",
        order = 120)]
    public class Color32Variable : BaseVariable<Color32, Color32Event>
    {
    }
}