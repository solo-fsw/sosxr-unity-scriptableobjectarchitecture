using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="int"/> values.</summary>
    [Serializable]
    public sealed class IntReference : BaseReference<int, IntVariable>
    {
        public IntReference()
        {
        }


        public IntReference(int value) : base(value)
        {
        }
    }
}