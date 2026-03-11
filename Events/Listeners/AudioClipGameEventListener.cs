using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="AudioClipGameEvent"/> events; forwards the payload to a <see cref="AudioClipUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "AudioClip Event Listener")]
    public sealed class AudioClipGameEventListener : BaseGameEventListener<AudioClip, AudioClipGameEvent, AudioClipUnityEvent>
    {
    }
}