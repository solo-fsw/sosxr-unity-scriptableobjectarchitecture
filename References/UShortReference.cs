using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="ushort"/> values.</summary>
    [Serializable]
    public sealed class UShortReference : BaseReference<ushort, UShortVariable>
    {
        public UShortReference()
        {
        }


        public UShortReference(ushort value) : base(value)
        {
        }
    }
}