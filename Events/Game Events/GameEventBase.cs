using System;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Generic ScriptableObject game event that carries a typed payload of type <typeparamref name="T" />.
    ///     Maintains separate listener lists for typed <see cref="IGameEventListener{T}" />, untyped <see cref="IGameEventListener" />,
    ///     typed <see cref="System.Action{T}" />, and parameterless <see cref="System.Action" /> delegates,
    ///     so consumers can choose the registration style that suits their needs.
    /// </summary>
    /// <typeparam name="T">The payload type passed to listeners when the event is raised.</typeparam>

    public abstract class GameEventBase<T> : GameEventBase, IGameEvent<T>, IStackTraceObject
    {
        [SerializeField] protected T _debugValue;
        private readonly List<IGameEventListener<T>> _typedListeners = new();
        private readonly List<Action<T>> _typedActions = new();


        /// <summary>
        ///     Broadcasts the event to all registered listeners and actions, passing <paramref name="value" /> to typed receivers.
        ///     Also records a stack trace entry when editor debug mode is enabled.
        /// </summary>
        public void Raise(T value)
        {
            AddStackTrace(value);

            for (var i = _typedListeners.Count - 1; i >= 0; i--)
            {
                _typedListeners[i].OnEventRaised(value);
            }

            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }

            for (var i = _typedActions.Count - 1; i >= 0; i--)
            {
                _typedActions[i](value);
            }

            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i]();
            }
        }


        /// <summary>Registers a typed <see cref="IGameEventListener{T}" />. No-op if already registered.</summary>
        public void AddListener(IGameEventListener<T> listener)
        {
            if (!_typedListeners.Contains(listener))
            {
                _typedListeners.Add(listener);
            }
        }


        /// <summary>Removes a typed <see cref="IGameEventListener{T}" /> from the listener list.</summary>
        public void RemoveListener(IGameEventListener<T> listener)
        {
            if (_typedListeners.Contains(listener))
            {
                _typedListeners.Remove(listener);
            }
        }


        /// <summary>Registers a typed <see cref="System.Action{T}" /> delegate. No-op if already registered.</summary>
        public void AddListener(Action<T> action)
        {
            if (!_typedActions.Contains(action))
            {
                _typedActions.Add(action);
            }
        }


        /// <summary>Removes a typed <see cref="System.Action{T}" /> delegate from the listener list.</summary>
        public void RemoveListener(Action<T> action)
        {
            if (_typedActions.Contains(action))
            {
                _typedActions.Remove(action);
            }
        }


        public override string ToString()
        {
            return "GameEventBase<" + typeof(T) + ">";
        }
    }


    /// <summary>
    ///     Non-generic base for all ScriptableObject game events.
    ///     Manages untyped <see cref="IGameEventListener" /> and <see cref="System.Action" /> registrations,
    ///     raises all listeners when <see cref="Raise" /> is called, and maintains a debug stack trace list
    ///     populated whenever debug mode is active in the editor.
    /// </summary>

    public abstract class GameEventBase : SOArchitectureBaseObject, IGameEvent, IStackTraceObject
    {
        protected readonly List<IGameEventListener> _listeners = new();
        protected readonly List<Action> _actions = new();


        /// <summary>
        ///     Notifies all registered listeners and actions in reverse-registration order.
        ///     Records a stack trace entry when editor debug mode is enabled.
        /// </summary>
        public virtual void Raise()
        {
            AddStackTrace();

            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }

            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i]();
            }
        }


        /// <summary>Registers an untyped <see cref="IGameEventListener" />. No-op if already registered.</summary>
        public void AddListener(IGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }


        /// <summary>Unregisters an untyped <see cref="IGameEventListener" />.</summary>
        public void RemoveListener(IGameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }


        /// <summary>Removes all registered listeners and actions from both lists.</summary>
        public virtual void RemoveAll()
        {
            _listeners.RemoveRange(0, _listeners.Count);
            _actions.RemoveRange(0, _actions.Count);
        }


        /// <summary>Ordered list of stack trace entries recorded each time this event was raised (most recent first). Editor-only when debug mode is enabled.</summary>
        public List<StackTraceEntry> StackTraces { get; } = new();


        public void AddStackTrace()
        {
            #if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                StackTraces.Insert(0, StackTraceEntry.Create());
            }
            #endif
        }


        public void AddStackTrace(object value)
        {
            #if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                StackTraces.Insert(0, StackTraceEntry.Create(value));
            }
            #endif
        }


        /// <summary>Registers a parameterless <see cref="System.Action" /> delegate. No-op if already registered.</summary>
        public void AddListener(Action action)
        {
            if (!_actions.Contains(action))
            {
                _actions.Add(action);
            }
        }


        /// <summary>Unregisters a parameterless <see cref="System.Action" /> delegate.</summary>
        public void RemoveListener(Action action)
        {
            if (_actions.Contains(action))
            {
                _actions.Remove(action);
            }
        }
    }
}