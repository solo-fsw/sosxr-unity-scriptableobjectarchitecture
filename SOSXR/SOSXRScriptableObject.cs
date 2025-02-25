using UnityEngine;


/// <summary>
///     The names for the ScriptableObject lifecycle methods are terrible, since they are the same as MonoBehaviour, but
///     are called at different times.
///     When deriving from this class, you can use these methods instead of the default ones.
///     They are still called at the same time as the default ones, but the naming makes it clear that these are not
///     happening when the MonoBehaviour methods are called.
///     Documentation: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/ScriptableObject.html
/// </summary>
public abstract class SOSXRScriptableObject : ScriptableObject
{
    /// <summary>
    ///     Don't use this method.
    ///     This calls OnCreation.
    /// </summary>
    private void Awake()
    {
        OnCreated();
    }


    /// <summary>
    ///     Better name for Awake
    ///     Called when an instance of ScriptableObject is created.
    /// </summary>
    protected virtual void OnCreated()
    {
        Debug.LogFormat(this, "{0} called OnCreated", name);
    }


    /// <summary>
    ///     Don't use this method.
    ///     This calls OnLoaded.
    /// </summary>
    private void OnEnable()
    {
        OnLoaded();
    }


    /// <summary>
    ///     A better name for OnEnable
    ///     This function is called when the object is loaded.
    /// </summary>
    protected virtual void OnLoaded()
    {
        Debug.LogFormat(this, "{0} called OnLoaded", name);
    }


    /// <summary>
    ///     Don't use this method.
    ///     This calls OnOutOfScope.
    /// </summary>
    private void OnDisable()
    {
        OnOutOfScope();
    }


    /// <summary>
    ///     A better name for OnDisable
    ///     This function is called when the scriptable object goes out of scope.
    /// </summary>
    protected virtual void OnOutOfScope()
    {
        Debug.LogFormat(this, "{0} called OnOutOfScope", name);
    }


    /// <summary>
    ///     Don't use this method.
    ///     This calls OnScriptableObjectDestroy.
    /// </summary>
    private void OnDestroy()
    {
        OnScriptableObjectDestroyed();
    }


    /// <summary>
    ///     Better name for OnDestroy
    ///     This function is called when the scriptable object will be destroyed.
    /// </summary>
    protected virtual void OnScriptableObjectDestroyed()
    {
        Debug.LogFormat(this, "{0} called OnScriptableObjectDestroyed", name);
    }
}

[CreateAssetMenu(menuName = "SOSXR/Test")]
public class Test : SOSXRScriptableObject
{
    
}