using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "AnimationCurveCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "AnimationCurve",
        order = 120)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.AnimationCurve"/> items.</summary>
    public class AnimationCurveCollection : Collection<AnimationCurve>
    {
    }
}