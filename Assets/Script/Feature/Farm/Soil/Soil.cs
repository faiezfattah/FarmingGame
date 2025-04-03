using System;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;

namespace Script.Feature.Farm.Soil {
public class Soil : MonoBehaviour, ISelectable {
    [SerializeField] private MeshFilter mesh;
    private SoilContext _context;
    private Action _onSelect;
    public Vector3 GetPointerPosition() {
        return transform.position + Vector3.up * 0.2f;
    }
    private void Start() {
        mesh = GetComponent<MeshFilter>();
    }
    public void Select() {
        // mesh.mesh = _context.Data.tilled;
        _onSelect?.Invoke();
    }

    public void SetContext(SoilContext context, Action onSelect) {
        _context = context;
        _onSelect = onSelect;

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
                case SoilState.Planted:
                    // mesh.mesh = _context.Data.planted;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        });
    }
}
}