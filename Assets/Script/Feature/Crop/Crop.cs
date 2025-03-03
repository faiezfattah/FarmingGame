using R3;
using Script.Core;
using Script.Feature.DayTime;
using Script.Registry.Crop;
using UnityEngine;
using VContainer;

namespace Script.Feature.Crop {
public class Crop : MonoBehaviour, IInteractable {
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CropData cropData;
    
    private CropContext _cropContext;
    private TimeSystem _timeSystem;
    private DisposableBag _subscriptions = new();

    public void Initialize(CropContext cropContext) {
        _cropContext = cropContext;

        UpdateVisuals(_cropContext.Level.Value);

        _cropContext.Growth
                    .Subscribe(_ => CheckForLevelAdvancement())
                    .AddTo(ref _subscriptions);

        _cropContext.Level
                    .Subscribe(level => UpdateVisuals(level))
                    .AddTo(ref _subscriptions);
    }

    private void CheckForLevelAdvancement() {
        if (!cropData.ShouldAdvanceToNextLevel(_cropContext)) return;
        
        _cropContext.Level.Value++;
    }

    private void UpdateVisuals(int level) {
        sr.sprite = cropData.GetData(level)?.Sprite;
    }

    public void Interact() {
        if (cropData.CanHarvest(_cropContext.Level.Value)) 
            Harvest();
        
        else Debug.Log($"Plant is growing. Level: {_cropContext.Level.Value}, Growth: {_cropContext.Growth.Value}");
    }

    private void Harvest() {
        Debug.Log($"Harvested crop with value: {cropData.price}");
        Destroy(gameObject);
    }

    private void OnDestroy() {
        _subscriptions.Dispose();
        _cropContext?.Dispose();
    }
}
}