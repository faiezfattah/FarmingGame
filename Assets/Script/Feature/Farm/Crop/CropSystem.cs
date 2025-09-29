using ObservableCollections;
using R3;
using Script.Core.Interface.Systems;
using Script.Core.Model.Crop;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;

namespace Script.Feature.Farm.Crop {
    public class CropSystem : MonoBehaviour {
        [SerializeField] Crop testingCrop;
        [SerializeField] Transform spawningPointer;
        [SerializeField] CropData debugCropData;
        [SerializeField] Core.Utils.Logger logger;
        [Inject] CropRegistry _cropRegistry;
        DisposableBag _bag;
        public Subject<(ItemData, Vector3)> OnHarvest = new();
        [Inject]
        public void Construct(ITimeSystem timeSystem) {
            _bag = new DisposableBag();
            timeSystem.DayCount.Subscribe(day => {
                UpdateCrop();
            }).AddTo(ref _bag);
        }
        public void UpdateCrop() {
            using var view = _cropRegistry.registry.CreateView(x => x);
            view.AttachFilter(x => x.SoilContext.State.CurrentValue == SoilState.Watered);
            foreach (var item in view) {
                item.Growth.Value++;
            }
        }

        private void AddCrop(CropContext cropContext) {
            _cropRegistry.registry.Add(cropContext);
        }

        private void RemoveCrop(CropContext cropContext, Vector3 itemSpawnPosition) {
            if (!_cropRegistry.registry.Remove(cropContext)) {
                Debug.LogWarning("Failed to remove crop");
                return;
            }
            OnHarvest.OnNext((cropContext.CropData.itemData, itemSpawnPosition));
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

            logger.Log($"made a new plant on: {context.DayPlanted}");

            return context;
        }
        private void OnDisable() {
            _bag.Dispose();
        }
    }
}