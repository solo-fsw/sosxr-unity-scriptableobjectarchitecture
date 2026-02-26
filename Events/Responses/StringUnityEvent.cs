using System;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="string"/> value.</summary>
    [Serializable]
    public sealed class StringUnityEvent : UnityEvent<string>
    {
    }
}