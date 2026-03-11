using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>Serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> carrying an <see cref="UnityEngine.AudioClip" /> value.</summary>
    public class AudioClipEvent : UnityEvent<AudioClip>
    {
    }


    /// <summary>ScriptableObject variable holding an <see cref="UnityEngine.AudioClip" /> reference.</summary>

    [CreateAssetMenu(
        fileName = "AudioClipVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "AudioClip",
        order = 120)]
    public class AudioClipVariable : BaseVariable<AudioClip, AudioClipEvent>
    {
    }
}