using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.Vector4" /> value.</summary>
    public class Vector4Event : UnityEvent<Vector4>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.Vector4" /> value.</summary>

    [CreateAssetMenu(
        fileName = "Vector4Variable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Vector4",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 12)]
    public sealed class Vector4Variable : BaseVariable<Vector4, Vector4Event>
    {
    }
}