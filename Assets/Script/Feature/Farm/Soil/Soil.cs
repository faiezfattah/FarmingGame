using Script.Core.Interface;
using UnityEngine;

namespace Script.Feature.Farm.Soil {
public class Soil : MonoBehaviour, ISelectable {
    public Vector3 GetPointerPosition() {
        return transform.position + Vector3.up * 0.2f;
    }
}
}