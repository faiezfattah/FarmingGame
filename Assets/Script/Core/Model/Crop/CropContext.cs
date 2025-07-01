using System;
using R3;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;

namespace Script.Core.Model.Crop {
    public class CropContext {
        public Guid Id;
        public int DayPlanted;
        public ReactiveProperty<int> Growth;
        public ReactiveProperty<int> Level;
        public CropData CropData;
        public SoilContext SoilContext;
        public CropContext(CropData cropData) {
            CropData = cropData;

            Id = Guid.NewGuid();
            Growth = new ReactiveProperty<int>(0);
            Level = new ReactiveProperty<int>(0);
        }
        public CropContext SetSoil(SoilContext soilContext) {
            SoilContext = soilContext;
            return this;
        }
    }
}