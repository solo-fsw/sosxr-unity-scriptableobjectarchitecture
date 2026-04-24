using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     MonoBehaviour bridge that forwards Unity's <c>OnDisable</c> to all <see cref="ISOOnDisable" /> ScriptableObjects
///     currently loaded in memory. Attach to a GameObject to give SOs an OnDisable-equivalent.
/// </summary>

public class ScriptableObjectOnDisable : MonoBehaviour
{
    private List<ISOOnDisable> _sos = new List<ISOOnDisable>();


    private void Awake()
    {
        var allSOs = Resources.FindObjectsOfTypeAll<ScriptableObject>();
        _sos.Clear();

        foreach (var so in allSOs)
        {
            if (so is ISOOnDisable isoOnDisable)
            {
                _sos.Add(isoOnDisable);
            }
        }
    }


    private void OnDisable()
    {
        foreach (var so in _sos)
        {
            so.SOOnDisable();
        }
    }
}


/// <summary>
///     Implement this interface on a <see cref="UnityEngine.ScriptableObject" /> to receive an <c>OnDisable</c>-equivalent
///     call via the <see cref="ScriptableObjectOnDisable" /> MonoBehaviour bridge.
/// </summary>

public interface ISOOnDisable
{
    /// <summary>Called by <see cref="ScriptableObjectOnDisable" /> when the hosting GameObject is disabled.</summary>
    void SOOnDisable();
}