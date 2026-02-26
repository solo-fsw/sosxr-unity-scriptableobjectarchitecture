using UnityEditor;
using UnityEngine;


namespace ScriptableObjectArchitecture.Editor
{
    /// <summary>Custom Inspector for typeless <see cref="GameEvent"/> assets; draws a "Raise" button and inherits the stack-trace panel from <see cref="BaseGameEventEditor"/>.</summary>
    [CustomEditor(typeof(GameEventBase), true)]
    public sealed class GameEventEditor : BaseGameEventEditor
    {
        private GameEvent Target => (GameEvent) target;


        protected override void DrawRaiseButton()
        {
            if (GUILayout.Button("Raise"))
            {
                Target.Raise();
            }
        }
    }
}