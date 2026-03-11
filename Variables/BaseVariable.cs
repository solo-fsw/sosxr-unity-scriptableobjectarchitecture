using System;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Non-generic base for all typed <see cref="BaseVariable{T}" /> ScriptableObject variables.
    ///     Provides a type-erased interface for inspector tooling and editor utilities.
    /// </summary>

    public abstract class BaseVariable : GameEventBase
    {
        /// <summary>Whether this variable's value is currently clamped to its min/max range.</summary>
        public abstract bool IsClamped { get; }
        /// <summary>Whether this variable type supports value clamping.</summary>
        public abstract bool Clampable { get; }
        /// <summary>Whether writes to this variable's value are blocked at runtime.</summary>
        public abstract bool ReadOnly { get; }
        /// <summary>The concrete <see cref="System.Type" /> of the value this variable stores.</summary>
        public abstract Type Type { get; }
        /// <summary>The <see cref="System.Type" /> of the <see cref="BaseReference{TBase,TVariable}" /> paired with this variable.</summary>
        public abstract Type ReferenceType { get; }
        /// <summary>Gets or sets the variable's value as a boxed <see cref="object" />.</summary>
        public abstract object BaseValue { get; set; }
        /// <summary>Whether the variable should reset to its default value when the asset is enabled (scene load / play-mode entry).</summary>
        public abstract bool UseDefaultValue { get; }
    }


    /// <summary>
    ///     Generic ScriptableObject variable that holds a single value of type <typeparamref name="T" />.
    ///     Extends <see cref="GameEventBase" /> so that setting the value automatically raises a change event.
    ///     Supports optional read-only protection, value clamping, and default-value reset on enable.
    /// </summary>
    /// <typeparam name="T">The type of value stored by this variable.</typeparam>

    public abstract class BaseVariable<T> : BaseVariable
    {
        [SerializeField] protected T _value;
        [SerializeField] private bool _readOnly;
        [SerializeField] private bool _useDefaultValue;
        [SerializeField] private bool _raiseWarning = true;
        [SerializeField] protected bool _isClamped;
        [SerializeField] protected T _minClampedValue;
        [SerializeField] protected T _maxClampedValue;
        [SerializeField] protected T _defaultValue;

        private T _oldValue;

        /// <summary>
        ///     Gets or sets the current value. Setters are routed through <see cref="SetValue(T)" />,
        ///     which enforces read-only protection, clamping, and change notification.
        /// </summary>
        public virtual T Value
        {
            get => _value;
            set => _value = SetValue(value);
        }

        /// <summary>The value the variable resets to when <see cref="UseDefaultValue" /> is <c>true</c> and the asset is enabled.</summary>
        public T DefaultValue
        {
            get => _defaultValue;
            set => _defaultValue = value;
        }

        /// <summary>The lower bound used when <see cref="IsClamped" /> is <c>true</c>. Returns <c>default</c> if the type is not clampable.</summary>
        public virtual T MinClampValue
        {
            get
            {
                if (Clampable)
                {
                    return _minClampedValue;
                }

                return default;
            }
        }

        /// <summary>The upper bound used when <see cref="IsClamped" /> is <c>true</c>. Returns <c>default</c> if the type is not clampable.</summary>
        public virtual T MaxClampValue
        {
            get
            {
                if (Clampable)
                {
                    return _maxClampedValue;
                }

                return default;
            }
        }

        public override bool Clampable => false;
        public override bool ReadOnly => _readOnly;
        public override bool IsClamped => _isClamped;
        public override Type Type => typeof(T);
        public override Type ReferenceType => typeof(BaseReference<T, BaseVariable<T>>);
        public override bool UseDefaultValue => _useDefaultValue;

        public override object BaseValue
        {
            get => _value;
            set => SetValue((T) value);
        }


        /// <summary>Sets the value from another <see cref="BaseVariable{T}" />, extracting its <see cref="Value" />.</summary>
        public virtual T SetValue(BaseVariable<T> value)
        {
            return SetValue(value.Value);
        }


        /// <summary>
        ///     Applies <paramref name="newValue" /> to the variable, enforcing read-only protection and clamping.
        ///     Raises the change event only when the new value differs from the previous one.
        /// </summary>
        public virtual T SetValue(T newValue)
        {
            if (_readOnly)
            {
                RaiseReadonlyWarning();

                return _value;
            }

            if (Clampable && IsClamped)
            {
                newValue = ClampValue(newValue);
            }

            _value = newValue;

            if (!AreValuesEqual(newValue, _oldValue))
            {
                Raise();
            }

            _oldValue = _value;

            return newValue;
        }


        /// <summary>
        ///     Equality check between two values of type <typeparamref name="T" />.
        ///     Override in derived classes to use type-appropriate comparison (e.g., <see cref="UnityEngine.Mathf.Epsilon" /> for floats).
        /// </summary>
        protected virtual bool AreValuesEqual(T a, T b)
        {
            if (a != null)
            {
                return a.Equals(b);
            }

            return b == null;
        }


        /// <summary>
        ///     Clamps <paramref name="value" /> to the <see cref="MinClampValue" />/<see cref="MaxClampValue" /> range.
        ///     Base implementation is a no-op; override in types that support clamping.
        /// </summary>
        protected virtual T ClampValue(T value)
        {
            return value;
        }


        private void RaiseReadonlyWarning()
        {
            if (!_readOnly || !_raiseWarning)
            {
                return;
            }

            Debug.LogWarning("Tried to set value on " + name + ", but value is readonly!", this);
        }


        public override string ToString()
        {
            return _value == null ? "null" : _value.ToString();
        }


        /// <summary>Implicit conversion — allows a <see cref="BaseVariable{T}" /> to be used directly where a <typeparamref name="T" /> is expected.</summary>
        public static implicit operator T(BaseVariable<T> variable)
        {
            return variable.Value;
        }


        public void OnValidate()
        {
            SetValue(Value);
        }


        public void OnEnable()
        {
            _oldValue = _value;

            if (UseDefaultValue)
            {
                ResetToDefaultValue();
            }
        }


        private void ResetToDefaultValue()
        {
            Value = _defaultValue;
        }
    }


    /// <summary>
    ///     Extension of <see cref="BaseVariable{T}" /> that adds a serialized <see cref="UnityEngine.Events.UnityEvent{T}" />.
    ///     When the variable's value changes, <see cref="Raise" /> broadcasts to both the base game-event listeners
    ///     and the typed Unity Event, allowing inspector-wired callbacks to receive the new value.
    /// </summary>
    /// <typeparam name="T">The type of value stored by this variable.</typeparam>
    /// <typeparam name="TEvent">The serializable <see cref="UnityEngine.Events.UnityEvent{T}" /> type used for typed callbacks.</typeparam>

    public abstract class BaseVariable<T, TEvent> : BaseVariable<T> where TEvent : UnityEvent<T>
    {
        [SerializeField] private TEvent _event;


        public override void Raise()
        {
            base.Raise();

            _event.Invoke(Value);
        }


        /// <summary>Registers a typed callback that will be invoked with the new value whenever this variable changes.</summary>
        public void AddListener(UnityAction<T> callback)
        {
            _event.AddListener(callback);
        }


        /// <summary>Unregisters a previously registered typed callback.</summary>
        public void RemoveListener(UnityAction<T> callback)
        {
            _event.RemoveListener(callback);
        }


        public override void RemoveAll()
        {
            base.RemoveAll();
            _event.RemoveAllListeners();
        }
    }
}