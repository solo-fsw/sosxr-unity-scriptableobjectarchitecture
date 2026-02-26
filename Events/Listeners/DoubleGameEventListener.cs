using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="DoubleGameEvent"/> events; forwards the payload to a <see cref="DoubleUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "double Event Listener")]
    public sealed class DoubleGameEventListener : BaseGameEventListener<double, DoubleGameEvent, DoubleUnityEvent>
    {
    }
}