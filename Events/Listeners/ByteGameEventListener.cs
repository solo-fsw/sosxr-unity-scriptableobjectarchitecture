using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="ByteGameEvent"/> events; forwards the payload to a <see cref="ByteUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "byte Event Listener")]
    public sealed class ByteGameEventListener : BaseGameEventListener<byte, ByteGameEvent, ByteUnityEvent>
    {
    }
}