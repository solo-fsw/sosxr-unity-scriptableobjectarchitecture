using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="UnityEngine.Quaternion"/> value.</summary>
    [Serializable]
    public sealed class QuaternionUnityEvent : UnityEvent<Quaternion>
    {
    }
}