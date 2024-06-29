using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEfxapd
    {
        public int BusinessKey { get; set; }
        public int InternalAssetNo { get; set; }
        public int VendorId { get; set; }
        public int Ponumber { get; set; }
        public DateTime Podate { get; set; }
        public int Grnnumber { get; set; }
        public DateTime Grndate { get; set; }
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
