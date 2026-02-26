using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="CharGameEvent"/> events; forwards the payload to a <see cref="CharUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "char Event Listener")]
    public sealed class CharGameEventListener : BaseGameEventListener<char, CharGameEvent, CharUnityEvent>
    {
    }
}