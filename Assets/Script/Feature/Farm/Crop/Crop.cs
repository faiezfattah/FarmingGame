using System;
using OneiricFarming.Core.Utils.EventBinding;
using Script.Core.Interface;
using Script.Core.Model.Crop;
using Script.Core.Utils;
using TriInspector;
using UnityEngine;

namespace Script.Feature.Farm.Crop {
public class Crop : MonoBehaviour, IEntity<CropContext>, IInteractable {

    [SerializeField] private SpriteRenderer sr;

    [SerializeField, ReadOnly] private CropContext _cropContext;
    private SubscriptionBag _subscriptions = new();
    private Action _onHarvest;

    public void Initialize(CropContext context, Action onHarvest) {
        _cropContext = context.Expect("CropContext is null");
        _onHarvest = onHarvest;

        UpdateVisuals(_cropContext.Level.Value);

        _cropContext.Growth
                    .Subscribe(CheckForLevelAdvancement)
                    .AddTo(ref _subscriptions);

        _cropContext.Level
                    .Subscribe(UpdateVisuals)
                    .AddTo(ref _subscriptions);
    }

    private void CheckForLevelAdvancement(int level) {
        if (!_cropContext.CanAdvance()) return;
        
        _cropContext.Level.Value++;
    }

    private void UpdateVisuals(int level) {
        sr.sprite = _cropContext.CropData.GetData(level)?.Sprite;
    }

    public void Interact() {
        if (_cropContext.CanHarvest()) 
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