using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Vector4"/> values.</summary>
    [Serializable]
    public sealed class Vector4Reference : BaseReference<Vector4, Vector4Variable>
    {
        public Vector4Reference()
        {
        }


        public Vector4Reference(Vector4 value) : base(value)
        {
        }
    }
}