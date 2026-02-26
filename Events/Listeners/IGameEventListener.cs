namespace ScriptableObjectArchitecture
{
    /// <summary>Implemented by typed listeners that receive a value of type <typeparamref name="T" /> when an event is raised.</summary>
    /// <typeparam name="T">The payload type.</typeparam>

    public interface IGameEventListener<T>
    {
        /// <summary>Called by the registered <see cref="GameEventBase{T}" /> when it is raised with <paramref name="value" />.</summary>
        void OnEventRaised(T value);
    }


    /// <summary>Implemented by untyped (parameterless) listeners.</summary>
    public interface IGameEventListener
    {
        /// <summary>Called by the registered <see cref="GameEventBase" /> when it is raised.</summary>
        void OnEventRaised();
    }
}