﻿using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEiitgr
    {
        public GtEiitgr()
        {
            GtEiitgcs = new HashSet<GtEiitgc>();
        }

        public int ItemGroup { get; set; }
        public string ItemGroupDesc { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual ICollection<GtEiitgc> GtEiitgcs { get; set; }
    }
}
