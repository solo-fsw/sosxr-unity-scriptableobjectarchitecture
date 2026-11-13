using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace ScriptableObjectArchitecture.Tests
{
    public class StackTraceTests
    {
        private const string EnableDebugPref = "SOArchitecture.EnableDebug";

        private bool _hadPref;
        private bool _previousPref;
        private GameEvent _event;
        private GameObject _gameObject;
        private GameEventListener _listener;


        [SetUp]
        public void SetUp()
        {
            _hadPref = EditorPrefs.HasKey(EnableDebugPref);
            _previousPref = EditorPrefs.GetBool(EnableDebugPref, true);

            _event = ScriptableObject.CreateInstance<GameEvent>();
            _gameObject = new GameObject("StackTraceTests");
            _listener = _gameObject.AddComponent<GameEventListener>();
        }


        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
            Object.DestroyImmediate(_event);

            if (_hadPref)
            {
                EditorPrefs.SetBool(EnableDebugPref, _previousPref);
            }
            else
            {
                EditorPrefs.DeleteKey(EnableDebugPref);
            }
        }


        [Test]
        public void Event_DebugDisabled_RecordsNoStackTraces()
        {
            EditorPrefs.SetBool(EnableDebugPref, false);

            _event.Raise();
            _event.Raise();

            Assert.That(_event.StackTraces, Is.Empty);
        }


        [Test]
        public void Event_DebugEnabled_RecordsMostRecentFirst()
        {
            EditorPrefs.SetBool(EnableDebugPref, true);

            _event.AddStackTrace("first");
            _event.AddStackTrace("second");

            Assert.That(_event.StackTraces.Count, Is.EqualTo(2));
            StringAssert.Contains("second", _event.StackTraces[0].ToString());
        }


        [Test]
        public void Event_DebugEnabled_StackTracesAreCapped()
        {
            EditorPrefs.SetBool(EnableDebugPref, true);

            for (var i = 0; i < StackTraceEntry.MaxEntries + 25; i++)
            {
                _event.Raise();
            }

            Assert.That(_event.StackTraces.Count, Is.EqualTo(StackTraceEntry.MaxEntries));
        }


        [Test]
        public void Listener_DebugDisabled_RecordsNoStackTraces()
        {
            EditorPrefs.SetBool(EnableDebugPref, false);

            _listener.AddStackTrace();
            _listener.AddStackTrace(1);

            Assert.That(_listener.StackTraces, Is.Empty);
        }


        [Test]
        public void Listener_DebugEnabled_StackTracesAreCapped()
        {
            EditorPrefs.SetBool(EnableDebugPref, true);

            for (var i = 0; i < StackTraceEntry.MaxEntries + 25; i++)
            {
                _listener.AddStackTrace(i);
            }

            Assert.That(_listener.StackTraces.Count, Is.EqualTo(StackTraceEntry.MaxEntries));
        }


        [Test]
        public void Record_KeepsNewestEntries()
        {
            var list = new List<StackTraceEntry>();

            for (var i = 0; i < StackTraceEntry.MaxEntries + 5; i++)
            {
                StackTraceEntry.Record(list, StackTraceEntry.Create("entry" + i));
            }

            Assert.That(list.Count, Is.EqualTo(StackTraceEntry.MaxEntries));
            StringAssert.Contains("entry" + (StackTraceEntry.MaxEntries + 4), list[0].ToString());
        }
    }
}
