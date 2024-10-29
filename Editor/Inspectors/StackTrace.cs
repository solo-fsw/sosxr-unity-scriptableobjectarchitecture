﻿using System;
using System.Linq;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;


namespace ScriptableObjectArchitecture.Editor
{
    public class StackTrace
    {
        private StackTraceEntry _selectedTrace;

        private readonly IStackTraceObject _target;
        private Rect _listRect;
        private Rect _stackTraceRect;
        private Rect _contentRect;
        private Vector2 _listScrollPosition;
        private Vector2 _contentScrollPosition;
        private readonly AnimBool _collapseAnimation;
        private float _subWindowValue = 0.6f;
        private float _splitHeight;
        private bool _resizeMouseDown;


        public StackTrace(IStackTraceObject target, bool startCollapsed = false)
        {
            if (target == null)
            {
                throw new NullReferenceException();
            }

            _target = target;

            _collapseAnimation = new AnimBool(!startCollapsed);
            _collapseAnimation.valueChanged.AddListener(Repaint);

            OnRepaint = new UnityEvent();
        }


        public float Height { get; set; } = DEFAULT_HEIGHT;

        public UnityEvent OnRepaint { get; set; }

        private const float DEFAULT_HEIGHT = 400;

        private const float HEADER_HEIGHT = 18;
        private const float RESIZE_MARGIN = 0.2f;
        private const float RESIZE_HEIGHT = 5;
        private const float CLEAR_LEFT_PADDING = 6;
        private const float CLEAR_WIDTH = 45;
        private const float COLLAPSE_WIDTH = 55;
        private const float LINE_HEIGHT = 18;


        public void Draw()
        {
            EditorGUILayout.Space();

            var rect = GUILayoutUtility.GetRect(0, GetHeight());

            if (Event.current.type == EventType.Repaint)
            {
                //This is necessary due to Unity's retarded handling of events - https://answers.unity.com/questions/515197/how-to-use-guilayoututilitygetrect-properly.html
                _stackTraceRect = rect;

                var boxRect = _stackTraceRect;

                boxRect.x--;
                boxRect.y--;

                boxRect.width++;
                boxRect.height++;

                Styles.Box.Draw(boxRect, GUIContent.none, 0);
            }

            _subWindowValue = Mathf.Clamp(_subWindowValue, RESIZE_MARGIN, 1 - RESIZE_MARGIN);

            _splitHeight = _stackTraceRect.height * _subWindowValue;
            _contentRect = new Rect(-1, _splitHeight, _stackTraceRect.width + 1, _stackTraceRect.height - _splitHeight);

            _listRect = new Rect
            {
                y = HEADER_HEIGHT,
                height = _stackTraceRect.height - HEADER_HEIGHT - _contentRect.height,
                width = _stackTraceRect.width
            };

            GUILayout.BeginArea(_stackTraceRect);

            DrawStackTraceHeader();

            EditorGUILayout.BeginFadeGroup(_collapseAnimation.faded);

            if (_collapseAnimation.faded > 0)
            {
                DrawList();
                DrawSelectedContent();
            }

            EditorGUILayout.EndFadeGroup();

            GUILayout.EndArea();
        }


        private void DrawSelectedContent()
        {
            var cursorRect = new Rect(0, _splitHeight - RESIZE_HEIGHT / 2, _stackTraceRect.width, RESIZE_HEIGHT);
            var currentEvent = Event.current;

            EditorGUIUtility.AddCursorRect(cursorRect, MouseCursor.ResizeVertical);

            if (currentEvent.type == EventType.Repaint)
            {
                Styles.Box.Draw(_contentRect, GUIContent.none, 0);
            }
            else if (currentEvent.type == EventType.MouseDown && cursorRect.Contains(currentEvent.mousePosition))
            {
                _resizeMouseDown = true;
            }
            else if (currentEvent.type == EventType.MouseUp && _resizeMouseDown)
            {
                _resizeMouseDown = false;
            }
            else if (_resizeMouseDown)
            {
                _subWindowValue = currentEvent.mousePosition.y / _stackTraceRect.height;

                Repaint();
            }

            if (_selectedTrace != null)
            {
                var scrollViewRect = new Rect
                {
                    y = _contentRect.y,
                    height = _contentRect.height,
                    width = _contentRect.width
                };

                var textSize = Styles.MessageStyle.CalcSize(new GUIContent(_selectedTrace));

                var position = new Rect(Vector2.zero, textSize);

                _contentScrollPosition = GUI.BeginScrollView(scrollViewRect, _contentScrollPosition, position);

                EditorGUI.SelectableLabel(position, _selectedTrace, Styles.MessageStyle);

                GUI.EndScrollView();
            }
        }


        private void DrawList()
        {
            var scrollViewRect = new Rect
            {
                y = _listRect.y,
                height = _listRect.height,
                width = _listRect.width
            };

            var position = new Rect
            {
                height = _target.StackTraces.Count * LINE_HEIGHT,
                width = scrollViewRect.width - 20
            };

            _listScrollPosition = GUI.BeginScrollView(scrollViewRect, _listScrollPosition, position);

            for (var i = 0; i < _target.StackTraces.Count; i++)
            {
                var currentTrace = _target.StackTraces[i];
                var currentText = GetFirstLine(currentTrace);

                var elementRect = new Rect
                {
                    width = _listRect.width,
                    height = LINE_HEIGHT,
                    y = i * LINE_HEIGHT
                };

                if (Event.current.type == EventType.MouseDown && elementRect.Contains(Event.current.mousePosition))
                {
                    _selectedTrace = currentTrace;

                    Repaint();
                }
                else if (Event.current.type == EventType.Repaint)
                {
                    var isSelected = _selectedTrace == currentTrace;
                    var backgroundStyle = i % 2 == 0 ? Styles.EvenBackground : Styles.OddBackground;

                    backgroundStyle.Draw(elementRect, false, false, isSelected, false);
                    Styles.Text.Draw(elementRect, new GUIContent(currentText), 0);
                }
            }

            GUI.EndScrollView();
        }


        private void DrawStackTraceHeader()
        {
            var rect = new Rect
            {
                width = _stackTraceRect.width,
                height = HEADER_HEIGHT
            };

            if (Event.current.type == EventType.Repaint)
            {
                Styles.Header.Draw(rect, new GUIContent("Stack Trace"), 0);
            }

            rect.x += CLEAR_LEFT_PADDING;
            rect.width = CLEAR_WIDTH;

            if (GUI.Button(rect, new GUIContent("Clear"), Styles.HeaderButton))
            {
                _target.StackTraces.Clear();
                Deselect();
            }

            rect.x += CLEAR_WIDTH;
            rect.width = COLLAPSE_WIDTH;

            _collapseAnimation.target = !GUI.Toggle(rect, !_collapseAnimation.target, new GUIContent("Collapse"), Styles.HeaderButton);
        }


        private float GetHeight()
        {
            return Mathf.Clamp(Height * _collapseAnimation.faded, HEADER_HEIGHT, float.MaxValue);
        }


        private void Repaint()
        {
            OnRepaint.Invoke();
        }


        private string GetFirstLine(string value)
        {
            return value.Split('\r', '\n').FirstOrDefault();
        }


        private void Deselect()
        {
            _selectedTrace = null;
        }


        private class Styles
        {
            static Styles()
            {
                Box = new GUIStyle("CN Box");
                EvenBackground = new GUIStyle("CN EntryBackEven");
                OddBackground = new GUIStyle("CN EntryBackodd");
                HeaderButton = new GUIStyle("ToolbarButton");
                MessageStyle = new GUIStyle("CN Message");

                Header = new GUIStyle("Toolbar");
                Header.alignment = TextAnchor.MiddleCenter;

                Text = new GUIStyle("CN EntryInfo");
                Text.padding = new RectOffset(0, 0, 3, 0);
            }


            public static readonly GUIStyle HeaderButton;
            public static readonly GUIStyle Text;
            public static readonly GUIStyle Box;
            public static readonly GUIStyle Header;
            public static readonly GUIStyle EvenBackground;
            public static readonly GUIStyle OddBackground;
            public static readonly GUIStyle MessageStyle;
        }
    }
}