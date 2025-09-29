using System;
using OneiricFarming.Core.Utils.EventBinding;
using Script.Core.Model.Soil;
using Script.Core.Utils;

namespace Script.Core.Model.Crop {
    [Serializable]
    public class CropContext {
        public Guid Id;
        public int DayPlanted;
        public Channel<int> Growth = new(0);
        public Channel<int> Level = new(0);
        public CropData CropData;
        public SoilContext SoilContext;
        public CropContext(CropData cropData) {
            CropData = cropData;

            Id = Guid.NewGuid();
        }
        public CropContext SetSoil(SoilContext soilContext) {
            SoilContext = soilContext;
            return this;
        }
        public bool CanHarvest() =>
            Level.Value == CropData.CropLevels.Count - 1;
        public bool CanAdvance() =>
            CropData.ChanceToAdvance > UnityEngine.Random.Range(0, 1f) && SoilContext.Expect("Soil context is null?").State.Value == SoilState.Watered;
        public bool ShouldAdvanceToNextLevel(CropContext context) {
            if (context.Level.Value >= CropData.CropLevels.Count - 1)
                return false;

            var nextLevelThreshold = CropData.CropLevels[context.Level.Value + 1].GrowthLevel;
            return context.Growth.Value >= nextLevelThreshold;
        }
    }
}