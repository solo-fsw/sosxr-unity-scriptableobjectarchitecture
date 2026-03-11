using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "SByteCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "sbyte",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    /// <summary>ScriptableObject collection of <see cref="sbyte"/> items.</summary>
    public class SByteCollection : Collection<sbyte>
    {
    }
}