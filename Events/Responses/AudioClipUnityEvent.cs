using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}"/> carrying a <see cref="UnityEngine.AudioClip"/> value.</summary>
    [Serializable]
    public sealed class AudioClipUnityEvent : UnityEvent<AudioClip>
    {
    }
}