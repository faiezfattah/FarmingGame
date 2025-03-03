using Script.Feature.DayTime;
using Script.Registry.Crop;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Crop {
public class CropSystem : MonoBehaviour {
    [SerializeField] private Crop testingCrop;
    [SerializeField] private Transform spawningPointer;

    private CropRegistry _cropRegistry;
    private TimeSystem _timeSystem;
    [Inject]
    public void Construct(CropRegistry registry, TimeSystem timeSystem) {
        _cropRegistry = registry;
        _timeSystem = timeSystem;
    }

    private void Start() {
        SpawnCrop();
    }

    private void AddCrop(CropContext cropContext) {
        if (!_cropRegistry.TryAdd(cropContext)) {
            Debug.LogWarning("Failed to add new crop");
            return;
        }
        Debug.Log("Added new crop");
    }

    private void RemoveCrop(CropContext cropContext) {
        if (!_cropRegistry.TryRemove(cropContext)) {
            Debug.LogWarning("Failed to remove crop");
            return;
        }
        Debug.Log("Removed crop");
    }

    [Button]
    private void SpawnCrop() {
        var instance = Instantiate(testingCrop, spawningPointer.position, Quaternion.identity);
        var context = new CropContext(_timeSystem);
        instance.Initialize(context);
        AddCrop(context);
    }
}
}