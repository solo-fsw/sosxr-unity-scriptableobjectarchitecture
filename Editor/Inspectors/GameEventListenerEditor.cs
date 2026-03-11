using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace ScriptableObjectArchitecture.Editor
{
    /// <summary>Custom Inspector for two-argument <see cref="BaseGameEventListener{TType,TEvent}"/> components; provides a "Raise" button that invokes <c>OnEventRaised</c> via reflection for editor-time testing.</summary>
    [CustomEditor(typeof(BaseGameEventListener<,>), true)]
    public class GameEventListenerEditor : BaseGameEventListenerEditor
    {
        private MethodInfo _raiseMethod;


        protected override void OnEnable()
        {
            base.OnEnable();

            _raiseMethod = target.GetType().BaseType.GetMethod("OnEventRaised");
        }


        protected override void DrawRaiseButton()
        {
            if (GUILayout.Button("Raise"))
            {
                _raiseMethod.Invoke(target, null);
            }
        }
    }
}