using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="long"/> values.</summary>
    [Serializable]
    public sealed class LongReference : BaseReference<long, LongVariable>
    {
        public LongReference()
        {
        }


        public LongReference(long value) : base(value)
        {
        }
    }
}