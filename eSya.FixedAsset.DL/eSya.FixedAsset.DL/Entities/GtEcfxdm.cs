using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEcfxdm
    {
        public int Isdcode { get; set; }
        public int AssetGroup { get; set; }
        public int AssetSubGroup { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public int DepreciationMethod { get; set; }
        public decimal DepreciationPercentage { get; set; }
        public int UsefulYears { get; set; }
        public DateTime? EffectiveTill { get; set; }
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
