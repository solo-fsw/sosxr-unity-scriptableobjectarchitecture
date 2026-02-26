using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying a <see cref="UnityEngine.LayerMask" /> value.</summary>
    public class LayerMaskEvent : UnityEvent<LayerMask>
    {
    }


    /// <summary>ScriptableObject variable holding a <see cref="UnityEngine.LayerMask" /> value.</summary>

    [CreateAssetMenu(
        fileName = "LayerMaskVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "LayerMask",
        order = 120)]
    public class LayerMaskVariable : BaseVariable<LayerMask, LayerMaskEvent>
    {
    }
}