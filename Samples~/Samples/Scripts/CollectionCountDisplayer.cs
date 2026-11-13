using UnityEngine;
using UnityEngine.UI;


namespace ScriptableObjectArchitecture.Examples
{
    public class CollectionCountDisplayer : MonoBehaviour
    {
        [SerializeField] private Text _textTarget;
        [SerializeField] private BaseCollection _setTarget;
        [SerializeField] private string _textFormat = "There are {0} things.";


        private int _lastCount = -1;

        private void Update()
        {
            int currentCount = _setTarget.Count;
            if (currentCount != _lastCount)
            {
                _textTarget.text = _textFormat.Replace("{0}", currentCount.ToString());
                _lastCount = currentCount;
            }
        }
    }
}
