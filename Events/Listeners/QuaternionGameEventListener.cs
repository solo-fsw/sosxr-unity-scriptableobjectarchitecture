using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>MonoBehaviour listener for <see cref="QuaternionGameEvent"/> events; forwards the payload to a <see cref="QuaternionUnityEvent"/>.</summary>
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Quaternion Event Listener")]
    public sealed class QuaternionGameEventListener : BaseGameEventListener<Quaternion, QuaternionGameEvent, QuaternionUnityEvent>
    {
    }
}