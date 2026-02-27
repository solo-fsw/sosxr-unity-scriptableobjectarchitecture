using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Root MonoBehaviour base class for all SO Architecture MonoBehaviours
    ///     (e.g. <see cref="BaseGameEventListener{TEvent, TResponse}" />, lifecycle helpers).
    ///     Acts as a common marker type for editor tooling.
    /// </summary>
    public abstract class SOArchitectureBaseMonobehaviour : MonoBehaviour { }
}

