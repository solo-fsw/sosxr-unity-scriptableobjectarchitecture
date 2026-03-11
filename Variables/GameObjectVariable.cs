using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.GameObject" /> reference.</summary>
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.GameObject" /> reference.</summary>

    [CreateAssetMenu(
        fileName = "GameObjectVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GameObject",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public sealed class GameObjectVariable : BaseVariable<GameObject, GameObjectEvent>
    {
    }
}