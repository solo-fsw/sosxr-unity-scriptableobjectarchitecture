using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "ObjectCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Object",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 1)]
    /// <summary>ScriptableObject collection of <see cref="Object"/> items.</summary>
    public class ObjectCollection : Collection<Object>
    {
    }
}