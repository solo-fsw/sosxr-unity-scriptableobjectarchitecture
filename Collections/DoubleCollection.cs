using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "DoubleCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "double",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    /// <summary>ScriptableObject collection of <see cref="double"/> items.</summary>
    public class DoubleCollection : Collection<double>
    {
    }
}