using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Vector2"/> values.</summary>
    [Serializable]
    public sealed class Vector2Reference : BaseReference<Vector2, Vector2Variable>
    {
        public Vector2Reference()
        {
        }


        public Vector2Reference(Vector2 value) : base(value)
        {
        }
    }
}