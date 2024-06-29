using System;
using System.Collections.Generic;

namespace eSya.FixedAsset.DL.Entities
{
    public partial class GtEfxaal
    {
        public int BusinessKey { get; set; }
        public string AssetTag { get; set; } = null!;
        public int InternalAssetNumber { get; set; }
        public int IaserialNumber { get; set; }
        public DateTime DateAllocated { get; set; }
        public DateTime? InstallationDate { get; set; }
        public int DepartmentId { get; set; }
        public int DeptLocnId { get; set; }
        public int TransferType { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal TransferValue { get; set; }
        public string CustodianType { get; set; } = null!;
        public string? EmployeeName { get; set; }
        public string? OtherDetails { get; set; }
        public int TempDepartmentId { get; set; }
        public int TempDeptLocn { get; set; }
        public string AssetStatus { get; set; } = null!;
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
