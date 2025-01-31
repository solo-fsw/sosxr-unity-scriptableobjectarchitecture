﻿using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        fileName = "UnsignedIntGameEvent.asset",
        menuName = SOArchitecture_Utility.ADVANCED_GAME_EVENT + "uint",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 16)]
    public sealed class UIntGameEvent : GameEventBase<uint>
    {
    }
}