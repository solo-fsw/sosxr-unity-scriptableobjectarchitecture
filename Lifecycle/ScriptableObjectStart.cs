using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     MonoBehaviour bridge that forwards Unity's <c>Start</c> to all <see cref="ISOStart" /> ScriptableObjects
///     currently loaded in memory. Attach to a GameObject in the first scene to give SOs a Start-equivalent.
/// </summary>

public class ScriptableObjectStart : MonoBehaviour
{
    private List<ISOStart> _sos = new List<ISOStart>();


    private void Start()
    {
        var allSOs = Resources.FindObjectsOfTypeAll<ScriptableObject>();
        _sos.Clear();

        foreach (var so in allSOs)
        {
            if (so is ISOStart isoStart)
            {
                _sos.Add(isoStart);
            }
        }

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