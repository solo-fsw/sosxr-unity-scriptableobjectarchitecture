using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "ColorCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Structs/Color",
        order = 120)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.Color"/> items.</summary>
    public class ColorCollection : Collection<Color>
    {
    }
}