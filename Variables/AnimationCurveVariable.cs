using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying an <see cref="UnityEngine.AnimationCurve" /> value.</summary>
    public class AnimationCurveEvent : UnityEvent<AnimationCurve>
    {
    }


    /// <summary>ScriptableObject variable holding an <see cref="UnityEngine.AnimationCurve" /> value.</summary>

    [CreateAssetMenu(
        fileName = "AnimationCurveVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "AnimationCurve",
        order = 120)]
    public class AnimationCurveVariable : BaseVariable<AnimationCurve, AnimationCurveEvent>
    {
    }
}