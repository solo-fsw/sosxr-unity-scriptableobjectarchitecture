using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="ulong"/> values.</summary>
    [Serializable]
    public sealed class ULongReference : BaseReference<ulong, ULongVariable>
    {
        public ULongReference()
        {
        }


        public ULongReference(ulong value) : base(value)
        {
        }
    }
}