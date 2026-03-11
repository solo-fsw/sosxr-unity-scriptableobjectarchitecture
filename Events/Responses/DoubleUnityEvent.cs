using System;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="double"/> value.</summary>
    [Serializable]
    public sealed class DoubleUnityEvent : UnityEvent<double>
    {
    }
}