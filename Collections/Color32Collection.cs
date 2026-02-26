using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "Color32Collection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Structs/Color32",
        order = 120)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.Color32"/> items.</summary>
    public class Color32Collection : Collection<Color32>
    {
    }
}