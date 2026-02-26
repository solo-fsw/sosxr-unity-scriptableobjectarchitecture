using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Color"/> values.</summary>
    [Serializable]
    public sealed class ColorReference : BaseReference<Color, ColorVariable>
    {
        public ColorReference()
        {
        }


        public ColorReference(Color value) : base(value)
        {
        }
    }
}