﻿using System;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    public sealed class LongUnityEvent : UnityEvent<long>
    {
    }
}