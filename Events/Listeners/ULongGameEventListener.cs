using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="ULongGameEvent"/> events; forwards the payload to a <see cref="ULongUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "ulong Event Listener")]
    public sealed class ULongGameEventListener : BaseGameEventListener<ulong, ULongGameEvent, ULongUnityEvent>
    {
    }
}