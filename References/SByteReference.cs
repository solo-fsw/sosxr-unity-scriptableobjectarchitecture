using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="sbyte"/> values.</summary>
    [Serializable]
    public sealed class SByteReference : BaseReference<sbyte, SByteVariable>
    {
        public SByteReference()
        {
        }


        public SByteReference(sbyte value) : base(value)
        {
        }
    }
}