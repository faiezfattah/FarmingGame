using System;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Crop;
using UnityEngine;

namespace Script.Feature.Farm.Crop {
public class Crop : MonoBehaviour, IEntity<CropContext>, IInteractable {
    
    [SerializeField] private SpriteRenderer sr;

    private CropData _cropData;
    private CropContext _cropContext;
    private DisposableBag _subscriptions = new();
    private Action _onHarvest;

    public void Initialize(CropContext context, Action onHarvest) {
        _cropContext = context;
        _onHarvest = onHarvest;
        _cropData = _cropContext.CropData;

        UpdateVisuals(_cropContext.Level.Value);

        _cropContext.Growth
                    .Subscribe(_ => CheckForLevelAdvancement())
                    .AddTo(ref _subscriptions);

        _cropContext.Level
                    .Subscribe(level => UpdateVisuals(level))
                    .AddTo(ref _subscriptions);
    }

    private void CheckForLevelAdvancement() {
        if (!_cropData.ShouldAdvanceToNextLevel(_cropContext)) return;
        
        _cropContext.Level.Value++;
    }

    private void UpdateVisuals(int level) {
        sr.sprite = _cropData.GetData(level)?.Sprite;
    }

    public void Interact() {
        if (_cropData.CanHarvest(_cropContext)) 
            Harvest();
        
        else Debug.Log($"Plant is growing. Level: {_cropContext.Level.Value}, Growth: {_cropContext.Growth.Value}");
    }

    private void Harvest() {
        Debug.Log("Harvested crop");
        _onHarvest?.Invoke();
        Destroy(gameObject);
    }

    private void OnDisable() {
            _subscriptions.Dispose();
    }
}
}