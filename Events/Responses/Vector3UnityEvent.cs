using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="UnityEngine.Vector3"/> value.</summary>
    [Serializable]
    public sealed class Vector3UnityEvent : UnityEvent<Vector3>
    {
    }
}