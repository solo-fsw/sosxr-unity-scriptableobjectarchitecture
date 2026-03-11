using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="char"/> values.</summary>
    [Serializable]
    public sealed class CharReference : BaseReference<char, CharVariable>
    {
        public CharReference()
        {
        }


        public CharReference(char value) : base(value)
        {
        }
    }
}