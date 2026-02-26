using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.Object" /> reference.</summary>
    public class ObjectEvent : UnityEvent<Object>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.Object" /> reference (any Unity object).</summary>

    [CreateAssetMenu(
        fileName = "ObjectVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Object",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 1)]
    public class ObjectVariable : BaseVariable<Object, ObjectEvent>
    {
    }
}