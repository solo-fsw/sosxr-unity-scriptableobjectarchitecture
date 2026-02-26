using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="BoolGameEvent"/> events; forwards the payload to a <see cref="BoolUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "bool Event Listener")]
    public sealed class BoolGameEventListener : BaseGameEventListener<bool, BoolGameEvent, BoolUnityEvent>
    {
    }
}