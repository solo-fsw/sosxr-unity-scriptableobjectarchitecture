using UnityEngine;


namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "GameObjectCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "GameObject",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    /// <summary>ScriptableObject collection of <see cref="UnityEngine.GameObject"/> items.</summary>
    public class GameObjectCollection : Collection<GameObject>
    {
    }
}