using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;

namespace Script.Feature.Farm.Soil {
public class Soil : MonoBehaviour, ISelectable {
    [SerializeField] private MeshFilter mesh;
    private SoilContext _context;
    public Vector3 GetPointerPosition() {
        return transform.position + Vector3.up * 0.2f;
    }
    private void Start() {
        mesh = GetComponent<MeshFilter>();
    }
        public void Select() {
        mesh.mesh = _context.Data.tilled;
    }

    public void SetContext(SoilContext context) {
        _context = context;
    }
}
}