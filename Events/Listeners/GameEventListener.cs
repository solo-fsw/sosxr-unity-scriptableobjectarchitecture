using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Concrete untyped listener for <see cref="GameEventBase" /> ScriptableObject events.
    ///     Add as a component, assign a <see cref="GameEvent" /> (or any <see cref="GameEventBase" />), and wire the
    ///     Response Unity Event to react to the event being raised.
    /// </summary>

    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Game Event Listener")]
    [ExecuteInEditMode]
    public sealed class GameEventListener : BaseGameEventListener<GameEventBase, UnityEvent>
    {
    }
}