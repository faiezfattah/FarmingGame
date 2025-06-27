using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Crop;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Farm.Crop {
    public class CropSystem : MonoBehaviour {
        [SerializeField] private Crop testingCrop;
        [SerializeField] private Transform spawningPointer;
        [SerializeField] private CropData debugCropData;

        [Inject]
        private CropRegistry _cropRegistry;
        [Inject]
        private ITimeSystem _timeSystem;
        [Inject]
        private IItemSystem _itemSystem;
        private DisposableBag _bag;

        private void Start() {
            _timeSystem.DayCount.Subscribe(_ => UpdateCrop()).AddTo(ref _bag);
        }

        private void UpdateCrop() {
            _cropRegistry.registry.ForEach(item => {
                if (item.CropData.CanAdvance()) {
                    item.Growth.Value++;
                }
            });
        }

        private void AddCrop(CropContext cropContext) {
            _cropRegistry.registry.Add(cropContext);
        }

        private void RemoveCrop(CropContext cropContext, Vector3 itemSpawnPosition) {
            if (!_cropRegistry.registry.Remove(cropContext)) {
                Debug.LogWarning("Failed to remove crop");
                return;
            }
            _itemSystem.SpawnItem(cropContext.CropData.itemData, itemSpawnPosition);
        }

        public CropContext SpawnCrop(CropData cropData, Vector3 position) {
            var instance = Instantiate(testingCrop, position, Quaternion.identity);
            var context = cropData.CreateContext();

            instance.Initialize(context, () => RemoveCrop(context, position));
            AddCrop(context);

            Debug.Log($"made a new plant on: {context.DayPlanted}");

            return context;
        }
        public CropContext SpawnCrop(CropContext context, Vector3 position) {
            var instance = Instantiate(testingCrop, position, Quaternion.identity, transform);

            instance.Initialize(context, () => RemoveCrop(context, position));
            AddCrop(context);

            Debug.Log($"made a new plant on: {context.DayPlanted}");

            return context;
        }
        private void OnDisable() {
            _bag.Dispose();
        }
    }
}