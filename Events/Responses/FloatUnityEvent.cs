using System;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="float"/> value.</summary>
    [Serializable]
    public sealed class FloatUnityEvent : UnityEvent<float>
    {
    }
}