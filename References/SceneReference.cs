using System;


namespace ScriptableObjectArchitecture
{
    /// <summary>A <see cref="BaseReference{TBase,TVariable}"/> for <see cref="ScriptableObjectArchitecture.SceneInfo"/> values.</summary>
    [Serializable]
    public sealed class SceneReference : BaseReference<SceneInfo, SceneVariable>
    {
        public SceneReference()
        {
        }


        public SceneReference(SceneInfo value) : base(value)
        {
        }
    }
}