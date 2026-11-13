# Changelog

All notable changes to this project will be documented in this file. The changelog format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

For earlier versions, please refer to the [original CHANGELOG](https://github.com/DanielEverland/ScriptableObject-Architecture/releases)

## [Unreleased]

### Fixed

- Unity 6.6 analyzer warning UAC0020: the property drawer scan no longer calls `Assembly.LoadFrom` on `Library/ScriptAssemblies`. It reads the assemblies the Editor already loaded. `LoadFrom` also loaded a second copy of each assembly.
- Unity 6.6 serialization analyzer warning UAC1002: non-generic `BaseReference` is now `[Serializable]`. It has no fields, so serialized data is unchanged.

## [0.4.0] - 2026-11-13

### Fixed

- Player builds failed on an unguarded `using UnityEditor;` in `Utility/SOArchitectureBaseObject.cs`. `Variables/SceneVariable.cs` now guards its editor code with `#if UNITY_EDITOR`.
- `GameEventBase.Raise` threw `ArgumentOutOfRangeException` when a response removed two or more listeners. Raise now iterates a pooled snapshot.
- A listener removed during a raise is skipped. A listener added during a raise runs from the next raise.
- Typed `RemoveAll` now also clears the typed listener and action lists.
- Listeners recorded a stack trace on every raise, so memory grew without bound. Stack traces are now recorded only in editor debug mode, capped by `StackTraceEntry.MaxEntries` (100).
- Gizmo debug entries are gated and capped the same way.

### Changed

- `package.json`: Unity `2021.3` (was `2019.2`), adds dependency `com.unity.ugui` 1.0.0.
- `Samples~/` renamed to visible `Samples/`. Sample path is `Samples/Samples`.
- Runtime asmdef references `UnityEngine.UI`.
- Samples asmdef references `UnityEngine.UI` and defines `SOA_SAMPLES_INPUT_SYSTEM` when `com.unity.inputsystem` is installed. `KeyboardMover` uses the new Input System only under that define.

### Added

- EditMode tests in `Tests/Editor/` (asmdef `ScriptableObject-Architecture.Tests`): `GameEventRaiseTests`, `StackTraceTests`, `PackageLayoutTests`.

## [0.2.1] - In progress

### Package Numbering Change

> #### Package will now be numbered starting with 0, to better reflect the current status in development (see the official semver information [here](https://semver.org/#spec-item-4)).
>
> If any issues arise when updating from previous (and higher numbered versions), please delete the old version before updating to this version.

### Added

- SOSXRScriptableObject base class to inherit from because lifecycle method names were garbage

## [2.0.0] - 2025-02-03

### Changed

- Added MIT license since none was present

## [1.8.0] - 2025-01-31

### Changed

- Rider cleanup of code
- Updated to URP support
- Updated to Unity 'New' Input System

## [1.7.4] - 2025-01-31

### Changed

- Changed regular folder 'Examples' as a separate thing: download them as samples through the 'Samples' button in the
  package manager