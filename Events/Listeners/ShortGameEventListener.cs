using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="ShortGameEvent"/> events; forwards the payload to a <see cref="ShortUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "short Event Listener")]
    public sealed class ShortGameEventListener : BaseGameEventListener<short, ShortGameEvent, ShortUnityEvent>
    {
    }
}