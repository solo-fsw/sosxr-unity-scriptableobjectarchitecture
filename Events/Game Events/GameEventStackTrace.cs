using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Immutable record of a single event-raise call, capturing the call stack, an optional payload value,
    ///     and the frame count at the time of the call.
    ///     Created via the factory methods <see cref="Create()" /> and <see cref="Create(object)" />.
    ///     Implicitly converts to <see cref="string" /> for inspector display.
    /// </summary>

    public class StackTraceEntry : IEquatable<StackTraceEntry>
    {
        /// <summary>Maximum number of entries kept per event or listener. Older entries are dropped first.</summary>
        public const int MaxEntries = 100;

        private readonly int _id;
        private readonly int _frameCount;
        private readonly string _stackTrace;
        private readonly object _value;
        private readonly bool _constructedWithValue;


        private StackTraceEntry(string trace)
        {
            _id = Random.Range(int.MinValue, int.MaxValue);
            _stackTrace = trace;

            if (Application.isPlaying)
            {
                _frameCount = Time.frameCount;
            }
        }


        private StackTraceEntry(string trace, object value)
        {
            _value = value;
            _constructedWithValue = true;
            _id = Random.Range(int.MinValue, int.MaxValue);
            _stackTrace = trace;

            if (Application.isPlaying)
            {
                _frameCount = Time.frameCount;
            }
        }


        /// <summary>Returns true when <paramref name="other" /> has the same unique id.</summary>
        public bool Equals(StackTraceEntry other)
        {
            return other._id == _id;
        }


        /// <summary>Creates a <see cref="StackTraceEntry" /> that includes the raised <paramref name="obj" /> value in its string representation.</summary>
        public static StackTraceEntry Create(object obj)
        {
            return new StackTraceEntry(Environment.StackTrace, obj);
        }


        /// <summary>
        ///     Inserts <paramref name="entry" /> at the front of <paramref name="list" /> (most recent first) and
        ///     drops the oldest entries so the list never holds more than <see cref="MaxEntries" />.
        /// </summary>
        public static void Record(List<StackTraceEntry> list, StackTraceEntry entry)
        {
            list.Insert(0, entry);

            if (list.Count > MaxEntries)
            {
                list.RemoveRange(MaxEntries, list.Count - MaxEntries);
            }
        }


        /// <summary>Creates a parameterless <see cref="StackTraceEntry" /> capturing only the current call stack.</summary>
        public static StackTraceEntry Create()
        {
            return new StackTraceEntry(Environment.StackTrace);
        }


        /// <summary>Returns true when <paramref name="obj" /> is a <see cref="StackTraceEntry" /> with the same unique id.</summary>
        public override bool Equals(object obj)
        {
            if (obj is StackTraceEntry)
            {
                return Equals(obj as StackTraceEntry);
            }

            return false;
        }


        /// <summary>Returns the unique id as the hash code.</summary>
        public override int GetHashCode()
        {
            return _id;
        }


        /// <summary>Returns the frame count, optional payload value, and stack trace as text.</summary>
        public override string ToString()
        {
            if (_constructedWithValue)
            {
                return string.Format("{1}   [{0}] {2}", _value == null ? "null" : _value.ToString(), _frameCount, _stackTrace);
            }

            return string.Format("{0} {1}", _frameCount, _stackTrace);
        }


        /// <summary>Implicitly converts this entry to a formatted string for inspector display.</summary>
        public static implicit operator string(StackTraceEntry trace)
        {
            return trace.ToString();
        }
    }
}