using eSya.FixedAsset.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.FixedAsset.IF
{
    public interface IAssetDepreciationRepository
    {
        Task<List<DO_ISDCodes>> GetISDCodesbyBusinesskey(int businesskey);
        Task<List<DO_ConfigFixedAssetGroup>> GetActiveFixedAssetGroup();
        Task<List<DO_ConfigFixedAssetGroup>> GetActiveFixedAssetSubGroupbyGroupId(int groupId);
        Task<List<DO_DepreciationMethod>> GetDepreciationMethodbyISDCode(int ISDCode);
        Task<DO_ReturnParameter> InsertOrUpdateDepreciationMethod(DO_DepreciationMethod obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDepreciationMethod( DO_DepreciationMethod obj);
    }
}
