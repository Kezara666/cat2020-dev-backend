using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.User;
using CAT20.Core.Services.Control;
using DocumentFormat.OpenXml.Office2010.Excel;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CAT20.Services.Control
{
    public class PartnerService : IPartnerService
    {
        private readonly IControlUnitOfWork _unitOfWork;
        public PartnerService(IControlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Partner> Create(Partner newPartner)
        {
            
                await _unitOfWork.Partners
                .AddAsync(newPartner);

                #region Audit Log
                //var note = new StringBuilder();
                //if (newPartner.ID == 0)
                //    note.Append("Created on ");
                //else
                //    note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");


                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = newPartner.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                await _unitOfWork.CommitAsync();
          

                return await _unitOfWork.Partners.GetByIdAsync(newPartner.Id);
        }
        public async Task Delete(Partner partner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Deleted on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append("System");

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = partner.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});
                partner.Active = 0;

                #endregion

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
            //_unitOfWork.Partners.Remove(partner);
        }
        public async Task<IEnumerable<Partner>> GetAll()
        {
            return await _unitOfWork.Partners.GetAllAsync();
        }
        public async Task<Partner> GetById(int? id)
        {
            return await _unitOfWork.Partners.GetByIdAsync(id);
        }

        public async Task<Partner> GetByNIC(string NIC)
        {
            return await _unitOfWork.Partners.GetByNICAsync(NIC);
        }
        public async Task<Partner> GetByPassportNo(string passport)
        {
            return await _unitOfWork.Partners.GetByPassportNoAsync(passport);
        }

        public async Task<Partner> GetByPhoneNo(string PhoneNo)
        {
            return await _unitOfWork.Partners.GetByPhoneNoAsync(PhoneNo);
        }

        public async Task<Partner> GetByEmail(string Email)
        {
            return await _unitOfWork.Partners.GetByEmailAsync(Email);
        }

        public async Task Update(Partner partnerToBeUpdated, Partner partner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (partnerToBeUpdated.NameEnglish != partner.NameEnglish)
                //    note.Append(" English Name :" + partnerToBeUpdated.NameEnglish + " Change to " + partner.NameEnglish);
                //if (partnerToBeUpdated.NameSinhala != partner.NameSinhala)
                //    note.Append(" Sinhala Name :" + partnerToBeUpdated.NameSinhala + " Change to " + partner.NameSinhala);
                //if (partnerToBeUpdated.NameTamil != partner.NameTamil)
                //    note.Append(" Tamil Name :" + partnerToBeUpdated.NameTamil + " Change to " + partner.NameTamil);
                //if (partnerToBeUpdated.Code != partner.Code)
                //    note.Append(" Code :" + partnerToBeUpdated.Code + " Change to " + partner.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = partnerToBeUpdated.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion


                if (partnerToBeUpdated.IsEditable == 1)
                {
                    partnerToBeUpdated.Name = partner.Name;
                }
                //partnerToBeUpdated.NicNumber = partner.NicNumber;
                //partnerToBeUpdated.MobileNumber = partner.MobileNumber;
                partnerToBeUpdated.PhoneNumber = partner.PhoneNumber;
                partnerToBeUpdated.Street1 = partner.Street1;
                partnerToBeUpdated.Street2 = partner.Street2;
                partnerToBeUpdated.City = partner.City;
                partnerToBeUpdated.Zip = partner.Zip;
                partnerToBeUpdated.Email = partner.Email;
                partnerToBeUpdated.Active = 1;
                partnerToBeUpdated.GnDivisionId = partner.GnDivisionId;
                partnerToBeUpdated.UpdatedAt = partner.UpdatedAt;
                partnerToBeUpdated.IsEditable = partner.IsEditable;
                partnerToBeUpdated.UpdatedBy = partner.UpdatedBy;
                partnerToBeUpdated.BusinessRegNo = partner.BusinessRegNo;
                //partnerToBeUpdated.RIUserId = partner.RIUserId;
                partnerToBeUpdated.IsPropertyOwner = partner.IsPropertyOwner;
                partnerToBeUpdated.IsBusinessOwner = partner.IsBusinessOwner;
                partnerToBeUpdated.PartnerMobiles = partner.PartnerMobiles;
                partnerToBeUpdated.PermittedThirdPartyAssessments = partner.PermittedThirdPartyAssessments;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task partnerNICchange(Partner partnerToBeUpdated, Partner partner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (partnerToBeUpdated.NameEnglish != partner.NameEnglish)
                //    note.Append(" English Name :" + partnerToBeUpdated.NameEnglish + " Change to " + partner.NameEnglish);
                //if (partnerToBeUpdated.NameSinhala != partner.NameSinhala)
                //    note.Append(" Sinhala Name :" + partnerToBeUpdated.NameSinhala + " Change to " + partner.NameSinhala);
                //if (partnerToBeUpdated.NameTamil != partner.NameTamil)
                //    note.Append(" Tamil Name :" + partnerToBeUpdated.NameTamil + " Change to " + partner.NameTamil);
                //if (partnerToBeUpdated.Code != partner.Code)
                //    note.Append(" Code :" + partnerToBeUpdated.Code + " Change to " + partner.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = partnerToBeUpdated.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion


                partnerToBeUpdated.NicNumber = partner.NicNumber;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task businessRegNumberchange(Partner partnerToBeUpdated, Partner partner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (partnerToBeUpdated.NameEnglish != partner.NameEnglish)
                //    note.Append(" English Name :" + partnerToBeUpdated.NameEnglish + " Change to " + partner.NameEnglish);
                //if (partnerToBeUpdated.NameSinhala != partner.NameSinhala)
                //    note.Append(" Sinhala Name :" + partnerToBeUpdated.NameSinhala + " Change to " + partner.NameSinhala);
                //if (partnerToBeUpdated.NameTamil != partner.NameTamil)
                //    note.Append(" Tamil Name :" + partnerToBeUpdated.NameTamil + " Change to " + partner.NameTamil);
                //if (partnerToBeUpdated.Code != partner.Code)
                //    note.Append(" Code :" + partnerToBeUpdated.Code + " Change to " + partner.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = partnerToBeUpdated.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion


                partnerToBeUpdated.BusinessRegNo = partner.BusinessRegNo;
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }



        public async Task partnerMobileNochange(Partner partnerToBeUpdated, Partner partner)
        {
            try
            {
                #region Audit Log

                //var note = new StringBuilder();
                //note.Append("Edited on ");
                //note.Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //note.Append(" by ");
                //note.Append(" System.");

                //if (partnerToBeUpdated.NameEnglish != partner.NameEnglish)
                //    note.Append(" English Name :" + partnerToBeUpdated.NameEnglish + " Change to " + partner.NameEnglish);
                //if (partnerToBeUpdated.NameSinhala != partner.NameSinhala)
                //    note.Append(" Sinhala Name :" + partnerToBeUpdated.NameSinhala + " Change to " + partner.NameSinhala);
                //if (partnerToBeUpdated.NameTamil != partner.NameTamil)
                //    note.Append(" Tamil Name :" + partnerToBeUpdated.NameTamil + " Change to " + partner.NameTamil);
                //if (partnerToBeUpdated.Code != partner.Code)
                //    note.Append(" Code :" + partnerToBeUpdated.Code + " Change to " + partner.Code);

                //await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                //{
                //    dateTime = DateTime.Now,
                //    TransactionID = partnerToBeUpdated.ID,
                //    TransactionName = "Partner",
                //    User = 1,
                //    Note = note.ToString()
                //});

                #endregion

                partnerToBeUpdated.MobileNumber = partner.MobileNumber;


                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<Partner>> GetAllForSabha(int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllForSabhaAsync(sabhaid);
        }

        public async Task<IEnumerable<Partner>> GetAllCustomersForSabha(int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllCustomersForSabhaAsync(sabhaid);
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessesForSabha(int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllBusinessesForSabhaAsync(sabhaid);
        }


        public async Task<IEnumerable<Partner>> GetAllForPartnerType(int id)
        {
            return await _unitOfWork.Partners.GetAllForPartnerTypeAsync(id);
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnerTypeAndSabha(int id, int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllForPartnerTypeAndSabhaAsync(id, sabhaid);
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessOwnersForSabha(int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllBusinessOwnersForSabhaAsync(sabhaid);
        }

        public async Task<IEnumerable<Partner>> GetAllBusinessOwnersForOffice(int sabhaid)
        {
            return await _unitOfWork.Partners.GetAllBusinessOwnersForOfficeAsync(sabhaid);
        }

        public async Task<IEnumerable<Partner>> GetAllForPartnersByIds(List<int?> ids)
        {
            return await _unitOfWork.Partners.GetAllForPartnersByIds(ids);
        }

        //---- [start - bulk create] --------
        public async Task<IEnumerable<Partner>> BulkCreate(IEnumerable<Partner> newObjsList)
        {
            try
            {
                int batchSize = 4000;
                for (int i = 0; i < newObjsList.Count(); i += batchSize)
                {
                    var batch = newObjsList.Skip(i).Take(batchSize).ToList();
                    await _unitOfWork.Partners.AddRangeAsync(batch);
                }
                await _unitOfWork.CommitAsync();

                return newObjsList;
            }
            catch (Exception ex)
            {
                return new List<Partner>();
            }
        }

        public async Task<Partner> GetByIdWithDetails(int id)
        {
           return  await _unitOfWork.Partners.GetByIdWithDetails(id);
        }

        public async Task<(int totalCount, IEnumerable<Partner> list)> GetAllForPartnersBySearchQuery(int? sabahId, List<int?> includedIds, string? query, int pageNo, int pageSize)
        {
            return await _unitOfWork.Partners.GetAllForPartnersBySearchQuery(sabahId,includedIds, query, pageNo, pageSize);
        }

        public Task<Partner> GetBusinessByRegNo(string regNo)
        {
            return _unitOfWork.Partners.GetBusinessByRegNo(regNo);
        }

        public Task<Partner> GetBusinessByPhoneNo(string phoneNo)
        {
            return _unitOfWork.Partners.GetBusinessByPhoneNo(phoneNo);
        }

        public async Task<(bool, string?)> CreatePartner(Partner newPartner)
        {
            try
            {
                if (newPartner.Id != 0)
                {
                    var partner = await _unitOfWork.Partners.GetByIdAsync(newPartner.Id);

                    if (partner != null)
                    {

                        partner.Name = newPartner.Name;
                        partner.PhoneNumber = newPartner.PhoneNumber;
                        partner.MobileNumber = newPartner.MobileNumber;
                        await _unitOfWork.CommitAsync();

                        return (true, "Successfully Updated");
                    }
                    else
                    {
                        throw new GeneralException("Unable To Found Business Registration Number");
                    }

                }
                else
                {
                    if (await _unitOfWork.Partners.IsBusinessRegNoExist(newPartner.BusinessRegNo!))
                    {
                        await _unitOfWork.Partners.AddAsync(newPartner);
                        await _unitOfWork.CommitAsync();
                        return (true, "Successfully Created");
                    }
                    else
                    {
                        throw new GeneralException("Business Registration Number Already Exists");
                    }
                }


            }
            catch (Exception ex)
            {

                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }

        public async Task<(bool, string?)> CreateBusiness(Partner newBusiness)
        {
            try
            {
                if (newBusiness.Id != 0)
                {
                    var business = await _unitOfWork.Partners.GetByIdAsync(newBusiness.Id);

                    if (business != null)
                    {

                        business.Name = newBusiness.Name;
                        business.PhoneNumber = newBusiness.PhoneNumber;
                        business.MobileNumber = newBusiness.MobileNumber;
                        await _unitOfWork.CommitAsync();

                        return (true, "Successfully Updated");
                    }
                    else
                    {
                        throw new GeneralException("Unable To Found Business Registration Number");
                    }

                }
                else
                {
                    if (await _unitOfWork.Partners.IsBusinessRegNoExist(newBusiness.BusinessRegNo!)) {
                        await _unitOfWork.Partners.AddAsync(newBusiness);
                        await _unitOfWork.CommitAsync();
                        return (true, "Successfully Created");
                    }
                    else
                    {
                        throw new GeneralException("Business Registration Number Already Exists");
                    }
                }


            }
            catch (Exception ex) {

                return (false, ex is FinalAccountException or GeneralException ? ex.Message : null);
            }
        }

        public async Task<Partner> CreatePartnerImage(HUploadUserDocument obj, object environment, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {
                var partner = await _unitOfWork.Partners.GetByIdAsync(obj.Id);

                if (partner != null)
                {
                    if (obj.File == null || obj.File.Length == 0)
                    {
                        return null;
                    }

                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    string uniqueFileName = partner.Id + "_P_" + Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;
                    filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    if (!string.IsNullOrEmpty(partner.ProfilePicPath))
                    {
                        string oldFilePath = Path.Combine(_uploadsFolder, partner.ProfilePicPath);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.File.CopyToAsync(stream);
                    }

                    partner.ProfilePicPath = uniqueFileName;
                    await _unitOfWork.CommitAsync();

                    return partner;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return null;
        }


        public async Task<Partner> GetPartnerImageById(int id)
        {
            var userDetail = await _unitOfWork.Partners.GetByIdAsync(id);

            if (userDetail == null)
            {
                throw new KeyNotFoundException($"Partner with ID {id} not found.");
            }
            return userDetail;

        }

        //public async Task<Partner> CreatePartnerDocument(HUploadPartnerDocument obj, object environment, string _uploadsFolder)
        //{
        //    string? filePath = null;

        //    try
        //    {
        //        var partner = await _unitOfWork.Partners.GetByIdAsync(obj.PartnerId);

        //        if (partner != null)
        //        {
        //            if (obj.File == null || obj.File.Length == 0)
        //            {
        //                return null;
        //            }

        //            if (!Directory.Exists(_uploadsFolder))
        //            {
        //                Directory.CreateDirectory(_uploadsFolder);
        //            }

        //            string uniqueFileName = partner.Id + "_" + obj.partnerDocumentType + "_" +  Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;

        //            filePath = Path.Combine(_uploadsFolder, uniqueFileName);

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await obj.File.CopyToAsync(stream);
        //            }
        //            var PartnerDocument = new PartnerDocument();
        //            PartnerDocument.Id = null;
        //            PartnerDocument.DocumentType = obj.partnerDocumentType;
        //            PartnerDocument.Description = obj.Description;
        //            PartnerDocument.FileName = uniqueFileName;
        //            PartnerDocument.PartnerId = obj.PartnerId;

        //            await _unitOfWork.PartnerDocuments.AddAsync(PartnerDocument);
        //            await _unitOfWork.CommitAsync();

        //            return partner;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //    }

        //    return null;
        //}

        public async Task<Partner> CreatePartnerDocument(HUploadPartnerDocument obj, object environment, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {
                var partner = await _unitOfWork.Partners.GetWithDocumentsByIdAsync(obj.PartnerId);

                if (partner != null)
                {
                    if (obj.File == null || obj.File.Length == 0)
                    {
                        return null;
                    }

                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    string uniqueFileName = partner.Id + "_" + obj.partnerDocumentType + "_" + Guid.NewGuid().ToString().Substring(0, 12) + "_" + obj.File.FileName;
                    filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    // Image Compression Logic
                    if (IsImageFile(obj.File.FileName))
                    {
                        using (var image = Image.Load(obj.File.OpenReadStream())) // Load the image from the uploaded file stream
                        {
                            var jpegEncoder = new JpegEncoder
                            {
                                Quality = 70 // Set the desired quality (1-100)
                            };

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.SaveAsJpegAsync(fileStream, jpegEncoder); // Save the compressed image
                            }
                        }
                    }
                    else
                    {
                        // Save non-image files
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await obj.File.CopyToAsync(stream);
                        }
                    }

                    if (obj.partnerDocumentType != PartnerDocumentType.Other)
                    {
                        var existingDocument = partner.PartnerDocuments
                            .FirstOrDefault(doc => doc.DocumentType == obj.partnerDocumentType);

                        if (existingDocument != null)
                        {
                            if (File.Exists(Path.Combine(_uploadsFolder, existingDocument.FileName)))
                            {
                                File.Delete(Path.Combine(_uploadsFolder, existingDocument.FileName));
                            }

                            existingDocument.FileName = uniqueFileName;
                            existingDocument.Description = obj.Description;
                        }
                        else
                        {
                            var newDocument = new PartnerDocument
                            {
                                DocumentType = obj.partnerDocumentType,
                                Description = obj.Description,
                                FileName = uniqueFileName,
                                PartnerId = obj.PartnerId
                            };

                            await _unitOfWork.PartnerDocuments.AddAsync(newDocument);
                        }
                    }
                    else
                    {
                        //add "Other"
                        var newDocument = new PartnerDocument
                        {
                            DocumentType = obj.partnerDocumentType,
                            Description = obj.Description,
                            FileName = uniqueFileName,
                            PartnerId = obj.PartnerId
                        };

                        await _unitOfWork.PartnerDocuments.AddAsync(newDocument);
                    }
                    await _unitOfWork.CommitAsync();
                    return partner; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return null;
        }



        private bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif";
        }

        public async Task<Partner> GetPartnerWithDocumentsById(int id)
        {
            var userDetail = await _unitOfWork.Partners.GetWithDocumentsByIdAsync(id);

            if (userDetail == null)
            {
                throw new KeyNotFoundException($"Partner with ID {id} not found.");
            }
            return userDetail;

        }


        public async Task<Partner> DeletePartnerDocument(int docId, int partnerId, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {
                var partner = await _unitOfWork.Partners.GetWithDocumentsByIdAsync(partnerId);

                if (partner != null)
                {
                    if (partner.PartnerDocuments.Any())
                    {
                        PartnerDocument existingDocumenttoDelete = new PartnerDocument();
                           existingDocumenttoDelete = partner.PartnerDocuments.FirstOrDefault(doc => doc.Id == docId);

                        if (existingDocumenttoDelete != null)
                        {
                            if (File.Exists(Path.Combine(_uploadsFolder, existingDocumenttoDelete.FileName)))
                            {
                                File.Delete(Path.Combine(_uploadsFolder, existingDocumenttoDelete.FileName));
                            }
                            //await _unitOfWork.PartnerDocuments.Remove(existingDocumenttoDelete);

                            existingDocumenttoDelete.Status = false;
                        }
                    }
                    await _unitOfWork.CommitAsync();

                    var partnernew = await _unitOfWork.Partners.GetWithDocumentsByIdAsync(partnerId);
                    return partnernew;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return null;
        }
    }
}