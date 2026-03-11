using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "SceneCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "Scene",
        order = 120)]
    /// <summary>ScriptableObject collection of <see cref="ScriptableObjectArchitecture.SceneInfo"/> items.</summary>
    public class SceneCollection : Collection<SceneInfo>
    {
    }
}