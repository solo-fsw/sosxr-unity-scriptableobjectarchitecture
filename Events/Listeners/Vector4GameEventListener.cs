using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="Vector4GameEvent"/> events; forwards the payload to a <see cref="Vector4UnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Vector4 Event Listener")]
    public sealed class Vector4GameEventListener : BaseGameEventListener<Vector4, Vector4GameEvent, Vector4UnityEvent>
    {
    }
}