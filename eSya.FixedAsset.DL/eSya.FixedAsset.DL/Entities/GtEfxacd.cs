using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEfxacd
    {
        public int BusinessKey { get; set; }
        public int InternalAssetNo { get; set; }
        public int IaserialNo { get; set; }
        public decimal UnitAcquisitionValue { get; set; }
        public decimal UnitAssetCost { get; set; }
        public string? EquipmentSerialNo { get; set; }
        public int AssetCondition { get; set; }
        public int AssetStatus { get; set; }
        public decimal ProvDepreciationValue { get; set; }
        public string? LastProvDeprMonthYr { get; set; }
        public decimal DepreciationValue { get; set; }
        public DateTime? LastTransferDate { get; set; }
        public decimal LastTransferValue { get; set; }
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
