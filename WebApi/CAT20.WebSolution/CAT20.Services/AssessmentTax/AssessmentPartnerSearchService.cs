using CAT20.Core;
using CAT20.Core.HelperModels;
using CAT20.Core.Services.AssessmentTax;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentPartnerSearchService : IAssessmentPartnerSearchService
    {
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        public AssessmentPartnerSearchService(IAssessmentTaxUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HPartnerName>> GetPartnerSearchByNames(int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, string query)
        {
            try
            {
                var updatedPartnerIDs = await _unitOfWork.Assessments.GetAllPartnerUpdatedPartnerIdsForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);
                var partners = await _unitOfWork.Partners.GetAllForPartnersByIdsThenSearchName(updatedPartnerIDs, query);

                /*
                 * Map entries form partner tables to new helper model
                 */


                var namesFromPartnerEntity = partners.Select(p => new HPartnerName
                {
                    ModelKey = 1,
                    Id = p.Id,
                    Name = p.Name,

                }).ToList();


                /*
               * Map entries form temp partner tables to new helper model
               */

                var tempPartners = await _unitOfWork.Assessments.GetAllTempPartnersNamesForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);



                var namesFromTempPartnerEntity = tempPartners.Select(tp => new HPartnerName
                {
                    ModelKey = 2,
                    //Id = tp.Id,
                    Name = tp.Name,

                }).DistinctBy(tp => tp.Name).ToList();



                return namesFromPartnerEntity.Concat(namesFromTempPartnerEntity).ToList();
            }
            catch (Exception ex)
            {
                return new List<HPartnerName>();
            }
        }



        public async Task<IEnumerable<HPartnerNIC>> GetPartnerSearchByNics(int? sabhaId, int? officeId, int? wardId, int? streetId, int? propertyType, string query)
        {
            try
            {
                var updatedPartnerIDs = await _unitOfWork.Assessments.GetAllPartnerUpdatedPartnerIdsForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);
                var partners = await _unitOfWork.Partners.GetAllForPartnersByIdsThenSearchNic(updatedPartnerIDs, query);

                /*
                 * Map entries form partner tables to new helper model
                 */

                var nicFromPartnerEntity = partners.Select(p => new HPartnerNIC
                {
                    ModelKey = 1,
                    Id = p.Id,
                    Nic = p.NicNumber,
                }).ToList();




                /*
               * Map entries form temp partner tables to new helper model
               */

                var tempPartners = await _unitOfWork.Assessments.GetAllTempPartnersNicsForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);


                var nicFromTempPartnerEntity = tempPartners.Select(tp => new HPartnerNIC
                {
                    ModelKey = 2,
                    //Id = tp.Id,
                    Nic = tp.NICNumber,

                }).DistinctBy(tp => tp.Nic).ToList();






                return nicFromPartnerEntity.Concat(nicFromTempPartnerEntity).ToList();
            }
            catch (Exception ex)
            {
                return new List<HPartnerNIC>();
            }
        }

        /*
        public async Task<HAssessmentPartners> GetPartnerSearchByNics(Nullable<int> sabhaId, Nullable<int> officeId, Nullable<int> wardId, Nullable<int> streetId, Nullable<int> propertyType, string query)
        {
            try
            {
                var updatedPartnerIDs = await _unitOfWork.Assessments.GetAllPartnerUpdatedPartnerIdsForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);
                var partners = await _unitOfWork.Partners.GetAllForPartnersByIdsThenSearchNic(updatedPartnerIDs, query);

                

                var nicFromPartnerEntity = partners.Select(p => new HPartnerNIC
                {
                    ModelKey = 1,
                    Id = p.Id,
                    Nic = p.NicNumber,
                }).ToList();

                var namesFromPartnerEntity = partners.Select(p => new HPartnerName
                {
                    ModelKey = 1,
                    Id = p.Id,
                    Names = p.Name,

                }).ToList();


             

                var tempPartners = await _unitOfWork.Assessments.GetAllTempPartnersNicsForSabha(sabhaId, officeId, wardId, streetId, propertyType, query);


                var nicFromTempPartnerEntity = tempPartners.Select(tp => new HPartnerNIC
                {
                    ModelKey = 2,
                    //Id = tp.Id,
                    Nic = tp.NICNumber,

                }).ToList();


                var namesFromTempPartnerEntity = tempPartners.Select(tp => new HPartnerName
                {
                    ModelKey = 2,
                    //Id = tp.Id,
                    Names = tp.Name,

                }).ToList();





                var assessmentPartners = new HAssessmentPartners
                {

                    PartnerNics = nicFromPartnerEntity.Concat(nicFromTempPartnerEntity).ToList(),
                    PartnerNames = namesFromPartnerEntity.Concat(namesFromTempPartnerEntity).ToList(),
                };


                return assessmentPartners;
            }
            catch (Exception ex)
            {
                return new HAssessmentPartners();
            }
        }
        */
    }
}