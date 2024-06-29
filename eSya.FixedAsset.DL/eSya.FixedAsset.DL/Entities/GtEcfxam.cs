using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEcfxam
    {
        public int AssetGroup { get; set; }
        public int AssetSubGroup { get; set; }
        public string FixedAssetAccount { get; set; } = null!;
        public string AccDepreciationAccount { get; set; } = null!;
        public string DepreciationAccount { get; set; } = null!;
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
