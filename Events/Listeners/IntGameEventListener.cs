using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="IntGameEvent"/> events; forwards the payload to a <see cref="IntUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "int Event Listener")]
    public sealed class IntGameEventListener : BaseGameEventListener<int, IntGameEvent, IntUnityEvent>
    {
    }
}