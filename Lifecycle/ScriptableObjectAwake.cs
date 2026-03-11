using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
///     MonoBehaviour bridge that forwards Unity's <c>Awake</c> to all <see cref="ISOAwake" /> ScriptableObjects
///     currently loaded in memory. Attach to a GameObject in the first scene to give SOs an Awake-equivalent.
/// </summary>

public class ScriptableObjectAwake : MonoBehaviour
{
    private IEnumerable<ISOAwake> _sos;


    private void Awake()
    {
        _sos = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ISOAwake>();

        foreach (var so in _sos)
        {
            so.SOAwake();
        }
    }
}


/// <summary>
///     Implement this interface on a <see cref="UnityEngine.ScriptableObject" /> to receive an <c>Awake</c>-equivalent
///     call via the <see cref="ScriptableObjectAwake" /> MonoBehaviour bridge.
/// </summary>

public interface ISOAwake
{
    /// <summary>Called once by <see cref="ScriptableObjectAwake" /> when the scene's Awake phase runs.</summary>
    public void SOAwake();
}