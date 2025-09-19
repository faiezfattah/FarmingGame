using System;
using R3;
using Script.Core.Model.Soil;

namespace Script.Core.Model.Crop {
    public class CropContext {
        public Guid Id;
        public int DayPlanted;
        public ReactiveProperty<int> Growth = new(0);
        public ReactiveProperty<int> Level = new(0);
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
    }
}