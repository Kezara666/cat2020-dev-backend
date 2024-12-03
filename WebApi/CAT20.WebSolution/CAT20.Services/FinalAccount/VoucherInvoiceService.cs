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
    public class VoucherInvoiceService : IVoucherInvoiceService
    {
        private readonly IVoteUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VoucherInvoiceService(IVoteUnitOfWork voteUnitOfWork, IMapper mapper)
        {
            _unitOfWork = voteUnitOfWork;
            _mapper = mapper;

        }

        public async Task<IEnumerable<VoucherInvoice>> GetAllInvoicesForVoucher(int voucherId)
        {
            return await _unitOfWork.VoucherInvoice.GetAllInvoicesForVoucher(voucherId);
        }

        public async Task<(bool, string?)> RemoveInvoice(VoucherInvoice voucherInvoice, object environment, string _uploadsFolder)
        {
            try
            {
                var invoice = await _unitOfWork.VoucherInvoice.GetByIdAsync(voucherInvoice.Id);
                if (invoice == null)
                {
                    throw new Exception("Document not found");
                }
                else
                {

                    if (Directory.Exists(_uploadsFolder))
                    {
                        var filePath = Path.Combine(_uploadsFolder, invoice.Uri);

                        _unitOfWork.VoucherInvoice.Remove(invoice);


                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        await _unitOfWork.CommitAsync();
                        return (true, "Remove Successfully");
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

        public async Task<(bool, string?)> UploadInvoice(SaveVoucherInvoice voucherInvoice, object environment, string _uploadsFolder)
        {
            string? filePath = null;

            try
            {

                var newVoucherInvoice = _mapper.Map<SaveVoucherInvoice, VoucherInvoice>(voucherInvoice);


                if (newVoucherInvoice.File == null || newVoucherInvoice.File.Length == 0)
                {
                    throw new FinalAccountException("File is empty");
                }

                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                //string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 12) + "_" + newVoucherInvoice.File.FileName;
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(newVoucherInvoice.File.FileName);

                filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newVoucherInvoice.File.CopyToAsync(stream);
                }

                newVoucherInvoice.Uri = uniqueFileName;


                await _unitOfWork.VoucherInvoice.AddAsync(newVoucherInvoice);
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
