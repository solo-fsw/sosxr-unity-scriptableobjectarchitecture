using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.GameObject"/> values.</summary>
    [Serializable]
    public sealed class GameObjectReference : BaseReference<GameObject, GameObjectVariable>
    {
        public GameObjectReference()
        {
        }


        public GameObjectReference(GameObject value) : base(value)
        {
        }
    }
}