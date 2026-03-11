using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="UnityEngine.AudioClip"/> values.</summary>
    [Serializable]
    public sealed class AudioClipReference : BaseReference<AudioClip, AudioClipVariable>
    {
        public AudioClipReference()
        {
        }


        public AudioClipReference(AudioClip value) : base(value)
        {
        }
    }
}