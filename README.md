---
media_link: 
MOC:
related:
tags: []
date_created: 2024-01-01
date_modified: 2026-02-26
---

# ScriptableObject Architecture for Unity (SOSXR Fork)

- By: Maarten R. Struijk Wilbrink
- For: Leiden University SOSXR
- Fully open source: Feel free to add to, or modify, anything you see fit.

A Unity framework for decoupling gameplay systems using ScriptableObject-based variables, events, and collections. Based on Ryan Hipple's 2017 Unite talk and Daniel Everland's original package — extended and maintained here for SOSXR projects.

## Installation

### Via Package Manager (UPM) (recommended)

1. Open the Unity project you want to install this package in.
2. Open the Package Manager window.
3. Click the `+` button and select `Add package from git URL…`.
4. Paste the URL of this repo into the text field and press `Add`. Make sure it ends with `.git`.

### Via Git (to contribute)

1. Fork this repository to your own Git account, and clone it into your Unity project's `Assets` folder.
2. Make a pull request to the main repository when ready.

## Architecture

The package is built around five core abstraction layers:

| Type | Kind | Role |
|---|---|---|
| **`BaseVariable<T>`** | ScriptableObject | Holds a typed value as an asset. Supports clamping, read-only mode, a default-value fallback, and fires an on-change `UnityEvent`. |
| **`BaseReference<T, TVariable>`** | Serializable class | Inspector toggle between a local constant and a `BaseVariable` asset. Used in MonoBehaviour fields. |
| **`BaseCollection<T>`** | ScriptableObject | A typed `List<T>` stored as an asset — useful for runtime sets (e.g. all active enemies). |
| **`GameEvent` / `GameEventBase<T>`** | ScriptableObject | Typeless or typed event channel. Raised from any script; listeners react without direct coupling. Records a stack trace in debug mode. |
| **`BaseGameEventListener<T,TEvent,TResponse>`** | MonoBehaviour | Component that registers with a `GameEvent` asset and forwards the payload to a `UnityEvent` response. |

**Data flow:**

```
[Producer] ──Raise()──► GameEvent (SO)
                            └──► GameEventListener (MB) ──► UnityEvent ──► [Consumer]

[MonoBehaviour field: BaseReference<float, FloatVariable>]
    ├── UseConstant = true  →  reads local float constant
    └── UseConstant = false →  reads/writes FloatVariable (SO)
```

## Quick Start (Code)

### Declaring a Variable reference in a MonoBehaviour

```csharp
using ScriptableObjectArchitecture;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Drag a FloatVariable asset here, or tick "Use Constant" for a plain float.
    [SerializeField] private FloatReference _maxHealth;

    private void Start()
    {
        Debug.Log($"Max health: {_maxHealth.Value}");
    }
}
```

### Raising a typed Game Event in code

```csharp
using ScriptableObjectArchitecture;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private FloatGameEvent _onDamageDealt;

    public void DealDamage(float amount)
    {
        _onDamageDealt.Raise(amount);
    }
}
```

### Listening to a Game Event via component

Add a `FloatGameEventListener` component to any GameObject. In the Inspector:
- **Event** — drag the `FloatGameEvent` ScriptableObject asset.
- **Response** — wire up any `UnityEvent<float>` handler (e.g. a method on a health bar component).

No code required on the listener side.

## Features

- **Variables** — typed ScriptableObject variables for all C# primitives (`float`, `int`, `bool`, `string`, etc.) and common Unity types (`Vector2/3/4`, `Quaternion`, `Color`, `Color32`, `AnimationCurve`, `AudioClip`, `GameObject`, `LayerMask`, `SceneInfo`).
- **Clamped Variables** — numeric variables with optional min/max clamping.
- **Variable References** — constant vs. asset toggle in the Inspector, zero code change required.
- **Typed Game Events** — raise events with a payload; full editor stack trace and one-click "Raise" button in the Inspector.
- **Typed Event Listeners** — MonoBehaviour components that hook any `GameEvent` to a `UnityEvent`.
- **Runtime Collections** — typed `List<T>` assets for runtime sets.
- **Code Generator** — generates new typed Variable/Reference/Event/Listener/Collection sets from the editor window.
- **Custom Inspector icons** — ScriptableObject assets show distinct icons in the Project window.

## Usage

### Variables

1. Right-click in the Project window → `Create → ScriptableObject Architecture → Variables → float`.
2. Name the asset (e.g. `PlayerSpeed`).
3. In any MonoBehaviour field typed `FloatReference`, drag the asset in — or tick `Use Constant` to bypass the asset.

To react to value changes at runtime, use `AddListener` / `RemoveListener` on the `FloatReference`, or wire the on-change event on the variable asset itself.

### Game Events

1. Right-click → `Create → ScriptableObject Architecture → Events → float Game Event`.
2. In a producer MonoBehaviour, hold a `FloatGameEvent` reference and call `_event.Raise(value)`.
3. On a consumer GameObject, add a `FloatGameEventListener` component, assign the event asset, and wire the `Response` UnityEvent.

### Collections

1. Right-click → `Create → ScriptableObject Architecture → Collections → float`.
2. Reference the collection asset in any MonoBehaviour (`FloatCollection`).
3. Use `.Add`, `.Remove`, `.Count`, and the indexer at runtime.

### Code Generator

Open **Window → SOSXR → ScriptableObject Architecture → Code Generator**. Enter a type name and namespace, click Generate — the tool creates all necessary files (Variable, Reference, GameEvent, GameEventListener, UnityEvent, Collection) in one step.

## Samples

The `Samples~` folder contains example scenes and scripts demonstrating variables, events, and collections in practice. Import via the Package Manager samples tab.

## Credits

Based on [Ryan Hipple's 2017 Unite talk](https://www.youtube.com/watch?v=raQ3iHhE_Kk) and [Daniel Everland's ScriptableObject-Architecture](https://github.com/DanielEverland/ScriptableObject-Architecture).
