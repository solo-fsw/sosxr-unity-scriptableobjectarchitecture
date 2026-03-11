using System;
using Object = UnityEngine.Object;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="Object"/> values.</summary>
    [Serializable]
    public class ObjectReference : BaseReference<Object, ObjectVariable>
    {
        public ObjectReference()
        {
        }


        public ObjectReference(Object value) : base(value)
        {
        }
    }
}