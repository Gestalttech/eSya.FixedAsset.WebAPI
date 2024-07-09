using eSya.FixedAsset.DO;
using eSya.FixedAsset.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.FixedAsset.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssetDepreciationController : ControllerBase
    {
        private readonly IAssetDepreciationRepository _assetDepreciationRepository;
        public AssetDepreciationController(IAssetDepreciationRepository assetDepreciationRepository)
        {
            _assetDepreciationRepository = assetDepreciationRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetISDCodesbyBusinesskey(int businesskey)
        {
            var ds = await _assetDepreciationRepository.GetISDCodesbyBusinesskey(businesskey);
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveFixedAssetGroup()
        {
            var ds = await _assetDepreciationRepository.GetActiveFixedAssetGroup();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveFixedAssetSubGroupbyGroupId(int groupId)
        {
            var ds = await _assetDepreciationRepository.GetActiveFixedAssetSubGroupbyGroupId(groupId);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetDepreciationMethodbyISDCode(int ISDCode)
        {
            var ds = await _assetDepreciationRepository.GetDepreciationMethodbyISDCode(ISDCode);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDepreciationMethod(DO_DepreciationMethod obj)
        {
            var ds = await _assetDepreciationRepository.InsertOrUpdateDepreciationMethod(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> ActiveOrDeActiveDepreciationMethod( DO_DepreciationMethod obj)
        {
            var ds = await _assetDepreciationRepository.ActiveOrDeActiveDepreciationMethod(obj);
            return Ok(ds);
        }
    }
}
