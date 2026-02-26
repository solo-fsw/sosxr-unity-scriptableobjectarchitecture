using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.Color32"/> values.</summary>
    [Serializable]
    public sealed class Color32Reference : BaseReference<Color32, Color32Variable>
    {
        public Color32Reference()
        {
        }


        public Color32Reference(Color32 value) : base(value)
        {
        }
    }
}