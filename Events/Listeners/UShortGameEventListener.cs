using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="UShortGameEvent"/> events; forwards the payload to a <see cref="UShortUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "ushort Event Listener")]
    public sealed class UShortGameEventListener : BaseGameEventListener<ushort, UShortGameEvent, UShortUnityEvent>
    {
    }
}