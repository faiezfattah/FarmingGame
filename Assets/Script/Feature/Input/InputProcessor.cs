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
    private readonly Subject<int> _numberSubject = new();
    private readonly Subject<Unit> _inventorySubject = new();
    private readonly Subject<Unit> _escapeSubject = new();

    public Observable<Vector2> MoveEvent =>  _moveSubject;
    public Observable<Unit> InteractEvent => _interactSubject;
    public Observable<Unit> DebugEvent =>  _debugSubject;
    public Observable<Unit> ToolbarEvent => _toolbarSubject;
    public Observable<int> NumberEvent => _numberSubject;
    public Observable<Unit> InventoryEvent => _inventorySubject;
    public Observable<Unit> EscapeEvent => _escapeSubject;
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

    public void OnInventory(InputAction.CallbackContext context) {
        if (context.performed) _inventorySubject.OnNext(Unit.Default);
    }
    public void OnEscape(InputAction.CallbackContext context) {
        if (context.performed) _escapeSubject.OnNext(Unit.Default);
    }
    public void On_1(InputAction.CallbackContext context) {
        if (context.performed) _numberSubject.OnNext(1);
    }

    public void On_2(InputAction.CallbackContext context) {
        if (context.performed) _numberSubject.OnNext(2);
    }

    public void On_3(InputAction.CallbackContext context) {
        if (context.performed) _numberSubject.OnNext(3);
    }

    public void On_4(InputAction.CallbackContext context) {
        if (context.performed) _numberSubject.OnNext(4);
    }

    public void On_5(InputAction.CallbackContext context) {
        if (context.performed) _numberSubject.OnNext(5);
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
