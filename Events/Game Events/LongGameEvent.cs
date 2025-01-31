﻿using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        fileName = "LongGameEvent.asset",
        menuName = SOArchitecture_Utility.ADVANCED_GAME_EVENT + "long",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 9)]
    public sealed class LongGameEvent : GameEventBase<long>
    {
    }
}