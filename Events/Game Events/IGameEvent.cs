namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Typed contract for ScriptableObject game events that carry a value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The payload type passed to listeners.</typeparam>

    public interface IGameEvent<T>
    {
        /// <summary>Broadcasts the event to all registered listeners with the provided <paramref name="value" />.</summary>
        void Raise(T value);


        /// <summary>Registers a typed <see cref="IGameEventListener{T}" />.</summary>
        void AddListener(IGameEventListener<T> listener);


        /// <summary>Unregisters a typed <see cref="IGameEventListener{T}" />.</summary>
        void RemoveListener(IGameEventListener<T> listener);


        /// <summary>Removes all registered listeners and actions.</summary>
        void RemoveAll();
    }


    /// <summary>Contract for untyped (parameterless) ScriptableObject game events.</summary>
    public interface IGameEvent
    {
        /// <summary>Broadcasts the event to all registered listeners.</summary>
        void Raise();


        /// <summary>Registers an untyped <see cref="IGameEventListener" />.</summary>
        void AddListener(IGameEventListener listener);


        /// <summary>Unregisters an untyped <see cref="IGameEventListener" />.</summary>
        void RemoveListener(IGameEventListener listener);


        /// <summary>Removes all registered listeners and actions.</summary>
        void RemoveAll();
    }
}