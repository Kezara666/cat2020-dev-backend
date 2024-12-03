using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.HelperModels;

namespace CAT20.Core.Services.Control
{
    public interface IPartnerService
    {
        Task<IEnumerable<Partner>> GetAll();
        Task<Partner> GetById(int? id);
        Task<Partner> GetByIdWithDetails(int id);
        Task<Partner> GetByNIC(string NIC);
        Task<Partner> GetByPassportNo(string passport);
        Task<Partner> GetByPhoneNo(string PhoneNo);
        Task<Partner> GetByEmail(string email);
        Task<Partner> Create(Partner newPartner);
        Task<(bool, string?)> CreatePartner(Partner newPartner);
        Task<(bool,string?)> CreateBusiness(Partner newBusiness);

        //---- [start - bulk create] --------
        Task<IEnumerable<Partner>> BulkCreate(IEnumerable<Partner> newObjsList);
        //---- [end - bulk create] ----------

        Task Update(Partner partnerToBeUpdated, Partner partner);

        Task partnerNICchange(Partner partnerToBeUpdated, Partner partner);

        Task businessRegNumberchange(Partner partnerToBeUpdated, Partner partner);

        Task partnerMobileNochange(Partner partnerToBeUpdated, Partner partner);

        

        Task Delete(Partner partner);
        Task<IEnumerable<Partner>> GetAllForPartnerType(int id);
        Task<IEnumerable<Partner>> GetAllForPartnersByIds(List<int?> ids);
        Task<IEnumerable<Partner>> GetAllForSabha(int sabha);
        Task<IEnumerable<Partner>> GetAllBusinessesForSabha(int sabhaid);
        Task<IEnumerable<Partner>> GetAllCustomersForSabha(int sabhaid);
        Task<IEnumerable<Partner>> GetAllBusinessOwnersForSabha(int sabha);
        Task<IEnumerable<Partner>> GetAllBusinessOwnersForOffice(int office);
        Task<IEnumerable<Partner>> GetAllForPartnerTypeAndSabha(int id, int sabhaId);


        Task<Partner> GetBusinessByRegNo(string regNo);
        Task<Partner> GetBusinessByPhoneNo(string phoneNo);
        Task<Partner> CreatePartnerImage(HUploadUserDocument obj, object environment, string _uploadsFolder);
        Task<Partner> GetPartnerImageById(int id);
        Task<Partner> CreatePartnerDocument(HUploadPartnerDocument obj, object environment, string _uploadsFolder);
        Task<Partner> GetPartnerWithDocumentsById(int id);
        Task<Partner> DeletePartnerDocument(int docid, int partnerid, string _uploadsFolder);

        Task<(int totalCount, IEnumerable<Partner> list)> GetAllForPartnersBySearchQuery(int? sabahId, List<int?> includedIds, string? query, int pageNo, int pageSize);
    }
}

