using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.AnimationCurve"/> values.</summary>
    [Serializable]
    public sealed class AnimationCurveReference : BaseReference<AnimationCurve, AnimationCurveVariable>
    {
        public AnimationCurveReference()
        {
        }


        public AnimationCurveReference(AnimationCurve value) : base(value)
        {
        }
    }
}