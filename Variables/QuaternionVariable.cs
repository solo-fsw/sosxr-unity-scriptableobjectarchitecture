using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.Quaternion" /> value.</summary>
    public class QuaternionEvent : UnityEvent<Quaternion>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.Quaternion" /> value.</summary>

    [CreateAssetMenu(
        fileName = "QuaternionVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Quaternion",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 13)]
    public sealed class QuaternionVariable : BaseVariable<Quaternion, QuaternionEvent>
    {
    }
}