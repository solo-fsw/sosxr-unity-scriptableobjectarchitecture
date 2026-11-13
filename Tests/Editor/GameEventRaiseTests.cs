using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;


namespace ScriptableObjectArchitecture.Tests
{
    public class GameEventRaiseTests
    {
        private GameEvent _event;
        private IntGameEvent _intEvent;
        private List<string> _calls;


        [SetUp]
        public void SetUp()
        {
            _event = ScriptableObject.CreateInstance<GameEvent>();
            _intEvent = ScriptableObject.CreateInstance<IntGameEvent>();
            _calls = new List<string>();
        }


        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_event);
            Object.DestroyImmediate(_intEvent);
        }


        [Test]
        public void Raise_CallsListenersInReverseRegistrationOrder()
        {
            _event.AddListener(new RecordingListener("a", _calls));
            _event.AddListener(new RecordingListener("b", _calls));
            _event.AddListener(new RecordingListener("c", _calls));

            _event.Raise();

            Assert.That(_calls, Is.EqualTo(new[] {"c", "b", "a"}));
        }


        [Test]
        public void Raise_ListenerRemovesTwoOthers_DoesNotThrow_AndSkipsRemoved()
        {
            var a = new RecordingListener("a", _calls);
            var b = new RecordingListener("b", _calls);
            var c = new RecordingListener("c", _calls);

            c.OnRaised = () =>
            {
                _event.RemoveListener(a);
                _event.RemoveListener(b);
            };

            _event.AddListener(a);
            _event.AddListener(b);
            _event.AddListener(c);

            Assert.DoesNotThrow(() => _event.Raise());
            Assert.That(_calls, Is.EqualTo(new[] {"c"}));
        }


        [Test]
        public void Raise_ListenerRemovesItselfAndAnother_DoesNotThrow()
        {
            var a = new RecordingListener("a", _calls);
            var b = new RecordingListener("b", _calls);
            var c = new RecordingListener("c", _calls);

            b.OnRaised = () =>
            {
                _event.RemoveListener(b);
                _event.RemoveListener(c);
            };

            _event.AddListener(a);
            _event.AddListener(b);
            _event.AddListener(c);

            Assert.DoesNotThrow(() => _event.Raise());
            Assert.That(_calls, Is.EqualTo(new[] {"c", "b", "a"}));

            _calls.Clear();
            _event.Raise();
            Assert.That(_calls, Is.EqualTo(new[] {"a"}));
        }


        [Test]
        public void Raise_ListenerRemovesEarlierListener_NoListenerCalledTwice()
        {
            var a = new RecordingListener("a", _calls);
            var b = new RecordingListener("b", _calls);
            var c = new RecordingListener("c", _calls);

            c.OnRaised = () => _event.RemoveListener(a);

            _event.AddListener(a);
            _event.AddListener(b);
            _event.AddListener(c);

            _event.Raise();

            Assert.That(_calls, Is.EqualTo(new[] {"c", "b"}));
        }


        [Test]
        public void Raise_ListenerAddedDuringRaise_IsCalledFromNextRaise()
        {
            var late = new RecordingListener("late", _calls);
            var a = new RecordingListener("a", _calls);
            a.OnRaised = () => _event.AddListener(late);
            _event.AddListener(a);

            _event.Raise();
            Assert.That(_calls, Is.EqualTo(new[] {"a"}));

            _calls.Clear();
            _event.Raise();
            Assert.That(_calls, Is.EqualTo(new[] {"late", "a"}));
        }


        [Test]
        public void Raise_ActionRemovesTwoOtherActions_DoesNotThrow()
        {
            Action a = () => _calls.Add("a");
            Action b = () => _calls.Add("b");
            Action c = null;

            c = () =>
            {
                _calls.Add("c");
                _event.RemoveListener(a);
                _event.RemoveListener(b);
            };

            _event.AddListener(a);
            _event.AddListener(b);
            _event.AddListener(c);

            Assert.DoesNotThrow(() => _event.Raise());
            Assert.That(_calls, Is.EqualTo(new[] {"c"}));
        }


        [Test]
        public void Raise_ReentrantRaise_CallsEveryListenerOncePerRaise()
        {
            var depth = 0;
            var a = new RecordingListener("a", _calls);
            var b = new RecordingListener("b", _calls);

            b.OnRaised = () =>
            {
                if (depth++ == 0)
                {
                    _event.Raise();
                }
            };

            _event.AddListener(a);
            _event.AddListener(b);

            Assert.DoesNotThrow(() => _event.Raise());
            Assert.That(_calls, Is.EqualTo(new[] {"b", "b", "a", "a"}));
        }


        [Test]
        public void Raise_ListenerThrows_NextRaiseStillWorks()
        {
            var thrower = new RecordingListener("throw", _calls) {OnRaised = () => throw new InvalidOperationException("boom")};
            _event.AddListener(new RecordingListener("a", _calls));
            _event.AddListener(thrower);

            Assert.Throws<InvalidOperationException>(() => _event.Raise());

            _event.RemoveListener(thrower);
            _calls.Clear();
            _event.Raise();

            Assert.That(_calls, Is.EqualTo(new[] {"a"}));
        }


        [Test]
        public void TypedRaise_ListenerRemovesTwoOthers_DoesNotThrow_AndSkipsRemoved()
        {
            var a = new RecordingIntListener("a", _calls);
            var b = new RecordingIntListener("b", _calls);
            var c = new RecordingIntListener("c", _calls);

            c.OnRaised = () =>
            {
                _intEvent.RemoveListener(a);
                _intEvent.RemoveListener(b);
            };

            _intEvent.AddListener(a);
            _intEvent.AddListener(b);
            _intEvent.AddListener(c);

            Assert.DoesNotThrow(() => _intEvent.Raise(7));
            Assert.That(_calls, Is.EqualTo(new[] {"c:7"}));
        }


        [Test]
        public void TypedRaise_TypedActionRemovesTwoOthers_DoesNotThrow()
        {
            Action<int> a = v => _calls.Add("a" + v);
            Action<int> b = v => _calls.Add("b" + v);
            Action<int> c = null;

            c = v =>
            {
                _calls.Add("c" + v);
                _intEvent.RemoveListener(a);
                _intEvent.RemoveListener(b);
            };

            _intEvent.AddListener(a);
            _intEvent.AddListener(b);
            _intEvent.AddListener(c);

            Assert.DoesNotThrow(() => _intEvent.Raise(3));
            Assert.That(_calls, Is.EqualTo(new[] {"c3"}));
        }


        [Test]
        public void TypedRaise_CallsAllFourListenerKindsInOrder()
        {
            _intEvent.AddListener(new RecordingIntListener("typed", _calls));
            _intEvent.AddListener((IGameEventListener) new RecordingListener("untyped", _calls));
            _intEvent.AddListener((Action<int>) (v => _calls.Add("typedAction:" + v)));
            _intEvent.AddListener((Action) (() => _calls.Add("action")));

            _intEvent.Raise(5);

            Assert.That(_calls, Is.EqualTo(new[] {"typed:5", "untyped", "typedAction:5", "action"}));
        }


        [Test]
        public void TypedRemoveAll_ClearsTypedAndUntypedListeners()
        {
            _intEvent.AddListener(new RecordingIntListener("typed", _calls));
            _intEvent.AddListener((IGameEventListener) new RecordingListener("untyped", _calls));
            _intEvent.AddListener((Action<int>) (v => _calls.Add("typedAction")));
            _intEvent.AddListener((Action) (() => _calls.Add("action")));

            _intEvent.RemoveAll();
            _intEvent.Raise(1);

            Assert.That(_calls, Is.Empty);
        }


        [Test]
        public void RemoveAll_DuringRaise_SkipsRemainingListeners()
        {
            var a = new RecordingListener("a", _calls);
            var b = new RecordingListener("b", _calls) {OnRaised = () => _event.RemoveAll()};
            _event.AddListener(a);
            _event.AddListener(b);

            _event.Raise();

            Assert.That(_calls, Is.EqualTo(new[] {"b"}));
        }


        private sealed class RecordingListener : IGameEventListener
        {
            private readonly string _name;
            private readonly List<string> _calls;
            public Action OnRaised;


            public RecordingListener(string name, List<string> calls)
            {
                _name = name;
                _calls = calls;
            }


            public void OnEventRaised()
            {
                _calls.Add(_name);
                OnRaised?.Invoke();
            }
        }


        private sealed class RecordingIntListener : IGameEventListener<int>
        {
            private readonly string _name;
            private readonly List<string> _calls;
            public Action OnRaised;


            public RecordingIntListener(string name, List<string> calls)
            {
                _name = name;
                _calls = calls;
            }


            public void OnEventRaised(int value)
            {
                _calls.Add(_name + ":" + value);
                OnRaised?.Invoke();
            }
        }
    }
}
