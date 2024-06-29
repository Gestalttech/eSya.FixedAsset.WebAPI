﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.FixedAsset.DO
{
    public class DO_ConfigFixedAssetGroup
    {
        public int AssetGroup { get; set; }
        public int AssetSubGroup { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string? AssetGroupDesc { get; set; }
        public string? AssetSubGroupDesc { get; set; }
    }
}
