using AutoMapper;
using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Final.Save;
using CAT20.Core.Models.FinalAccount;
using CAT20.Core.Services.FinalAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.FinalAccount
{
    public class VoucherDocumentService : IVoucherDocumentService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VoucherDocumentService(IVoteUnitOfWork voteUnitOfWork,IMapper mapper )
        {
            _unitOfWork = voteUnitOfWork;
            _mapper = mapper;

        }
               
        public async Task<IEnumerable<VoucherDocument>> GetAllDocumentsForVoucher(int voucherId)
        {
            return await _unitOfWork.VoucherDocument.GetAllDocumentsForVoucher(voucherId);
        }

        public async Task<(bool, string?)> RemoveDocument(VoucherDocument voucherDocument, object environment, string _uploadsFolder)
        {
            try
            {
                var document = await _unitOfWork.VoucherDocument.GetByIdAsync(voucherDocument.Id);
                if (document == null)
                {
                    throw new Exception("Document not found");
                }
                else
                {

                    if (Directory.Exists(_uploadsFolder))
                    {
                        var filePath = Path.Combine(_uploadsFolder, document.Uri);

                        _unitOfWork.VoucherDocument.Remove(document);


                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        await _unitOfWork.CommitAsync();
                        return (true,"Remove Successfully");
                    }
                    else
                    {
                        Console.WriteLine();
                        throw new FinalAccountException("Directory not found");
                    }
                }

            }
            catch (Exception ex)
            {

                if (ex is FinalAccountException)
                {
                    return (false, ex.Message.ToString());
                }
                else
                {

                    return (false, null);
                }

            }
        }

        public async Task<(bool,string?)> UploadDocument(SaveVoucherDocument voucherDocument, object environment, string _uploadsFolder)
        {


                string? filePath = null;

            try
            {

                var newVoucherDocument = _mapper.Map<SaveVoucherDocument, VoucherDocument>(voucherDocument);


                if (newVoucherDocument.File == null || newVoucherDocument.File.Length == 0)
                {
                    throw new FinalAccountException("File is empty");
                }

                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                //string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 12) + "_" + newVoucherDocument.File.FileName;
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(newVoucherDocument.File.FileName);

                filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newVoucherDocument.File.CopyToAsync(stream);
                }

                newVoucherDocument.Uri = uniqueFileName;


                await _unitOfWork.VoucherDocument.AddAsync(newVoucherDocument);
                await _unitOfWork.CommitAsync();

                return (true, "Upload Successfully");


            }
            catch (Exception ex)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (ex is FinalAccountException)
                {
                    return (false, ex.Message.ToString());
                }
                else
                {
                    return (false, null);
                }


            }

        }
    }
}
