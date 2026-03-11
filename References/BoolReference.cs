using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="bool"/> values.</summary>
    [Serializable]
    public sealed class BoolReference : BaseReference<bool, BoolVariable>
    {
        public BoolReference()
        {
        }


        public BoolReference(bool value) : base(value)
        {
        }
    }
}