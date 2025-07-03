using System;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;
using TriInspector;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
namespace Script.Feature.Farm.Soil {
    public class Soil : MonoBehaviour, IActionable {
        [SerializeField] private MeshFilter mesh;
        [ShowInInspector, ReadOnly] private bool HasCropContext => _context?.CropPlanted != null;
        [ShowInInspector, ReadOnly] private string contextState => _context?.State.Value.ToString() ?? "no-state";
        private SoilContext _context;
        private Action<IUseable> _onAction;
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
                mesh.mesh = state switch {
                    SoilState.Initial => _context.Data.initial,
                    SoilState.Watered => _context.Data.watered,
                    SoilState.Tilled => _context.Data.tilled,
                    _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
                };

                if (_context.State.Value != SoilState.Initial) {
                    // Debug.Log("state updated: " + _context.State.Value);
                }
            });
        }
        private void OnMouseEnter() {
            IActionable.Event.OnPointerHovered.OnNext(this);
        }
        private void OnMouseExit() {
            IActionable.Event.OnPointerExited.OnNext(this);
        }
    }
}