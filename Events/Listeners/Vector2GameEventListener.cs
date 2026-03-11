using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="Vector2GameEvent"/> events; forwards the payload to a <see cref="Vector2UnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Vector2 Event Listener")]
    public sealed class Vector2GameEventListener : BaseGameEventListener<Vector2, Vector2GameEvent, Vector2UnityEvent>
    {
    }
}