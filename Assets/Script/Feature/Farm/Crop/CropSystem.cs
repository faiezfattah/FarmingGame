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
    [SerializeField] private CropData debugCropData;
    

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
        DebugSpawnCrop();
        _timeSystem.DayCount.Subscribe(_ => UpdateCrop()).AddTo(ref _bag);
    }

    private void UpdateCrop() {
        foreach (var item in _cropRegistry.CropContexts) {
            item.Growth.Value++;
        }
    }
    
    private void AddCrop(CropContext cropContext) {
        _cropRegistry.CropContexts.Add(cropContext);
    }

    private void RemoveCrop(CropContext cropContext) {
        if (!_cropRegistry.CropContexts.Remove(cropContext)) {
            Debug.LogWarning("Failed to remove crop");
        }
        _itemSystem.SpawnItem(cropContext.CropData.itemData, transform.position);
    }

    [Button]
    private void DebugSpawnCrop() {
        SpawnCrop(debugCropData);
    }
    private void SpawnCrop(CropData cropData) {
        var instance = Instantiate(testingCrop, spawningPointer.position, Quaternion.identity);
        var context = cropData.CreateContext(RemoveCrop);
        
        instance.Initialize(context);
        AddCrop(context);
        
        Debug.Log($"made a new plant on: {context.DayPlanted}");
    }

    private void OnDisable() {
        _bag.Dispose();
    }
}
}