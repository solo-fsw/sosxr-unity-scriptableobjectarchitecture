using UnityEngine;
using UnityEngine.UI;


namespace ScriptableObjectArchitecture.Examples
{
    public class CollectionCountDisplayer : MonoBehaviour
    {
        [SerializeField] private Text _textTarget;
        [SerializeField] private BaseCollection _setTarget;
        [SerializeField] private string _textFormat = "There are {0} things.";


        private void Update()
        {
            _textTarget.text = string.Format(_textFormat, _setTarget.Count);
        }
    }
}