using System.Collections;
using Type = System.Type;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Non-generic base class for ScriptableObject collection assets.
    ///     Provides a type-erased <see cref="System.Collections.IList" /> view and implements
    ///     <see cref="System.Collections.IEnumerable" /> for iteration in editor tooling.
    /// </summary>

    public abstract class BaseCollection : SOArchitectureBaseObject, IEnumerable
    {
        /// <summary>Gets or sets the element at <paramref name="index" /> as a boxed object.</summary>
        public object this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        /// <summary>The number of elements currently in the collection.</summary>
        public int Count => List.Count;

        /// <summary>The underlying <see cref="System.Collections.IList" /> — implemented by the typed subclass.</summary>
        public abstract IList List { get; }
        /// <summary>The concrete element type stored in this collection.</summary>
        public abstract Type Type { get; }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }


        /// <summary>Returns <c>true</c> if the boxed <paramref name="obj" /> is present in the collection.</summary>
        public bool Contains(object obj)
        {
            return List.Contains(obj);
        }
    }
}