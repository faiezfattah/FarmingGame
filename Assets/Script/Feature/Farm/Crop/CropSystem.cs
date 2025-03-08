using R3;
using Script.Core.Interface;
using Script.Core.Model.Crop;
using Script.Feature.DayTime;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Farm.Crop {
public class CropSystem : MonoBehaviour {
    [SerializeField] private Crop testingCrop;
    [SerializeField] private Transform spawningPointer;
    

    private CropRegistry _cropRegistry;
    private TimeSystem _timeSystem;
    private IItemSystem _itemSystem;
    private DisposableBag _bag;
    [Inject]
    public void Construct(CropRegistry registry, TimeSystem timeSystem, IItemSystem itemSystem) {
        _cropRegistry = registry;
        _timeSystem = timeSystem;
        _itemSystem = itemSystem;
    }

    private void Start() {
        SpawnCrop();
        _timeSystem.DayCount.Subscribe(_ => UpdateCrop()).AddTo(ref _bag);
    }

    private void UpdateCrop() {
        foreach (var item in _cropRegistry.cropContexts) {
            item.Growth.Value++;
        }
    }
    
    private void AddCrop(CropContext cropContext) {
        if (!_cropRegistry.TryAdd(cropContext)) {
            Debug.LogWarning("Failed to add new crop");
        }
        // Debug.Log("Added new crop");
    }

    private void RemoveCrop(CropContext cropContext) {
        if (!_cropRegistry.TryRemove(cropContext)) {
            Debug.LogWarning("Failed to remove crop");
        }
        _itemSystem.SpawnItem(cropContext.CropItem, transform.position);
    }

    [Button]
    private void SpawnCrop() {
        var instance = Instantiate(testingCrop, spawningPointer.position, Quaternion.identity);
        var context = new CropContext(RemoveCrop);
        
        instance.Initialize(context);
        AddCrop(context);
        
        Debug.Log($"made a new plant on: {context.DayPlanted}");

        // var registry = _cropRegistry.GetAll();
        // foreach (var item in registry) {
        //     Debug.Log(item);
        // }
    }

    private void OnDisable() {
        _bag.Dispose();
    }
}
}