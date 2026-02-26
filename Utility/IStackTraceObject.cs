using System.Collections.Generic;


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Implemented by any SO Architecture object that can record raise/notify stack traces
    ///     for editor-time debugging (both <see cref="GameEventBase" /> and <see cref="DebuggableGameEventListener" />).
    /// </summary>

    public interface IStackTraceObject
    {
        /// <summary>Ordered list of <see cref="StackTraceEntry" /> objects recorded each time the event was raised or the listener notified (most recent first).</summary>
        List<StackTraceEntry> StackTraces { get; }


        /// <summary>Records a parameterless stack trace entry. Only active when editor debug mode is enabled.</summary>
        void AddStackTrace();


        /// <summary>Records a stack trace entry that includes the raised <paramref name="value" />. Only active when editor debug mode is enabled.</summary>
        void AddStackTrace(object value);
    }
}