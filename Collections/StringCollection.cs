using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "StringCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "string",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 2)]
    /// <summary>ScriptableObject collection of <see cref="string"/> items.</summary>
    public class StringCollection : Collection<string>
    {
    }
}