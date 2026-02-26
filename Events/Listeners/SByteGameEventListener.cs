using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="SByteGameEvent"/> events; forwards the payload to a <see cref="SByteUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "sbyte Event Listener")]
    public sealed class SByteGameEventListener : BaseGameEventListener<sbyte, SByteGameEvent, SByteUnityEvent>
    {
    }
}