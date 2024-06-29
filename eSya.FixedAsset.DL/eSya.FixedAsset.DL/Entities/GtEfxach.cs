using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEfxach
    {
        public int BusinessKey { get; set; }
        public int InternalAssetNo { get; set; }
        public int AssetGroup { get; set; }
        public int AssetSubGroup { get; set; }
        public string AssetDescription { get; set; } = null!;
        public int Model { get; set; }
        public int Manufacturer { get; set; }
        public string? AssetSpecification { get; set; }
        public int AssetIdentification { get; set; }
        public int AssetType { get; set; }
        public int AssetAttribute { get; set; }
        public int AssetClass { get; set; }
        public int TypeOfLease { get; set; }
        public decimal AssetLifeInYears { get; set; }
        public decimal Quantity { get; set; }
        public string? SourcedFrom { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal AcquisitionValue { get; set; }
        public decimal AssetCost { get; set; }
        public DateTime? UnderWarrantyFrom { get; set; }
        public DateTime? UnderWarrantyTill { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
