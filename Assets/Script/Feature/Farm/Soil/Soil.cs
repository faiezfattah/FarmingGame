using System;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;
using TriInspector;
namespace Script.Feature.Farm.Soil {
public class Soil : MonoBehaviour, IActionable {
    [SerializeField] private MeshFilter mesh;
    [ShowInInspector, ReadOnly] private bool HasCropContext => _context.CropPlanted != null;
    private SoilContext _context;
    private Action<IUseable> _onAction;
    private IToolbarRegistry _toolbar;
    [Inject] public void Construct(IToolbarRegistry tool) { // TODO: remove this 
        _toolbar = tool;
    }
    public Vector3 GetPointerPosition() {
        return transform.position + Vector3.up * 0.2f;
    }
    private void Start() {
        mesh = GetComponent<MeshFilter>();
    }
    
    public void Action(IUseable useable) {
        _onAction?.Invoke(useable);
    }

    public void SetContext(SoilContext context, Action<IUseable> onSelect) {
        _context = context;
        _onAction = onSelect;

         _context.State.Subscribe(state => {
            switch (state) {
                case SoilState.Initial:
                    mesh.mesh = _context.Data.initial;
                    break;
                case SoilState.Tilled:
                    mesh.mesh = _context.Data.tilled;
                    break;
                case SoilState.Watered:
                    // mesh.mesh = _context.Data.watered;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            
            if (_context.State.Value != SoilState.Initial) {
                Debug.Log("state updated: " + _context.State.Value);
            }
        });
    }
}
}