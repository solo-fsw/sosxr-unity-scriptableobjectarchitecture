using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="string"/> values.</summary>
    [Serializable]
    public sealed class StringReference : BaseReference<string, StringVariable>
    {
        public StringReference()
        {
        }


        public StringReference(string value) : base(value)
        {
        }
    }
}