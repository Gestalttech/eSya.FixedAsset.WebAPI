using eSya.FixedAsset.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.FixedAsset.IF
{
    public interface IAssetGroupRepository
    {
        #region Config Fixed Asset
        Task<List<DO_ConfigFixedAssetGroup>> GetFixedAssetGroup();
        Task<List<DO_ConfigFixedAssetGroup>> GetFixedAssetSubGroup();
        Task<DO_ReturnParameter> InsertIntoFixedAssetGroup(DO_ConfigFixedAssetGroup obj);
        #endregion
    }
}
