using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="LongGameEvent"/> events; forwards the payload to a <see cref="LongUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "long Event Listener")]
    public sealed class LongGameEventListener : BaseGameEventListener<long, LongGameEvent, LongUnityEvent>
    {
    }
}