using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="double"/> values.</summary>
    [Serializable]
    public sealed class DoubleReference : BaseReference<double, DoubleVariable>
    {
        public DoubleReference()
        {
        }


        public DoubleReference(double value) : base(value)
        {
        }
    }
}