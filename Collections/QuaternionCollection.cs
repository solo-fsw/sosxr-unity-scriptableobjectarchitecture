using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "QuaternionCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Structs/Quaternion",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 13)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.Quaternion"/> items.</summary>
    public class QuaternionCollection : Collection<Quaternion>
    {
    }
}