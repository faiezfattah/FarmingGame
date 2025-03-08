using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Script.Feature.Input {
public class InputProcessor : PlayerInput.IDefaultActions, IDisposable {
    private PlayerInput _input = new();

    public Action<Vector2> MoveEvent;
    public Action InteractEvent;
    public Action DebugEvent;
    public void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) InteractEvent?.Invoke();
    }

    public void OnDebug(InputAction.CallbackContext context) {
        if (context.performed) DebugEvent?.Invoke();
    }

    public InputProcessor() {
        _input.Enable();
        _input.Default.SetCallbacks(this);
        _input.Default.Enable();
    }
    public void Dispose() {
        _input?.Disable();
    }
}
}
