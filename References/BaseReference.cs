using System;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [Serializable]
    /// <summary>
    ///     Serializable dual-mode reference: can hold either a local constant value or point to a
    ///     <see cref="BaseVariable{TBase}" /> ScriptableObject asset.
    ///     The active mode is toggled via the <c>Use Constant</c> / <c>Use Variable</c> inspector popup
    ///     drawn by <see cref="ScriptableObjectArchitecture.Editor.BaseReferenceDrawer" />.
    ///     Use in MonoBehaviour fields to decouple hard-coded values from shared variable assets.
    /// </summary>
    /// <typeparam name="TBase">The value type (e.g. <see cref="float" />, <see cref="int" />).</typeparam>
    /// <typeparam name="TVariable">The corresponding <see cref="BaseVariable{TBase}" /> asset type.</typeparam>

    public class BaseReference<TBase, TVariable> : BaseReference where TVariable : BaseVariable<TBase>
    {
        [SerializeField] protected bool _useConstant;
        [SerializeField] protected TBase _constantValue;
        [SerializeField] protected TVariable _variable;


        public BaseReference()
        {
        }


        public BaseReference(TBase baseValue)
        {
            _useConstant = true;
            _constantValue = baseValue;
        }


        /// <summary>
        ///     Gets or sets the underlying variable asset. Setting this also switches the reference to variable mode
        ///     (<c>_useConstant = false</c>).
        /// </summary>
        public TVariable Variable
        {
            get => _variable;
            set
            {
                _useConstant = false;
                _variable = value;
            }
        }

        /// <summary>
        ///     Gets or sets the effective value.
        ///     Reads from the constant or the variable depending on the active mode.
        ///     Writing while in variable mode updates the variable asset; writing while in constant mode updates the local constant.
        /// </summary>
        public TBase Value
        {
            get => _useConstant || _variable == null ? _constantValue : _variable.Value;
            set
            {
                if (!_useConstant && _variable != null)
                {
                    _variable.Value = value;
                }
                else
                {
                    _useConstant = true;
                    _constantValue = value;
                }
            }
        }

        /// <summary><c>true</c> when the reference has a usable value — either a constant is set or a variable asset is assigned.</summary>
        public bool IsValueDefined => _useConstant || _variable != null;


        /// <summary>Creates a shallow copy of this reference with identical mode, constant value, and variable asset.</summary>
        public BaseReference CreateCopy()
        {
            var copy = (BaseReference<TBase, TVariable>) Activator.CreateInstance(GetType());
            copy._useConstant = _useConstant;
            copy._constantValue = _constantValue;
            copy._variable = _variable;

            return copy;
        }


        /// <summary>Registers an <see cref="IGameEventListener" /> with the underlying variable, if one is assigned.</summary>
        public void AddListener(IGameEventListener listener)
        {
            if (_variable != null)
            {
                _variable.AddListener(listener);
            }
        }


        /// <summary>Unregisters an <see cref="IGameEventListener" /> from the underlying variable.</summary>
        public void RemoveListener(IGameEventListener listener)
        {
            if (_variable != null)
            {
                _variable.RemoveListener(listener);
            }
        }


        /// <summary>Registers a parameterless <see cref="System.Action" /> callback with the underlying variable's change event.</summary>
        public void AddListener(Action action)
        {
            if (_variable != null)
            {
                _variable.AddListener(action);
            }
        }


        /// <summary>Unregisters a parameterless <see cref="System.Action" /> callback from the underlying variable's change event.</summary>
        public void RemoveListener(Action action)
        {
            if (_variable != null)
            {
                _variable.RemoveListener(action);
            }
        }


        public override string ToString()
        {
            return Value.ToString();
        }
    }


    // Non-generic marker base required because Unity's property drawer system cannot match generic arguments directly.
    /// <summary>
    ///     Non-generic marker base for all <see cref="BaseReference{TBase,TVariable}" /> types.
    ///     Required because Unity's <c>CustomPropertyDrawer</c> attribute cannot target open generic types directly.
    /// </summary>

    public abstract class BaseReference
    {
    }
}