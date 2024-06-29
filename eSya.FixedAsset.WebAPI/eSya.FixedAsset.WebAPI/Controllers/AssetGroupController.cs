using eSya.FixedAsset.DL.Repository;
using eSya.FixedAsset.DO;
using eSya.FixedAsset.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.FixedAsset.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssetGroupController : ControllerBase
    {
        private readonly IAssetGroupRepository _assetGroupRepository;
        public AssetGroupController(IAssetGroupRepository assetGroupRepository)
        {
            _assetGroupRepository = assetGroupRepository;
        }

        #region Config Fixed Asset
        [HttpGet]
        public async Task<IActionResult> GetFixedAssetGroup()
        {
            var ds = await _assetGroupRepository.GetFixedAssetGroup();
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetFixedAssetSubGroup()
        {
            var ds = await _assetGroupRepository.GetFixedAssetSubGroup();
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoFixedAssetGroup(DO_ConfigFixedAssetGroup obj)
        {
            var ds = await _assetGroupRepository.InsertIntoFixedAssetGroup(obj);
            return Ok(ds);
        }

        #endregion
    }
}
