using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Vector3"/> values.</summary>
    [Serializable]
    public sealed class Vector3Reference : BaseReference<Vector3, Vector3Variable>
    {
        public Vector3Reference()
        {
        }


        public Vector3Reference(Vector3 value) : base(value)
        {
        }
    }
}