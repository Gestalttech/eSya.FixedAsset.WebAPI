using eSya.FixedAsset.DL.Entities;
using eSya.FixedAsset.DO;
using eSya.FixedAsset.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.FixedAsset.DL.Repository
{
    public class AssetGroupRepository: IAssetGroupRepository
    {
        private readonly IStringLocalizer<AssetGroupRepository> _localizer;
        public AssetGroupRepository(IStringLocalizer<AssetGroupRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Config Fixed Asset
        public async Task<List<DO_ConfigFixedAssetGroup>> GetFixedAssetGroup()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitgcs.Where(x => x.ActiveStatus && x.ItemGroup== 1)
                        .Join(db.GtEiitcts,
                         gc => gc.ItemCategory,
                         ct => ct.ItemCategory,
                         (gc, ct) => new { gc, ct })
                        .Select(r => new DO_ConfigFixedAssetGroup
                        {
                            AssetGroup=r.gc.ItemCategory,
                            AssetGroupDesc = r.ct.ItemCategoryDesc
                            
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ConfigFixedAssetGroup>> GetFixedAssetSubGroup()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEiitgcs.Where(x => x.ActiveStatus && x.ItemGroup == 1)
                        .Join(db.GtEiitcts,
                         gc => gc.ItemCategory,
                         ct => ct.ItemCategory,
                         (gc, ct) => new { gc, ct })
                        .Join(db.GtEiitscs,
                         ctc => new { ctc.gc.ItemCategory, ctc.gc.ItemSubCategory },
                         st => new { st.ItemCategory, st.ItemSubCategory },
                         (ctc, st) => new { ctc, st })
                        .GroupJoin(db.GtEcfxags,
                        e => new { e.ctc.gc.ItemCategory, e.ctc.gc.ItemSubCategory},
                        d => new { ItemCategory=d.AssetGroup, ItemSubCategory=d.AssetSubGroup },
                        (x, y) => new { x, y })
                       .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new DO_ConfigFixedAssetGroup
                         {
                             AssetGroup = a.x.ctc.gc.ItemCategory,
                             AssetSubGroup = a.x.ctc.gc.ItemSubCategory,
                             AssetGroupDesc =a.x.ctc.ct.ItemCategoryDesc,
                             AssetSubGroupDesc = a.x.st.ItemSubCategoryDesc,
                             ActiveStatus = b == null ? false : b.ActiveStatus

                         }).ToListAsync();

                    return await ds;

                 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoFixedAssetGroup(DO_ConfigFixedAssetGroup obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ast_Exists = db.GtEcfxags.Where(x => x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup ).FirstOrDefault();
                        if (ast_Exists == null)
                        {
                            var asst = new GtEcfxag
                            {
                                AssetGroup = obj.AssetGroup,
                                AssetSubGroup = obj.AssetSubGroup,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEcfxags.Add(asst);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                        else
                        {
                            ast_Exists.ActiveStatus = obj.ActiveStatus;
                            ast_Exists.ModifiedBy = obj.UserID;
                            ast_Exists.ModifiedOn = System.DateTime.Now;
                            ast_Exists.ModifiedTerminal = obj.TerminalID;
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion
    }
}
