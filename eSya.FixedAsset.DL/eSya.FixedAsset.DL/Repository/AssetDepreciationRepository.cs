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
    public class AssetDepreciationRepository: IAssetDepreciationRepository
    {
        private readonly IStringLocalizer<AssetDepreciationRepository> _localizer;
        public AssetDepreciationRepository(IStringLocalizer<AssetDepreciationRepository> localizer)
        {
            _localizer = localizer;
        }
        #region
        public async Task<List<DO_ISDCodes>> GetISDCodesbyBusinesskey(int businesskey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEcbslns
                        .Join(db.GtEccncds,
                         b => b.Isdcode,
                         i => i.Isdcode,
                         (b, i) => new { b, i }).
                         Where(x=>x.b.ActiveStatus && x.b.BusinessKey==businesskey && x.i.ActiveStatus)
                        .Select(r => new DO_ISDCodes
                        {
                            Isdcode = r.b.Isdcode,
                            CountryName=r.i.CountryName,
                            CountryFlag=r.i.CountryFlag,
                            CountryCode=r.i.CountryCode,

                        }).ToListAsync();

                    var distIsdcodes = ds.GroupBy(p => p.Isdcode)
                           .Select(g => g.First())
                           .ToList();
                    return distIsdcodes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ConfigFixedAssetGroup>> GetActiveFixedAssetGroup()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEcfxags.Where(x => x.ActiveStatus)
                        .Join(db.GtEiitcts,
                         gc => gc.AssetGroup,
                         ct => ct.ItemCategory,
                         (gc, ct) => new { gc, ct })
                        .Select(r => new DO_ConfigFixedAssetGroup
                        {
                            AssetGroup = r.gc.AssetGroup,
                            AssetGroupDesc = r.ct.ItemCategoryDesc

                        }).ToListAsync();

                    var distastgroups = ds.GroupBy(p => p.AssetGroup)
                           .Select(g => g.First())
                           .ToList();
                    return distastgroups;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ConfigFixedAssetGroup>> GetActiveFixedAssetSubGroupbyGroupId(int groupId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEcfxags.Where(x => x.ActiveStatus && x.AssetGroup==groupId)
                       .Join(db.GtEiitscs,
                         gc => gc.AssetSubGroup,
                         ct => ct.ItemSubCategory,
                         (gc, ct) => new { gc, ct })

                       .Select(r => new DO_ConfigFixedAssetGroup
                       {
                           AssetSubGroup = r.gc.AssetSubGroup,
                           AssetSubGroupDesc = r.ct.ItemSubCategoryDesc

                       }).ToListAsync();

                    var distsubgroups = ds.GroupBy(p => p.AssetSubGroup)
                           .Select(g => g.First())
                           .ToList();
                    return distsubgroups;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DepreciationMethod>> GetDepreciationMethodbyISDCode(int ISDCode)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEcfxdms.Where(x => x.Isdcode== ISDCode)
                       .Join(db.GtEiitcts,
                         gc => gc.AssetGroup,
                         ct => ct.ItemCategory,
                         (gc, ct) => new { gc, ct })
                        .Join(db.GtEiitscs,
                         ctc => new { ctc.gc.AssetGroup, ctc.gc.AssetSubGroup },
                         st => new { AssetGroup= st.ItemCategory, AssetSubGroup= st.ItemSubCategory },
                         (ctc, st) => new { ctc, st })
                        .Join(db.GtEcapcds,
                         ad => new { ad.ctc.gc.DepreciationMethod },
                         a => new { DepreciationMethod = a.ApplicationCode},
                         (ad, a) => new { ad, a }).
                       Select(x=> new DO_DepreciationMethod
                       {
                          
                            AssetGroup =x.ad.ctc.gc.AssetGroup,
                            AssetSubGroup = x.ad.ctc.gc.AssetSubGroup,
                            EffectiveFrom = x.ad.ctc.gc.EffectiveFrom,
                            DepreciationMethod = x.ad.ctc.gc.DepreciationMethod,
                            DepreciationPercentage = x.ad.ctc.gc.DepreciationPercentage,
                            UsefulYears = x.ad.ctc.gc.UsefulYears,
                            EffectiveTill = x.ad.ctc.gc.EffectiveTill,
                            AssetGroupDesc =x.ad.ctc.ct.ItemCategoryDesc,
                            AssetSubGroupDesc =x.ad.st.ItemSubCategoryDesc, 
                            DepreciationMethodDesc=x.a.CodeDesc,
                            ActiveStatus = x.ad.ctc.gc.ActiveStatus

                        }).ToListAsync();
                   
                    return ds;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateDepreciationMethod(DO_DepreciationMethod obj)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                       
                        var dmethodExist = db.GtEcfxdms.Where(w => w.Isdcode == obj.Isdcode && w.AssetGroup == obj.AssetGroup && w.AssetSubGroup==obj.AssetSubGroup &&
                            w.DepreciationMethod == obj.DepreciationMethod && w.EffectiveTill == null).FirstOrDefault();
                            if (dmethodExist != null)
                            {
                                if (obj.EffectiveFrom != dmethodExist.EffectiveFrom)
                                {
                                    if (obj.EffectiveFrom < dmethodExist.EffectiveFrom)
                                    {
                                        return new DO_ReturnParameter() { Status = false, StatusCode = "W0148", Message = string.Format(_localizer[name: "W0148"]) };
                                    }
                                    dmethodExist.EffectiveTill = obj.EffectiveFrom.AddDays(-1);
                                    dmethodExist.ModifiedBy = obj.UserID;
                                    dmethodExist.ModifiedOn = System.DateTime.Now;
                                    dmethodExist.ModifiedTerminal = obj.TerminalID;
                                    dmethodExist.ActiveStatus = false;

                                    var dm = new GtEcfxdm
                                    {
                                        Isdcode = obj.Isdcode,
                                        AssetGroup = obj.AssetGroup,
                                        AssetSubGroup = obj.AssetSubGroup,
                                        DepreciationMethod = obj.DepreciationMethod,
                                        EffectiveFrom = obj.EffectiveFrom,
                                        DepreciationPercentage = obj.DepreciationPercentage,
                                        UsefulYears = obj.UsefulYears,
                                        ActiveStatus = obj.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };
                                    db.GtEcfxdms.Add(dm);
                                

                            }
                            else
                                {
                                      dmethodExist.DepreciationPercentage = obj.DepreciationPercentage;
                                      dmethodExist.UsefulYears = obj.UsefulYears;
                                      dmethodExist.ActiveStatus = obj.ActiveStatus;
                                      dmethodExist.ModifiedBy = obj.UserID;
                                      dmethodExist.ModifiedOn = System.DateTime.Now;
                                      dmethodExist.ModifiedTerminal = obj.TerminalID;
                                }

                            }

                        else
                        {

                            var dmm = new GtEcfxdm
                                    {
                                        Isdcode = obj.Isdcode,
                                        AssetGroup = obj.AssetGroup,
                                        AssetSubGroup = obj.AssetSubGroup,
                                        DepreciationMethod = obj.DepreciationMethod,
                                        EffectiveFrom = obj.EffectiveFrom,
                                        DepreciationPercentage = obj.DepreciationPercentage,
                                        UsefulYears = obj.UsefulYears,
                                       // EffectiveTill = obj.EffectiveTill,
                                        ActiveStatus = obj.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn =System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };
                                    db.GtEcfxdms.Add(dmm);
                                

                            }
                        
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveDepreciationMethod( DO_DepreciationMethod obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEcfxdm dm = db.GtEcfxdms.Where(x => x.Isdcode == obj.Isdcode && x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup && x.DepreciationMethod == obj.DepreciationMethod && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).FirstOrDefault();

                        if (dm == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0001", Message = string.Format(_localizer[name: "W0001"]) };
                        }

                        dm.ActiveStatus = obj.status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (obj.status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
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
        
        #region
        //public async Task<DO_ReturnParameter> InsertOrUpdateDepreciationMethod(DO_DepreciationMethod obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {

        //                    GtEcfxdm amdm;

        //                    amdm = db.GtEcfxdms.Where(x => x.Isdcode == obj.Isdcode && x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup && x.DepreciationMethod == obj.DepreciationMethod && x.EffectiveTill == null).FirstOrDefault();

        //                    if (amdm == null)
        //                    {

        //                            amdm = new GtEcfxdm
        //                            {
        //                                Isdcode = obj.Isdcode,
        //                                AssetGroup = obj.AssetGroup,
        //                                AssetSubGroup = obj.AssetSubGroup,
        //                                DepreciationMethod = obj.DepreciationMethod,
        //                                EffectiveFrom = obj.EffectiveFrom,
        //                                EffectiveTill = obj.EffectiveTill,
        //                                DepreciationPercentage = obj.DepreciationPercentage,
        //                                UsefulYears = obj.UsefulYears,
        //                                ActiveStatus = obj.ActiveStatus,
        //                                FormId = obj.FormID,
        //                                CreatedBy = obj.UserID,
        //                                CreatedOn = System.DateTime.Now,
        //                                CreatedTerminal = obj.TerminalID
        //                            };
        //                            db.GtEcfxdms.Add(amdm);

        //                    }
        //                    else
        //                    {
        //                        amdm = db.GtEcfxdms.Where(x => x.Isdcode == obj.Isdcode && x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup && x.DepreciationMethod == obj.DepreciationMethod && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).FirstOrDefault();

        //                        if (amdm == null)
        //                        {
        //                        GtEcfxdm _adm = db.GtEcfxdms.Where(x => x.Isdcode == obj.Isdcode && x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup && x.DepreciationMethod == obj.DepreciationMethod && x.EffectiveTill == null).First();

        //                            _adm.EffectiveTill = obj.EffectiveFrom.AddDays(-1);
        //                            _adm.ModifiedBy = obj.UserID;
        //                            _adm.ModifiedOn = System.DateTime.Now;
        //                            _adm.ModifiedTerminal = obj.TerminalID;

        //                            amdm = new GtEcfxdm
        //                            {
        //                                Isdcode = obj.Isdcode,
        //                                AssetGroup = obj.AssetGroup,
        //                                AssetSubGroup = obj.AssetSubGroup,
        //                                DepreciationMethod = obj.DepreciationMethod,
        //                                EffectiveFrom = obj.EffectiveFrom,
        //                                EffectiveTill = obj.EffectiveTill,
        //                                DepreciationPercentage = obj.DepreciationPercentage,
        //                                ActiveStatus = obj.ActiveStatus,
        //                                UsefulYears = obj.UsefulYears,
        //                                FormId = obj.FormID,
        //                                CreatedBy = obj.UserID,
        //                                CreatedOn = System.DateTime.Now,
        //                                CreatedTerminal = obj.TerminalID
        //                            };
        //                            db.GtEcfxdms.Add(amdm);
        //                        }
        //                        else
        //                        {
        //                            amdm = db.GtEcfxdms.Where(x => x.Isdcode == obj.Isdcode && x.AssetGroup == obj.AssetGroup && x.AssetSubGroup == obj.AssetSubGroup && x.DepreciationMethod == obj.DepreciationMethod && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).First();

        //                            amdm.DepreciationPercentage = obj.DepreciationPercentage;
        //                            amdm.UsefulYears = obj.UsefulYears;
        //                            amdm.ActiveStatus = obj.ActiveStatus;
        //                            amdm.ModifiedBy = obj.UserID;
        //                            amdm.ModifiedOn = System.DateTime.Now;
        //                            amdm.ModifiedTerminal = obj.TerminalID;

        //                        }
        //                    }

        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, Message = "Depreciation Methods Created Successfully." };
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        //public async Task<DO_ReturnParameter> InsertOrUpdateDepreciationMethod(DO_DepreciationMethod obj)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (obj.isEdit == 0)
        //                {
        //                    GtEcfxdm is_effefdateExist = db.GtEcfxdms.FirstOrDefault(be => be.Isdcode == obj.Isdcode && be.AssetGroup == obj.AssetGroup && be.AssetSubGroup==obj.AssetSubGroup &&
        //                    be.DepreciationMethod==obj.DepreciationMethod && be.EffectiveFrom >= obj.EffectiveFrom);
        //                    if (is_effefdateExist != null)
        //                    {
        //                        return new DO_ReturnParameter() { Status = false, StatusCode = "W00114", Message = string.Format(_localizer[name: "W00114"]) };
        //                    }

        //                    GtEcfxdm is_effetodateExist = db.GtEcfxdms.FirstOrDefault(be => be.Isdcode == obj.Isdcode && be.AssetGroup == obj.AssetGroup
        //                    && be.AssetGroup==obj.AssetGroup && be.DepreciationMethod==obj.DepreciationMethod && be.EffectiveTill >= obj.EffectiveTill);
        //                    if (is_effetodateExist != null)
        //                    {
        //                        return new DO_ReturnParameter() { Status = false, StatusCode = "W00115", Message = string.Format(_localizer[name: "W00115"]) };

        //                    }

        //                    var is_SubsCheck = db.GtEcfxdms.FirstOrDefault(be => be.Isdcode == obj.Isdcode && be.AssetGroup == obj.AssetGroup
        //                    && be.AssetSubGroup==obj.AssetSubGroup && be.DepreciationMethod==obj.DepreciationMethod && (be.EffectiveTill >= obj.EffectiveFrom || obj.EffectiveTill >= obj.EffectiveFrom));
        //                    if (is_SubsCheck != null)
        //                    {
        //                        return new DO_ReturnParameter() { Status = false, StatusCode = "W00116", Message = string.Format(_localizer[name: "W00116"]) };

        //                    }
        //                }
        //                GtEcfxdm b_Susbs = db.GtEcfxdms.Where(be => be.Isdcode == obj.Isdcode && be.AssetGroup == obj.AssetGroup
        //                && be.AssetSubGroup==obj.AssetSubGroup && be.DepreciationMethod==obj.DepreciationMethod && be.EffectiveFrom == obj.EffectiveFrom).FirstOrDefault();

        //                if (b_Susbs != null)
        //                {
        //                    b_Susbs.EffectiveTill = obj.EffectiveTill;
        //                    b_Susbs.DepreciationPercentage = obj.DepreciationPercentage;
        //                    b_Susbs.UsefulYears = obj.UsefulYears;
        //                    b_Susbs.ActiveStatus = obj.ActiveStatus;
        //                    b_Susbs.ModifiedBy = obj.UserID;
        //                    b_Susbs.ModifiedOn = System.DateTime.Now;
        //                    b_Susbs.ModifiedTerminal = obj.TerminalID;
        //                    await db.SaveChangesAsync();
        //                    dbContext.Commit();
        //                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
        //                }
        //                else
        //                {
        //                    var b_Subs = new GtEcfxdm
        //                    {
        //                        Isdcode = obj.Isdcode,
        //                        AssetGroup = obj.AssetGroup,
        //                        AssetSubGroup=obj.AssetSubGroup,
        //                        DepreciationMethod=obj.DepreciationMethod,
        //                        EffectiveFrom = obj.EffectiveFrom,
        //                        EffectiveTill = obj.EffectiveTill,
        //                        DepreciationPercentage = obj.DepreciationPercentage,
        //                        UsefulYears = obj.UsefulYears,
        //                        ActiveStatus = obj.ActiveStatus,
        //                        FormId = obj.FormID,
        //                        CreatedBy = obj.UserID,
        //                        CreatedOn = System.DateTime.Now,
        //                        CreatedTerminal = obj.TerminalID
        //                    };

        //                    db.GtEcfxdms.Add(b_Subs);
        //                    await db.SaveChangesAsync();
        //                    dbContext.Commit();
        //                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
        //                }


        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                dbContext.Rollback();
        //                throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
        #endregion

        #endregion
    }
}
