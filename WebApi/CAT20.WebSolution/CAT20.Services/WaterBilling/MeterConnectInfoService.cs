using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.Vote;
using CAT20.Core.Models.WaterBilling;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.WaterBilling;
using Irony.Parsing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.WaterBilling
{
    public class MeterConnectInfoService : IMeterConnectInfoService
    {
        private readonly IWaterBillingUnitOfWork _wb_unitOfWork;
        private readonly IOfficeService _officeService;

        public MeterConnectInfoService(IWaterBillingUnitOfWork wb_unitOfWork,IOfficeService officeService)
        {
            _wb_unitOfWork = wb_unitOfWork;
            _officeService = officeService;
        }

        public async Task<(bool,string?,MeterConnectInfo)> Create(MeterConnectInfo meterConnectInfo, HTokenClaim token, int keyPattern)
        {
            try
            {
                var offIces = await _officeService.getAllOfficesForSabhaId(token.sabhaId);


                if (await _wb_unitOfWork.MeterConnectInfos.AlreadyExist(meterConnectInfo.ConnectionNo,offIces.Select(o=>o.ID).ToList()))
                {

                    throw new GeneralException("Meter Connection No Already Exist");

                }

                var numberSequence = await _wb_unitOfWork.NumberSequences.GetNumberByOfficeId(token.officeId);
                if (numberSequence == null)
                {
                    var newEntity = new NumberSequence
                    {
                        Id = null,
                        OfficeId = token.officeId,
                        CoreNumber = 1,
                        ApplicationNumber = 1,
                        Status = 1,
                    };


                    await _wb_unitOfWork.NumberSequences.AddAsync(newEntity);
                    numberSequence = newEntity;
                }
                else
                {
                    numberSequence.CoreNumber++;
                    await _wb_unitOfWork.NumberSequences.UpdateAsync(numberSequence);
                }



                var generateUniqueKey =  token.officeId.ToString() + '-' + keyPattern.ToString() + '-' + numberSequence.CoreNumber.ToString().PadLeft(5, '0');
                meterConnectInfo.ConnectionId = generateUniqueKey;
                meterConnectInfo.IsAssigned = false;
               

                await _wb_unitOfWork.MeterConnectInfos.AddAsync(meterConnectInfo);
                await _wb_unitOfWork.CommitAsync();

                return (true, "Successfully Created", meterConnectInfo);


            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                //meterConnectInfo.ConnectionId = null; return meterConnectInfo;

                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null, new MeterConnectInfo());

            }

        }

        public async Task<bool> Delete(string meterConnectionInfoId)
        {
            try
            {
                var mci = await _wb_unitOfWork.MeterConnectInfos.GetById(meterConnectionInfoId);
                if(mci != null && mci.IsAssigned ==false) {

                    _wb_unitOfWork.MeterConnectInfos.Remove(mci);
                    await _wb_unitOfWork.CommitAsync();
                }
                else
                {
                    throw new Exception("Unable To Delete {meterConnectionInfoId} MeterConnection Info");
                }

               return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Task<IEnumerable<MeterConnectInfo>> GetAllAssignedByOrderUnderSubRoad(int subRoadId)
        {
            return _wb_unitOfWork.MeterConnectInfos.GetAllAssignedByOrderUnderSubRoad(subRoadId);
        }

        public Task<IEnumerable<MeterConnectInfo>> GetAllAvailableByOrderUnderSubRoad(int subRoadId)
        {
            return _wb_unitOfWork.MeterConnectInfos.GetAllAvailableByOrderUnderSubRoad(subRoadId);
        }

        public Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoad(int subRoadId)
        {
            return _wb_unitOfWork.MeterConnectInfos.GetAllByOrderUnderSubRoad(subRoadId);
        }

        public Task<IEnumerable<MeterConnectInfo>> GetAllByOrderUnderSubRoadList(List<int?> subRoadIds)
        {
            return _wb_unitOfWork.MeterConnectInfos.GetAllByOrderUnderSubRoadList(subRoadIds);
        }

        public Task<IEnumerable<MeterConnectInfo>> GetAllConnection()
        {
            throw new NotImplementedException();
        }

        public async Task<MeterConnectInfo> GetById(string id)
        {
            return await _wb_unitOfWork.MeterConnectInfos.GetById(id);
        }

        public async Task<MeterConnectInfo> GetInfoById(string id)
        {
            return await _wb_unitOfWork.MeterConnectInfos.GetInfoById(id);
        }

        public async Task<IEnumerable<MeterConnectInfo>> SaveMultipleMeterConnections(List<MeterConnectInfo> meterConnectInfos)
        {

            //meterReaderAssignResources.ForEach(item => item.Id = null);

            //foreach (var item in meterConnectInfos)
            //{
            //    //add Primary Key
            //    do
            //    {
            //        var generateUniqueKey = GenerateUniqueString(6);
            //        var mci = await _wb_unitOfWork.MeterConnectInfos.GetById(generateUniqueKey);

            //        if (mci == null)
            //        {
            //            item.ConnectionId = generateUniqueKey;
            //            break;

            //        }
            //    } while (true);

            //}
            //try
            //{
            //    await _wb_unitOfWork.MeterConnectInfos.AddRangeAsync(meterConnectInfos);
            //    await _wb_unitOfWork.CommitAsync();
            //    return meterConnectInfos;


            //}
            //catch (Exception ex)
            //{

            //    //foreach (var entityEntry in _wb_unitOfWork.Entries<MeterConnectInfo>())
            //    //{
            //    //    if (entityEntry.State == EntityState.Unchanged)
            //    //    {
            //    //        // This entity was skipped during the AddRange operation
            //    //        var skippedEntity = entityEntry.Entity;
            //    //        // You can track or log the skipped entity as needed
            //    //    }
            //    //}



            //    Console.WriteLine(ex.Message.ToString());
            //    throw;
            //}
            List<MeterConnectInfo> trackeditems = new List<MeterConnectInfo>();

            foreach (var item in meterConnectInfos)
            {
                //var chnagedItem = await Create(item);
                //trackeditems.Add(await Create(item));

            }

            return trackeditems;

        }

        public async Task<(bool, string?, MeterConnectInfo)> Update( MeterConnectInfo newObj, HTokenClaim token)
        {
            var mci = await _wb_unitOfWork.MeterConnectInfos.GetById(newObj.ConnectionId!);
            try
            {
                var offIces = await _officeService.getAllOfficesForSabhaId(token.sabhaId);

                if ( mci.ConnectionNo != newObj.ConnectionNo && await _wb_unitOfWork.MeterConnectInfos.AlreadyExist(newObj.ConnectionNo!, offIces.Select(o => o.ID).ToList()))
                {

                    throw new GeneralException("Meter Connection No Already Exist");

                }

                if (mci != null)
                {
                    mci.ConnectionNo = newObj.ConnectionNo;
                    mci.MeterNo = newObj.MeterNo;
                    mci.UpdatedBy = newObj.UpdatedBy;
                    mci.UpdatedAt = DateTime.Now;

                    await _wb_unitOfWork.CommitAsync();
                    return (true, "Successfully Update", mci);
                }
                else
                {
                    throw new Exception("Unable To Update MeterConnection Info");
                }
            }
            catch(Exception ex)
            {
                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null, new MeterConnectInfo());
            }

        }

        public async Task UpdateOrderNo(string ConnectionId, int orderNo)
        {
            await _wb_unitOfWork.MeterConnectInfos.UpdateOrderNo(ConnectionId, orderNo);
        }


        //Key generator
        private string GenerateUniqueString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
