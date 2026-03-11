using System;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="sbyte"/> value.</summary>
    [Serializable]
    public sealed class SByteUnityEvent : UnityEvent<sbyte>
    {
    }
}