using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Quaternion"/> values.</summary>
    [Serializable]
    public sealed class QuaternionReference : BaseReference<Quaternion, QuaternionVariable>
    {
        public QuaternionReference()
        {
        }


        public QuaternionReference(Quaternion value) : base(value)
        {
        }
    }
}