using CAT20.Core;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{

    public class AllocationService : IAllocationService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AllocationService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Allocation> GetById(int id)
        {
            return await _unitOfWork.Allocations.GetById(id);
        }

        public async Task<Allocation> GetForAssessment(int assessmentid)
        {
            return await _unitOfWork.Allocations.GetForAssessment(assessmentid);
        }

        public async Task<IEnumerable<Allocation>> GetAll()
        {
            return await _unitOfWork.Allocations.GetAll();
        }

        public async Task<IEnumerable<Allocation>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.Allocations.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<Allocation>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Allocations.GetAllForSabha(sabhhaid);
        }

        public async Task<Allocation> Create(Allocation newAllocation)
        {
            try
            {
                await _unitOfWork.Allocations
                .AddAsync(newAllocation);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newAllocation.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newAllocation.ID,
                //    TransactionName = "Allocation",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return newAllocation;
        }

        public async Task<IEnumerable<Allocation>> BulkCreate(IEnumerable<Allocation> newObjsList)
        {
            try
            {
                await _unitOfWork.Allocations
                .AddRangeAsync(newObjsList);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return newObjsList;
        }

        public async Task Update(Allocation AllocationToBeUpdated, Allocation Allocation)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (AllocationToBeUpdated.NameEnglish != Allocation.NameEnglish)
                //    note.Append(" English Name :" + AllocationToBeUpdated.NameEnglish + " Change to " + Allocation.NameEnglish);
                //if (AllocationToBeUpdated.NameSinhala != Allocation.NameSinhala)
                //    note.Append(" Sinhala Name :" + AllocationToBeUpdated.NameSinhala + " Change to " + Allocation.NameSinhala);
                //if (AllocationToBeUpdated.NameTamil != Allocation.NameTamil)
                //    note.Append(" Tamil Name :" + AllocationToBeUpdated.NameTamil + " Change to " + Allocation.NameTamil);
                //if (AllocationToBeUpdated.Code != Allocation.Code)
                //    note.Append(" Code :" + AllocationToBeUpdated.Code + " Change to " + Allocation.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = AllocationToBeUpdated.ID,
                //    TransactionName = "Allocation",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                AllocationToBeUpdated.Status = Allocation.Status;
                AllocationToBeUpdated.AllocationDescription = Allocation.AllocationDescription;
                AllocationToBeUpdated.AllocationAmount = Allocation.AllocationAmount;
                AllocationToBeUpdated.ChangedDate = Allocation.ChangedDate;
                AllocationToBeUpdated.UpdatedBy = Allocation.UpdatedBy;
                //AllocationToBeUpdated.AssessmentId = Allocation.AssessmentId;
                AllocationToBeUpdated.UpdatedAt = DateTime.Now;

                await _unitOfWork.CommitAsync();


            }
            catch (Exception)
            {
            }
        }

        public async Task Delete(Allocation obj)
        {
            _unitOfWork.Allocations.Remove(obj);
            await _unitOfWork.CommitAsync();
        }

        public async Task<(bool, string?)> UpdateNextYearAllocations(int sabhaId)
        {
            try
            {
                var asllocations = await _unitOfWork.Allocations.GetForNextYearAllocationsUpdate(sabhaId);

                foreach (var asllocation in asllocations)
                {

                    asllocation.NextYearAllocationAmount = asllocation.AllocationAmount;

                }

                await _unitOfWork.CommitAsync();

                return (true, "Successfully");
            }catch (Exception ex)
            {
                return (false, ex.Message.ToString());
            }
        }
    }
}