using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.LayerMask"/> values.</summary>
    [Serializable]
    public sealed class LayerMaskReference : BaseReference<LayerMask, LayerMaskVariable>
    {
        public LayerMaskReference()
        {
        }


        public LayerMaskReference(LayerMask value) : base(value)
        {
        }
    }
}