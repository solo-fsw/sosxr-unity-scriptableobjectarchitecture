using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
///     MonoBehaviour bridge that forwards Unity's <c>Start</c> to all <see cref="ISOStart" /> ScriptableObjects
///     currently loaded in memory. Attach to a GameObject in the first scene to give SOs a Start-equivalent.
/// </summary>

public class ScriptableObjectStart : MonoBehaviour
{
    private IEnumerable<ISOStart> _sos;


    private void Start()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOStart>();

        foreach (var so in _sos)
        {
            so.SOStart();
        }
    }
}


/// <summary>
///     Implement this interface on a <see cref="UnityEngine.ScriptableObject" /> to receive a <c>Start</c>-equivalent
///     call via the <see cref="ScriptableObjectStart" /> MonoBehaviour bridge.
/// </summary>

public interface ISOStart
{
    /// <summary>Called once by <see cref="ScriptableObjectStart" /> when the scene's Start phase runs.</summary>
    void SOStart();
}