﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventListener<TType, TEvent, TResponse> : DebuggableGameEventListener, IGameEventListener<TType>
        where TEvent : GameEventBase<TType>
        where TResponse : UnityEvent<TType>
    {
        [SerializeField]
        private TEvent _previouslyRegisteredEvent = default;
        [SerializeField]
        private TEvent _event = default;
        [SerializeField]
        private TResponse _response = default;

        [SerializeField]
        protected TType _debugValue = default;
        protected override ScriptableObject GameEvent => _event;
        protected override UnityEventBase Response => _response;


        public void OnEventRaised(TType value)
        {
            RaiseResponse(value);

            CreateDebugEntry(_response);

            AddStackTrace(value);
        }


        private void RaiseResponse(TType value)
        {
            _response.Invoke(value);
        }


        private void OnEnable()
        {
            if (_event != null)
            {
                Register();
            }
        }


        private void OnDisable()
        {
            if (_event != null)
            {
                _event.RemoveListener(this);
            }
        }


        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }


    public abstract class BaseGameEventListener<TEvent, TResponse> : DebuggableGameEventListener, IGameEventListener
        where TEvent : GameEventBase
        where TResponse : UnityEvent
    {
        [SerializeField]
        private TEvent _previouslyRegisteredEvent = default;
        [SerializeField]
        private TEvent _event = default;
        [SerializeField]
        private TResponse _response = default;
        protected override ScriptableObject GameEvent => _event;
        protected override UnityEventBase Response => _response;


        public void OnEventRaised()
        {
            RaiseResponse();

            CreateDebugEntry(_response);

            AddStackTrace();
        }


        protected void RaiseResponse()
        {
            _response.Invoke();
        }


        private void OnEnable()
        {
            if (_event != null)
            {
                Register();
            }
        }


        private void OnDisable()
        {
            if (_event != null)
            {
                _event.RemoveListener(this);
            }
        }


        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }


    public abstract class DebuggableGameEventListener : SOArchitectureBaseMonobehaviour, IStackTraceObject
    {
        #pragma warning disable 0414
        [SerializeField]
        private bool _showDebugFields = false;
        [SerializeField]
        private bool _enableGizmoDebugging = true;
        [SerializeField]
        private Color _debugColor = Color.cyan;
        #pragma warning restore

        public List<StackTraceEntry> StackTraces { get; } = new();

        protected abstract ScriptableObject GameEvent { get; }
        protected abstract UnityEventBase Response { get; }


        public void AddStackTrace(object obj)
        {
            #if UNITY_EDITOR
            StackTraces.Insert(0, StackTraceEntry.Create(obj));
            #endif
        }


        public void AddStackTrace()
        {
            #if UNITY_EDITOR
            StackTraces.Insert(0, StackTraceEntry.Create());
            #endif
        }


        protected void CreateDebugEntry(UnityEventBase response)
        {
            #if UNITY_EDITOR
            for (var i = 0; i < response.GetPersistentEventCount(); i++)
            {
                var gameObjectTarget = GetGameObject(response.GetPersistentTarget(i));

                if (gameObject == null || gameObjectTarget == null)
                {
                    continue;
                }

                if (Vector3.Distance(gameObject.transform.position, gameObjectTarget.transform.position) <= EVENT_MIN_DISTANCE)
                {
                    continue;
                }

                var targetName = gameObject ? gameObject.name : "Null";

                var functionName = string.Format("{0} ({1})", targetName, response.GetPersistentMethodName(i));

                _debugEntries.Add(new DebugEvent(gameObjectTarget, functionName));
            }
            #endif
        }


        #if UNITY_EDITOR
        private const float DOTTED_LINE_LENGTH = 5;
        private const float DOT_LENGTH = 0.5f;
        private const float DOT_WIDTH = 3;
        private const float EVENT_MOVEMENT_SPEED = 3;
        private const float EVENT_MIN_DISTANCE = 0.3f;

        private readonly List<DebugEvent> _debugEntries = new();


        private static class Styles
        {
            static Styles()
            {
                TextStyle = new GUIStyle();
                TextStyle.alignment = TextAnchor.UpperCenter;
            }


            public static readonly GUIStyle TextStyle;
        }


        private void OnDrawGizmos()
        {
            UpdateDebugInfo();
        }


        private void UpdateDebugInfo()
        {
            Handles.color = _debugColor;
            Styles.TextStyle.normal.textColor = _debugColor;

            DrawLine();
            DrawEvents();
        }


        private void DrawEvents()
        {
            for (var i = _debugEntries.Count - 1; i >= 0; i--)
            {
                DrawEvent(i);
            }
        }


        private void DrawEvent(int index)
        {
            var debugEvent = _debugEntries[index];

            if (debugEvent.Target == null)
            {
                return;
            }

            debugEvent.Offset += EVENT_MOVEMENT_SPEED * Time.deltaTime;

            var delta = (debugEvent.Target.transform.position - gameObject.transform.position).normalized;
            var position = gameObject.transform.position + delta * debugEvent.Offset;

            DrawPoint(position, delta);
            DrawText(position, debugEvent);

            if (debugEvent.Offset >= Vector3.Distance(gameObject.transform.position, debugEvent.Target.transform.position))
            {
                _debugEntries.RemoveAt(index);
            }
        }


        private void DrawText(Vector3 position, DebugEvent debugEvent)
        {
            if (!EnableGizmoDebuggin())
            {
                return;
            }

            var text = string.Join("\n", GameEvent.name, debugEvent.FunctionName);

            Handles.Label(position, text, Styles.TextStyle);
        }


        private void DrawLine()
        {
            if (!EnableGizmoDebuggin())
            {
                return;
            }

            var listeningObjects = new List<GameObject>();

            for (var i = 0; i < Response.GetPersistentEventCount(); i++)
            {
                AddObject(listeningObjects, Response.GetPersistentTarget(i));
            }

            foreach (var obj in listeningObjects)
            {
                if (gameObject == obj)
                {
                    continue;
                }

                Handles.DrawDottedLine(transform.position, obj.transform.position, DOTTED_LINE_LENGTH);
            }
        }


        private void DrawPoint(Vector3 position, Vector3 direction)
        {
            if (EnableGizmoDebuggin())
            {
                Handles.DrawAAPolyLine(DOT_WIDTH, position, position + direction.normalized * DOT_LENGTH);
            }
        }


        private bool EnableGizmoDebuggin()
        {
            if (!SOArchitecturePreferences.AreGizmosEnabled)
            {
                return false;
            }

            return _enableGizmoDebugging;
        }


        private void AddObject(List<GameObject> listeningObjects, Object obj)
        {
            var toAdd = GetGameObject(obj);

            if (!listeningObjects.Contains(toAdd) && toAdd != null)
            {
                listeningObjects.Add(toAdd);
            }
        }


        private GameObject GetGameObject(Object obj)
        {
            if (obj is Component)
            {
                var component = obj as Component;

                return component.gameObject;
            }

            if (obj is GameObject)
            {
                return obj as GameObject;
            }

            return null;
        }


        private class DebugEvent
        {
            public float Offset;
            public readonly GameObject Target;
            public readonly string FunctionName;


            public DebugEvent(GameObject target, string methodName)
            {
                FunctionName = methodName;
                Target = target;
                Offset = 0;
            }
        }
        #endif
    }
}