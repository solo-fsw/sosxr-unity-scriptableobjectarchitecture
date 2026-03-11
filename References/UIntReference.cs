using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="uint"/> values.</summary>
    [Serializable]
    public sealed class UIntReference : BaseReference<uint, UIntVariable>
    {
        public UIntReference()
        {
        }


        public UIntReference(uint value) : base(value)
        {
        }
    }
}