using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "UShortCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "ushort",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 18)]
    /// <summary>ScriptableObject collection of <see cref="ushort"/> items.</summary>
    public class UShortCollection : Collection<ushort>
    {
    }
}