using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="Object"/> value.</summary>
    [Serializable]
    public class ObjectUnityEvent : UnityEvent<Object>
    {
    }
}