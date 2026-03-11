using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="char" /> value.</summary>
    public class CharEvent : UnityEvent<char>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="char" /> value.</summary>

    [CreateAssetMenu(
        fileName = "CharVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "char",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 7)]
    public sealed class CharVariable : BaseVariable<char, CharEvent>
    {
    }
}