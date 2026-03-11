using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "Vector3Collection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Structs/Vector3",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 11)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.Vector3"/> items.</summary>
    public class Vector3Collection : Collection<Vector3>
    {
    }
}