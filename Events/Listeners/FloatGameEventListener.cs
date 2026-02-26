using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="FloatGameEvent"/> events; forwards the payload to a <see cref="FloatUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "float Event Listener")]
    public sealed class FloatGameEventListener : BaseGameEventListener<float, FloatGameEvent, FloatUnityEvent>
    {
    }
}