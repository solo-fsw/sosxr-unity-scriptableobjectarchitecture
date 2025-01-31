using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        fileName = "AudioClipGameEvent.asset",
        menuName = SOArchitecture_Utility.ADVANCED_GAME_EVENT + "AudioClip",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 5)]
    public sealed class AudioClipGameEvent : GameEventBase<AudioClip>
    {
    }
}