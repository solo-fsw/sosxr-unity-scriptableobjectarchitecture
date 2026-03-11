using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="string" /> value.</summary>
    public class StringEvent : UnityEvent<string>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="string" /> value.</summary>

    [CreateAssetMenu(
        fileName = "StringVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "string",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 2)]
    public sealed class StringVariable : BaseVariable<string, StringEvent>
    {
    }
}