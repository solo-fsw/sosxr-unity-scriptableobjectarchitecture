using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="byte"/> values.</summary>
    [Serializable]
    public sealed class ByteReference : BaseReference<byte, ByteVariable>
    {
        public ByteReference()
        {
        }


        public ByteReference(byte value) : base(value)
        {
        }
    }
}