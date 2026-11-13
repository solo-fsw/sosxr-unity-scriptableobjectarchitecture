using System;
using System.Buffers;
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
        ///     Each list is called in reverse-registration order from a snapshot, so listeners may add or remove
        ///     listeners while the event is being raised. A listener removed during a raise is not called afterwards;
        ///     a listener added during a raise is called from the next raise on.
        ///     Also records a stack trace entry when editor debug mode is enabled.
        /// </summary>
        public void Raise(T value)
        {
            AddStackTrace(value);

            var version = RemovalVersion;
            var typedListeners = ListSnapshot.Take(_typedListeners, out var count);

            try
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var listener = typedListeners[i];

                    if (IsStillRegistered(_typedListeners, listener, version))
                    {
                        listener.OnEventRaised(value);
                    }
                }
            }
            finally
            {
                ListSnapshot.Release(typedListeners, count);
            }

            InvokeUntypedListeners();

            version = RemovalVersion;
            var typedActions = ListSnapshot.Take(_typedActions, out count);

            try
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var action = typedActions[i];

                    if (IsStillRegistered(_typedActions, action, version))
                    {
                        action(value);
                    }
                }
            }
            finally
            {
                ListSnapshot.Release(typedActions, count);
            }

            InvokeUntypedActions();
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
            if (_typedListeners.Remove(listener))
            {
                MarkRemoved();
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
            if (_typedActions.Remove(action))
            {
                MarkRemoved();
            }
        }


        /// <summary>Removes all registered listeners and actions, typed and untyped.</summary>
        public override void RemoveAll()
        {
            _typedListeners.Clear();
            _typedActions.Clear();
            base.RemoveAll();
        }


        /// <summary>Returns a label that includes the payload type.</summary>
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

        private int _removalVersion;

        /// <summary>
        ///     Incremented every time a listener or action is removed. Used during a raise to skip listeners
        ///     that were removed after the snapshot was taken.
        /// </summary>
        protected int RemovalVersion => _removalVersion;


        /// <summary>
        ///     Notifies all registered listeners and actions in reverse-registration order.
        ///     Iterates a snapshot, so listeners may add or remove listeners while the event is being raised.
        ///     Records a stack trace entry when editor debug mode is enabled.
        /// </summary>
        public virtual void Raise()
        {
            AddStackTrace();

            InvokeUntypedListeners();
            InvokeUntypedActions();
        }


        /// <summary>Calls every untyped <see cref="IGameEventListener" /> in reverse-registration order from a snapshot.</summary>
        protected void InvokeUntypedListeners()
        {
            var version = RemovalVersion;
            var listeners = ListSnapshot.Take(_listeners, out var count);

            try
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var listener = listeners[i];

                    if (IsStillRegistered(_listeners, listener, version))
                    {
                        listener.OnEventRaised();
                    }
                }
            }
            finally
            {
                ListSnapshot.Release(listeners, count);
            }
        }


        /// <summary>Calls every parameterless <see cref="System.Action" /> in reverse-registration order from a snapshot.</summary>
        protected void InvokeUntypedActions()
        {
            var version = RemovalVersion;
            var actions = ListSnapshot.Take(_actions, out var count);

            try
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    var action = actions[i];

                    if (IsStillRegistered(_actions, action, version))
                    {
                        action();
                    }
                }
            }
            finally
            {
                ListSnapshot.Release(actions, count);
            }
        }


        /// <summary>
        ///     Returns true when <paramref name="item" /> may still be called: either nothing was removed since
        ///     <paramref name="version" /> was read, or the item is still in <paramref name="list" />.
        /// </summary>
        protected bool IsStillRegistered<TItem>(List<TItem> list, TItem item, int version)
        {
            return version == _removalVersion || list.Contains(item);
        }


        /// <summary>Call after removing a listener or action, so a running raise skips it.</summary>
        protected void MarkRemoved()
        {
            unchecked
            {
                _removalVersion++;
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
            if (_listeners.Remove(listener))
            {
                MarkRemoved();
            }
        }


        /// <summary>Removes all registered listeners and actions from both lists.</summary>
        public virtual void RemoveAll()
        {
            _listeners.Clear();
            _actions.Clear();
            MarkRemoved();
        }


        /// <summary>
        ///     Stack trace entries recorded each time this event was raised (most recent first).
        ///     Editor-only, only while debug mode is enabled, and capped at <see cref="StackTraceEntry.MaxEntries" />.
        /// </summary>
        public List<StackTraceEntry> StackTraces { get; } = new();


        /// <summary>Records a stack trace entry without a payload. Editor debug mode only.</summary>
        public void AddStackTrace()
        {
            #if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                StackTraceEntry.Record(StackTraces, StackTraceEntry.Create());
            }
            #endif
        }


        /// <summary>Records a stack trace entry that includes <paramref name="value" />. Editor debug mode only.</summary>
        public void AddStackTrace(object value)
        {
            #if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                StackTraceEntry.Record(StackTraces, StackTraceEntry.Create(value));
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
            if (_actions.Remove(action))
            {
                MarkRemoved();
            }
        }
    }


    /// <summary>
    ///     Copies a list into a pooled array so it can be iterated while the list changes.
    ///     Each call rents its own array, so nested (re-entrant) raises are safe.
    /// </summary>
    internal static class ListSnapshot
    {
        public static TItem[] Take<TItem>(List<TItem> source, out int count)
        {
            count = source.Count;

            if (count == 0)
            {
                return Array.Empty<TItem>();
            }

            var buffer = ArrayPool<TItem>.Shared.Rent(count);
            source.CopyTo(buffer);

            return buffer;
        }


        public static void Release<TItem>(TItem[] buffer, int count)
        {
            if (count == 0)
            {
                return;
            }

            Array.Clear(buffer, 0, count);
            ArrayPool<TItem>.Shared.Return(buffer);
        }
    }
}
