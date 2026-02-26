using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="GameObjectGameEvent"/> events; forwards the payload to a <see cref="GameObjectUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "GameObject Event Listener")]
    public sealed class GameObjectGameEventListener : BaseGameEventListener<GameObject, GameObjectGameEvent, GameObjectUnityEvent>
    {
    }
}