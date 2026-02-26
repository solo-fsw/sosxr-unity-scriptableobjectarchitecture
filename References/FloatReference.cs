using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="float"/> values.</summary>
    [Serializable]
    public sealed class FloatReference : BaseReference<float, FloatVariable>
    {
        public FloatReference()
        {
        }


        public FloatReference(float value) : base(value)
        {
        }
    }
}