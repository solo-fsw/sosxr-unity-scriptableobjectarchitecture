using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="UIntGameEvent"/> events; forwards the payload to a <see cref="UIntUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "uint Event Listener")]
    public sealed class UIntGameEventListener : BaseGameEventListener<uint, UIntGameEvent, UIntUnityEvent>
    {
    }
}