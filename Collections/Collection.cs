using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Generic ScriptableObject collection holding a serialized <see cref="System.Collections.Generic.List{T}" />.
    ///     Implements both typed and untyped enumeration and provides common list operations (<see cref="Add" />, <see cref="Remove" />, <see cref="Contains" />, etc.).
    /// </summary>
    /// <typeparam name="T">The element type stored in this collection.</typeparam>

    public class Collection<T> : BaseCollection, IEnumerable<T>
    {
        [SerializeField] private List<T> _list = new();

        public new T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public override IList List => _list;

        public override Type Type => typeof(T);


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }


        /// <summary>Appends <paramref name="obj" /> to the end of the collection.</summary>
        public void Add(T obj)
        {
            _list.Add(obj);
        }


        /// <summary>Removes the first occurrence of <paramref name="obj" /> if it exists.</summary>
        public void Remove(T obj)
        {
            if (_list.Contains(obj))
            {
                _list.Remove(obj);
            }
        }


        /// <summary>Removes all elements from the collection.</summary>
        public void Clear()
        {
            _list.Clear();
        }


        /// <summary>Returns <c>true</c> if <paramref name="value" /> exists in the collection.</summary>
        public bool Contains(T value)
        {
            return _list.Contains(value);
        }


        /// <summary>Returns the zero-based index of <paramref name="value" />, or <c>-1</c> if not found.</summary>
        public int IndexOf(T value)
        {
            return _list.IndexOf(value);
        }


        /// <summary>Removes the element at <paramref name="index" />.</summary>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }


        /// <summary>Inserts <paramref name="value" /> at the specified <paramref name="index" />.</summary>
        public void Insert(int index, T value)
        {
            _list.Insert(index, value);
        }


        public override string ToString()
        {
            return "Collection<" + typeof(T) + ">(" + Count + ")";
        }


        /// <summary>Copies the collection elements to a new array.</summary>
        public T[] ToArray()
        {
            return _list.ToArray();
        }
    }
}