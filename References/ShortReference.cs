using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="short"/> values.</summary>
    [Serializable]
    public sealed class ShortReference : BaseReference<short, ShortVariable>
    {
        public ShortReference()
        {
        }


        public ShortReference(short value) : base(value)
        {
        }
    }
}