using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     MonoBehaviour bridge that forwards Unity's <c>Update</c> to all <see cref="ISOUpdate" /> ScriptableObjects
///     currently loaded in memory. Attach to a persistent GameObject to give SOs a per-frame Update-equivalent.
/// </summary>

public class ScriptableObjectUpdate : MonoBehaviour
{
    private List<ISOUpdate> _sos = new List<ISOUpdate>();


    private void Awake()
    {
        var allSOs = Resources.FindObjectsOfTypeAll<ScriptableObject>();
        _sos.Clear();

        foreach (var so in allSOs)
        {
            if (so is ISOUpdate isoUpdate)
            {
                _sos.Add(isoUpdate);
            }
        }
    }


    private void Update()
    {
        foreach (var so in _sos)
        {
            so.SOUpdate();
        }
    }
}


/// <summary>
///     Implement this interface on a <see cref="UnityEngine.ScriptableObject" /> to receive a per-frame <c>Update</c>-equivalent
///     call via the <see cref="ScriptableObjectUpdate" /> MonoBehaviour bridge.
/// </summary>

public interface ISOUpdate
{
    /// <summary>Called every frame by <see cref="ScriptableObjectUpdate" /> during the Update phase.</summary>
    void SOUpdate();
}