using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Concrete parameterless <see cref="GameEventBase" /> ScriptableObject asset.
    ///     Create via <c>Assets → Create → SOSXR/SO Architecture/Game Events/Game Event</c>.
    ///     Raise it from code (<c>myEvent.Raise()</c>) or via the inspector Raise button.
    /// </summary>

    [CreateAssetMenu(
        fileName = "GameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "Game Event",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS - 1)]
    public sealed class GameEvent : GameEventBase
    {
    }
}