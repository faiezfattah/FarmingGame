using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Feature.Input {
public class InputProcessor : PlayerInput.IDefaultActions, IDisposable {
    private PlayerInput _input = new();

    private readonly Subject<Vector2> _moveSubject = new();
    private readonly Subject<Unit> _interactSubject = new();
    private readonly Subject<Unit> _debugSubject = new();
    private readonly Subject<Unit> _toolbarSubject = new();

    public Observable<Vector2> MoveEvent =>  _moveSubject;
    public Observable<Unit> InteractEvent => _interactSubject;
    public Observable<Unit> DebugEvent =>  _debugSubject;
    public Observable<Unit> ToolbarEvent => _toolbarSubject;
    public void OnMove(InputAction.CallbackContext context) {
        _moveSubject.OnNext(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) _interactSubject.OnNext(Unit.Default);
    }

    public void OnDebug(InputAction.CallbackContext context) {
        if (context.performed) _debugSubject.OnNext(Unit.Default);
    }

    public void OnToolbar(InputAction.CallbackContext context) {
        if (context.performed) _toolbarSubject.OnNext(Unit.Default);
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
