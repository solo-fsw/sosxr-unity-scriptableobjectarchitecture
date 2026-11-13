using UnityEngine;


namespace ScriptableObjectArchitecture.Examples
{
    public class ObjectAdder : MonoBehaviour
    {
        [SerializeField] private GameObjectCollection _targetCollection;


        private void OnEnable()
        {
            _targetCollection.Add(gameObject);
        }


        private void OnDisable()
        {
            _targetCollection.Remove(gameObject);
        }
    }
}