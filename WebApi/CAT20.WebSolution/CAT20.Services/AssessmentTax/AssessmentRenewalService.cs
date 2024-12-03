using CAT20.Core;
using CAT20.Core.CustomExceptions;
using CAT20.Core.DTO.Assessment;
using CAT20.Core.HelperModels;
using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Services.AssessmentTax;
using CAT20.Core.Services.Control;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Services.AssessmentTax
{
    public class AssessmentRenewalService : IAssessmentRenewalService
    {
        private readonly ILogger<AssessmentProcessService> _logger;
        private readonly IAssessmentTaxUnitOfWork _unitOfWork;
        private readonly IOfficeService _officeService;

        public AssessmentRenewalService(ILogger<AssessmentProcessService> logger, IAssessmentTaxUnitOfWork unitOfWork, IOfficeService officeService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _officeService = officeService;
        }

        public async Task<(bool, string, byte[])> GetExcelForRenewal(int sabhaId, HTokenClaim token)
        {
            try
            {
                var YourPassword = "spU*#Spe6i0U4os4ip_0afonUqUPh!4oyI$a4l&a";
                var officess = await _officeService.getAllOfficesForSabhaId(sabhaId);
                var wards = await _unitOfWork.Wards.GetAllForSabha(sabhaId);
                var streets = await _unitOfWork.Streets.GetAllForSabha(sabhaId);
                var propertyTypes = await _unitOfWork.AssessmentPropertyTypes.GetAllForSabha(sabhaId);
                var descriptions = await _unitOfWork.Descriptions.GetAllForSabha(sabhaId);


                var assessments = await _unitOfWork.Assessments.GetAssessmentForRenewal(sabhaId);

                using (var workbook = new XLWorkbook())
                {
                    var offficeWorksheet = workbook.Worksheets.Add("Offices");
                    var wardsWorksheet = workbook.Worksheets.Add("Wards");
                    var streetWorksheet = workbook.Worksheets.Add("Streets");
                    var propertyTypesWorksheet = workbook.Worksheets.Add("PropertyTypes");
                    var descriptionWorksheet = workbook.Worksheets.Add("PropertyDescriptions");

                    var assessmentWorksheet = workbook.Worksheets.Add("AssessmentData");
                    var worksheet = workbook.Worksheets.Add(token.sabhaId.ToString());

                    var allowedElements = XLSheetProtectionElements.FormatColumns
                        | XLSheetProtectionElements.AutoFilter | XLSheetProtectionElements.Sort
                        | XLSheetProtectionElements.FormatEverything | XLSheetProtectionElements.FormatColumns
                        | XLSheetProtectionElements.SelectUnlockedCells | XLSheetProtectionElements.SelectEverything;


                    offficeWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    wardsWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    streetWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    propertyTypesWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    descriptionWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    assessmentWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    worksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);

                    //var protection = offficeWorksheet.Protect("YourPassword");
                    ////protection = wardsWorksheet.Protect("YourPassword");
                    ////protection = streetWorksheet.Protect("YourPassword");
                    //protection = propertyTypesWorksheet.Protect("YourPassword");
                    //protection = descriptionWorksheet.Protect("YourPassword");
                    //protection = assessmentWorksheet.Protect("YourPassword");


                    wardsWorksheet.Range("A1:F1").SetAutoFilter();
                    streetWorksheet.Range("A1:G1").SetAutoFilter();
                    propertyTypesWorksheet.Range("A1:H1").SetAutoFilter();
                    descriptionWorksheet.Range("A1:D1").SetAutoFilter();
                    assessmentWorksheet.Range("A1:T1").SetAutoFilter();
                    

                    offficeWorksheet.Cell(1, 1).Value = "OfficeId"; // First row, first column
                    offficeWorksheet.Cell(1, 2).Value = "Office Name";
                    offficeWorksheet.Cell(1, 3).Value = "SabhaId";

                    foreach (var (item, index) in officess.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        offficeWorksheet.Cell(row, 1).Value = item.ID;
                        offficeWorksheet.Cell(row, 2).Value = item.NameEnglish;
                        offficeWorksheet.Cell(row, 3).Value = item.SabhaID;
                    }


                    wardsWorksheet.Cell(1, 1).Value = "WardId"; // First row, first column
                    wardsWorksheet.Cell(1, 2).Value = "WardName";
                    wardsWorksheet.Cell(1, 3).Value = "WardCode";
                    wardsWorksheet.Cell(1, 4).Value = "WardNo";
                    wardsWorksheet.Cell(1, 5).Value = "OfficeId";
                    wardsWorksheet.Cell(1, 6).Value = "SabhaId";

                    foreach (var (item, index) in wards.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        wardsWorksheet.Cell(row, 1).Value = item.Id;
                        wardsWorksheet.Cell(row, 2).Value = item.WardName;
                        wardsWorksheet.Cell(row, 3).Value = item.WardCode;
                        wardsWorksheet.Cell(row, 4).Value = item.WardNo;
                        wardsWorksheet.Cell(row, 5).Value = item.OfficeId;
                        wardsWorksheet.Cell(row, 6).Value = item.SabhaId;
                    }

                    streetWorksheet.Cell(1, 1).Value = "StreetId"; // First row, first column
                    streetWorksheet.Cell(1, 2).Value = "StreetName";
                    streetWorksheet.Cell(1, 3).Value = "StreetNo";
                    streetWorksheet.Cell(1, 4).Value = "StreetCode";
                    streetWorksheet.Cell(1, 5).Value = "WardName";
                    streetWorksheet.Cell(1, 6).Value = "WardNo";
                    streetWorksheet.Cell(1, 7).Value = "WardId";

                    foreach (var (item, index) in streets.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        streetWorksheet.Cell(row, 1).Value = item.Id;
                        streetWorksheet.Cell(row, 2).Value = item.StreetName;
                        streetWorksheet.Cell(row, 3).Value = item.StreetNo;
                        streetWorksheet.Cell(row, 4).Value = item.StreetCode;
                        streetWorksheet.Cell(row, 5).Value = item.Ward!.WardName;
                        streetWorksheet.Cell(row, 6).Value = item.Ward.WardNo;
                        streetWorksheet.Cell(row, 7).Value = item.WardId;


                    }

                    propertyTypesWorksheet.Cell(1, 1).Value = "PropertyTypeId"; // First row, first column
                    propertyTypesWorksheet.Cell(1, 2).Value = "PropertyType";
                    propertyTypesWorksheet.Cell(1, 3).Value = "QuarterRate";
                    propertyTypesWorksheet.Cell(1, 4).Value = "WarrantRate";
                    propertyTypesWorksheet.Cell(1, 5).Value = "PropertyTypeNo";
                    propertyTypesWorksheet.Cell(1, 6).Value = "NextYearQuarterRate";
                    propertyTypesWorksheet.Cell(1, 7).Value = "NextYearWarrantRate";
                    propertyTypesWorksheet.Cell(1, 8).Value = "SabhaId";

                    foreach (var (item, index) in propertyTypes.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        propertyTypesWorksheet.Cell(row, 1).Value = item.Id;
                        propertyTypesWorksheet.Cell(row, 2).Value = item.PropertyTypeName;
                        propertyTypesWorksheet.Cell(row, 3).Value = item.QuarterRate;
                        propertyTypesWorksheet.Cell(row, 4).Value = item.WarrantRate;
                        propertyTypesWorksheet.Cell(row, 5).Value = item.PropertyTypeNo;
                        propertyTypesWorksheet.Cell(row, 6).Value = item.NextYearQuarterRate;
                        propertyTypesWorksheet.Cell(row, 7).Value = item.NextYearWarrantRate;
                        propertyTypesWorksheet.Cell(row, 8).Value = item.SabhaId;


                    }



                    descriptionWorksheet.Cell(1, 1).Value = "DescriptionId"; // First row, first column
                    descriptionWorksheet.Cell(1, 2).Value = "Description";
                    descriptionWorksheet.Cell(1, 3).Value = "DescriptionNo";
                    descriptionWorksheet.Cell(1, 4).Value = "SabhaId";

                    foreach (var (item, index) in descriptions.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        descriptionWorksheet.Cell(row, 1).Value = item.Id;
                        descriptionWorksheet.Cell(row, 2).Value = item.DescriptionText;
                        descriptionWorksheet.Cell(row, 3).Value = item.DescriptionNo;
                        descriptionWorksheet.Cell(row, 4).Value = item.SabhaId;


                    }

                    // Define named range for Property Types
                    // Define named range for Property Types including both PropertyTypeId and PropertyTypeName
                    var propertyTypeTableRange = propertyTypesWorksheet.Range("A2:A" + (propertyTypes.Count() + 1)); // A2:A for PropertyTypeId, B2:B for PropertyTypeName
                    propertyTypeTableRange.AddToNamed("PropertyTypeIds", XLScope.Workbook);


                    var propertyTypeRange = propertyTypesWorksheet.Range("A2:E" + (propertyTypes.Count() + 1)); // Adjust based on actual range
                    propertyTypeRange.AddToNamed("PropertyTypeNames", XLScope.Workbook);




                    // Define named range for descriptions
                    var descriptionTableRange = descriptionWorksheet.Range("A2:A" + (descriptions.Count() + 1)); 
                    descriptionTableRange.AddToNamed("DescriptionIds", XLScope.Workbook);

                    var descriptionRange = descriptionWorksheet.Range("A2:C" + (descriptions.Count() + 1));
                    descriptionRange.AddToNamed("Descriptions", XLScope.Workbook);


                    assessmentWorksheet.Cell(1, 1).Value = "Ward";
                    assessmentWorksheet.Cell(1, 2).Value = "Street";
                    assessmentWorksheet.Cell(1, 3).Value = "KFormId";
                    assessmentWorksheet.Cell(1, 4).Value = "AssessmentNo";
                    assessmentWorksheet.Cell(1, 5).Value = "ObsoleteNumber";

                    assessmentWorksheet.Cell(1, 6).Value = "OwnerNIC";
                    assessmentWorksheet.Cell(1, 7).Value = "OwnerEmail";
                    assessmentWorksheet.Cell(1, 8).Value = "OwnerName";

                    assessmentWorksheet.Cell(1, 9).Value = "CurrentAllocation";
                    assessmentWorksheet.Cell(1, 10).Value = "NewAllocationAmount";

                    //assessmentWorksheet.Cell(1, 11).Value = "#";

                    assessmentWorksheet.Cell(1, 12).Value = "CurrentPropertyType";
                    assessmentWorksheet.Cell(1, 13).Value = "PropertyTypeNo";

                    assessmentWorksheet.Cell(1, 14).Value = "NewPropertyTypeId";
                    assessmentWorksheet.Cell(1, 15).Value = "NewPropertyType";
                    assessmentWorksheet.Cell(1, 16).Value = "NewPropertyTypeNo";

                    //assessmentWorksheet.Cell(1, 14).Value = "#";

                    assessmentWorksheet.Cell(1, 17).Value = "CurrentDescription";
                    assessmentWorksheet.Cell(1, 18).Value = "CurrentDescriptionNo";

                    assessmentWorksheet.Cell(1, 19).Value = "NewDescriptionId";
                    assessmentWorksheet.Cell(1, 20).Value = "NewDescriptionNo";
                    assessmentWorksheet.Cell(1, 21).Value = "NewDescription";

                    foreach (var (item, index) in assessments.Select((item, index) => (item, index)))
                    {

                       

                        int row = index + 2;
                        assessmentWorksheet.Cell(row, 1).Value = item.Street!.Ward != null ? item.Street.Ward!.WardName : "";
                        assessmentWorksheet.Cell(row, 2).Value = item.Street != null ? item.Street.StreetName : "";
                        assessmentWorksheet.Cell(row, 3).Value = item.Id;
                        assessmentWorksheet.Cell(row, 4).Value = item.AssessmentNo;
                        assessmentWorksheet.Cell(row, 5).Value = item.Obsolete;

                        

                        assessmentWorksheet.Cell(row, 8).Value = "";


                        if (item.IsPartnerUpdated)
                        {
                            var partner =  _unitOfWork.Partners.GetByIdSynchronously(item.PartnerId!.Value);

                            if (partner != null)
                            {
                                assessmentWorksheet.Cell(row, 6).Value = partner.NicNumber;
                                assessmentWorksheet.Cell(row, 7).Value = partner.Email;
                                assessmentWorksheet.Cell(row, 8).Value = partner.Name;
                            }
                        }
                        else
                        {
                            assessmentWorksheet.Cell(row, 6).Value = item.AssessmentTempPartner != null ? item.AssessmentTempPartner!.NICNumber : "-";
                            assessmentWorksheet.Cell(row, 7).Value = item.AssessmentTempPartner != null ? item.AssessmentTempPartner!.Email : "-";
                            assessmentWorksheet.Cell(row, 8).Value = item.AssessmentTempPartner != null ? item.AssessmentTempPartner!.Name : "-";
                        }


                        assessmentWorksheet.Cell(row, 9).Value = item.Allocation != null ? item.Allocation!.AllocationAmount : "-";
                        assessmentWorksheet.Cell(row, 10).Value = "";
                        assessmentWorksheet.Cell(row, 10).Style.Protection.Locked = false;

                        assessmentWorksheet.Cell(row, 12).Value = item.AssessmentPropertyType != null ? item.AssessmentPropertyType!.PropertyTypeName : "";
                        assessmentWorksheet.Cell(row, 13).Value = item.AssessmentPropertyType != null ? item.AssessmentPropertyType!.PropertyTypeNo : "";


                        //// Apply drop down for the new property type selection
                        var propertyTypeIdCell = assessmentWorksheet.Cell(row, 14); 
                        propertyTypeIdCell.CreateDataValidation().List("PropertyTypeIds");

                        // In the NewPropertyTypeId column (column 10)
                        var propertyTypeCell = assessmentWorksheet.Cell(row, 15);
                        // Add VLOOKUP formula to look up PropertyTypeId based on the selected PropertyTypeName in column 8
                        propertyTypeCell.FormulaA1 = $"=VLOOKUP(N{row}, PropertyTypeNames, 2, FALSE)"; // K is the NewPropertyType column

                        // In the NewPropertyTypeId column (column 10)
                        var propertyTypeNoCell = assessmentWorksheet.Cell(row, 16);
                        // Add VLOOKUP formula to look up PropertyTypeId based on the selected PropertyTypeName in column 8
                        propertyTypeNoCell.FormulaA1 = $"=VLOOKUP(N{row}, PropertyTypeNames, 5, FALSE)"; // K is the NewPropertyType column

                        propertyTypeIdCell.Style.Protection.Locked = false;

                        //assessmentWorksheet.Cell(row, 17).Value = "";

                        assessmentWorksheet.Cell(row, 17).Value = item.Description != null ? item.Description!.DescriptionText : "";
                        assessmentWorksheet.Cell(row, 18).Value = item.Description != null ? item.Description!.DescriptionNo : "";


                        var descriptionTypeIdCell = assessmentWorksheet.Cell(row, 19);
                        descriptionTypeIdCell.CreateDataValidation().List("DescriptionIds");


                        var descriptionTypeNoCell = assessmentWorksheet.Cell(row, 20);
                        descriptionTypeNoCell.FormulaA1 = $"=VLOOKUP(S{row}, Descriptions, 3, FALSE)";

                        var descriptionTypeCell = assessmentWorksheet.Cell(row, 21);
                        descriptionTypeCell.FormulaA1 = $"=VLOOKUP(S{row}, Descriptions, 2, FALSE)";

                        descriptionTypeIdCell.Style.Protection.Locked = false;


                    }

                    offficeWorksheet.SheetView.FreezeRows(1);
                    wardsWorksheet.SheetView.FreezeRows(1);
                    streetWorksheet.SheetView.FreezeRows(1);
                    propertyTypesWorksheet.SheetView.FreezeRows(1);
                    descriptionWorksheet.SheetView.FreezeRows(1);
                    assessmentWorksheet.SheetView.FreezeRows(1);


                    offficeWorksheet.Column(1).Style.Protection.SetLocked(true);
                    offficeWorksheet.Row(1).Style.Protection.SetLocked(true);
                    offficeWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    offficeWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    wardsWorksheet.Column(1).Style.Protection.SetLocked(true);
                    wardsWorksheet.Row(1).Style.Protection.SetLocked(true);
                    wardsWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    wardsWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    streetWorksheet.Column(1).Style.Protection.SetLocked(true);
                    streetWorksheet.Row(1).Style.Protection.SetLocked(true);
                    streetWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    streetWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    propertyTypesWorksheet.Column(1).Style.Protection.SetLocked(true);
                    propertyTypesWorksheet.Row(1).Style.Protection.SetLocked(true);
                    propertyTypesWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    propertyTypesWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);


                    foreach (IXLColumn column in offficeWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in wardsWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }
                    foreach (IXLColumn column in streetWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in propertyTypesWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in descriptionWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in assessmentWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }


                    var maxDescriptionColumnWidth = descriptionWorksheet.Column(2).Width;
                    assessmentWorksheet.Column(21).Width = maxDescriptionColumnWidth;

                    offficeWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightGreen;
                    wardsWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightBlue;
                    streetWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightCoral;
                    propertyTypesWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                    using (var memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        var uniqueFileName = $"asmt_list_for_renewal_{sabhaId}_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";

                        return (true, uniqueFileName, fileBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null);
            }
        }

        public async Task<(bool, string, byte[])> GetExcelForNew(int sabhaId, HTokenClaim token)
        {
            try
            {
                var YourPassword = "spU*#Spe6i0U4os4ip_0afonUqUPh!4oyI$a4l&a";
                var officess = await _officeService.getAllOfficesForSabhaId(sabhaId);
                var wards = await _unitOfWork.Wards.GetAllForSabha(sabhaId);
                var streets = await _unitOfWork.Streets.GetAllForSabha(sabhaId);
                var propertyTypes = await _unitOfWork.AssessmentPropertyTypes.GetAllForSabha(sabhaId);
                var descriptions = await _unitOfWork.Descriptions.GetAllForSabha(sabhaId);


                //var assessments = await _unitOfWork.Assessments.GetAssessmentForRenewal(sabhaId);

                using (var workbook = new XLWorkbook())
                {
                    var offficeWorksheet = workbook.Worksheets.Add("Offices");
                    var wardsWorksheet = workbook.Worksheets.Add("Wards");
                    var streetWorksheet = workbook.Worksheets.Add("Streets");
                    var propertyTypesWorksheet = workbook.Worksheets.Add("PropertyTypes");
                    var descriptionWorksheet = workbook.Worksheets.Add("PropertyDescriptions");

                    var assessmentWorksheet = workbook.Worksheets.Add("NewAssessmentData");
                    var worksheet = workbook.Worksheets.Add(token.sabhaId.ToString());

                    //var protection = offficeWorksheet.Protect("YourPassword");
                    //protection = wardsWorksheet.Protect("YourPassword");
                    //protection = streetWorksheet.Protect("YourPassword");
                    //protection = propertyTypesWorksheet.Protect("YourPassword");
                    //protection = descriptionWorksheet.Protect("YourPassword");
                    //var assessmentSheetprotection = assessmentWorksheet.Protect("YourPassword");

                    var allowedElements = XLSheetProtectionElements.FormatColumns
                       | XLSheetProtectionElements.AutoFilter | XLSheetProtectionElements.Sort
                       | XLSheetProtectionElements.FormatEverything | XLSheetProtectionElements.FormatColumns
                       | XLSheetProtectionElements.SelectUnlockedCells | XLSheetProtectionElements.SelectEverything;


                    offficeWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    wardsWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    streetWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    propertyTypesWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    descriptionWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    assessmentWorksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);
                    worksheet.Protect(YourPassword, XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);

                    //var allowedElements = XLSheetProtectionElements.FormatColumns
                    //    |XLSheetProtectionElements.AutoFilter| XLSheetProtectionElements.Sort 
                    //    |XLSheetProtectionElements.FormatEverything | XLSheetProtectionElements.FormatColumns
                    //    |XLSheetProtectionElements.SelectUnlockedCells  |XLSheetProtectionElements.SelectEverything;


                    //var assessmentSheetprotection = assessmentWorksheet.Protect("YourPassword", XLProtectionAlgorithm.Algorithm.SimpleHash, allowedElements);


                    wardsWorksheet.Range("A1:F1").SetAutoFilter();
                    streetWorksheet.Range("A1:G1").SetAutoFilter();
                    propertyTypesWorksheet.Range("A1:H1").SetAutoFilter();
                    descriptionWorksheet.Range("A1:D1").SetAutoFilter();

                    assessmentWorksheet.Range("A1:T1").SetAutoFilter();


                    offficeWorksheet.Cell(1, 1).Value = "OfficeId"; // First row, first column
                    offficeWorksheet.Cell(1, 2).Value = "Office Name";
                    offficeWorksheet.Cell(1, 3).Value = "SabhaId";

                    foreach (var (item, index) in officess.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        offficeWorksheet.Cell(row, 1).Value = item.ID;
                        offficeWorksheet.Cell(row, 2).Value = item.NameEnglish;
                        offficeWorksheet.Cell(row, 3).Value = item.SabhaID;
                    }


                    wardsWorksheet.Cell(1, 1).Value = "WardId"; // First row, first column
                    wardsWorksheet.Cell(1, 2).Value = "WardName";
                    wardsWorksheet.Cell(1, 3).Value = "WardCode";
                    wardsWorksheet.Cell(1, 4).Value = "WardNo";
                    wardsWorksheet.Cell(1, 5).Value = "OfficeId";
                    wardsWorksheet.Cell(1, 6).Value = "SabhaId";

                    foreach (var (item, index) in wards.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        wardsWorksheet.Cell(row, 1).Value = item.Id;
                        wardsWorksheet.Cell(row, 2).Value = item.WardName;
                        wardsWorksheet.Cell(row, 3).Value = item.WardCode;
                        wardsWorksheet.Cell(row, 4).Value = item.WardNo;
                        wardsWorksheet.Cell(row, 5).Value = item.OfficeId;
                        wardsWorksheet.Cell(row, 6).Value = item.SabhaId;
                    }

                    streetWorksheet.Cell(1, 1).Value = "StreetId"; // First row, first column
                    streetWorksheet.Cell(1, 2).Value = "StreetName";
                    streetWorksheet.Cell(1, 3).Value = "StreetNo";
                    streetWorksheet.Cell(1, 4).Value = "StreetCode";
                    streetWorksheet.Cell(1, 5).Value = "WardName";
                    streetWorksheet.Cell(1, 6).Value = "WardNo";
                    streetWorksheet.Cell(1, 7).Value = "WardId";

                    foreach (var (item, index) in streets.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        streetWorksheet.Cell(row, 1).Value = item.Id;
                        streetWorksheet.Cell(row, 2).Value = item.StreetName;
                        streetWorksheet.Cell(row, 3).Value = item.StreetNo;
                        streetWorksheet.Cell(row, 4).Value = item.StreetCode;
                        streetWorksheet.Cell(row, 5).Value = item.Ward!.WardName;
                        streetWorksheet.Cell(row, 6).Value = item.Ward.WardNo;
                        streetWorksheet.Cell(row, 7).Value = item.WardId;


                    }

                    propertyTypesWorksheet.Cell(1, 1).Value = "PropertyTypeId"; // First row, first column
                    propertyTypesWorksheet.Cell(1, 2).Value = "PropertyType";
                    propertyTypesWorksheet.Cell(1, 3).Value = "QuarterRate";
                    propertyTypesWorksheet.Cell(1, 4).Value = "WarrantRate";
                    propertyTypesWorksheet.Cell(1, 5).Value = "PropertyTypeNo";
                    propertyTypesWorksheet.Cell(1, 6).Value = "NextYearQuarterRate";
                    propertyTypesWorksheet.Cell(1, 7).Value = "NextYearWarrantRate";
                    propertyTypesWorksheet.Cell(1, 8).Value = "SabhaId";

                    foreach (var (item, index) in propertyTypes.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        propertyTypesWorksheet.Cell(row, 1).Value = item.Id;
                        propertyTypesWorksheet.Cell(row, 2).Value = item.PropertyTypeName;
                        propertyTypesWorksheet.Cell(row, 3).Value = item.QuarterRate;
                        propertyTypesWorksheet.Cell(row, 4).Value = item.WarrantRate;
                        propertyTypesWorksheet.Cell(row, 5).Value = item.PropertyTypeNo;
                        propertyTypesWorksheet.Cell(row, 6).Value = item.NextYearQuarterRate;
                        propertyTypesWorksheet.Cell(row, 7).Value = item.NextYearWarrantRate;
                        propertyTypesWorksheet.Cell(row, 8).Value = item.SabhaId;


                    }



                    descriptionWorksheet.Cell(1, 1).Value = "DescriptionId"; // First row, first column
                    descriptionWorksheet.Cell(1, 2).Value = "Description";
                    descriptionWorksheet.Cell(1, 3).Value = "DescriptionNo";
                    descriptionWorksheet.Cell(1, 4).Value = "SabhaId";

                    foreach (var (item, index) in descriptions.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        descriptionWorksheet.Cell(row, 1).Value = item.Id;
                        descriptionWorksheet.Cell(row, 2).Value = item.DescriptionText;
                        descriptionWorksheet.Cell(row, 3).Value = item.DescriptionNo;
                        descriptionWorksheet.Cell(row, 4).Value = item.SabhaId;


                    }

                    var officeTableRange = offficeWorksheet.Range("A2:A" + (wards.Count() + 1)); 
                    officeTableRange.AddToNamed("OfficeIds", XLScope.Workbook);

                    var officeRange = offficeWorksheet.Range("A2:C" + (officess.Count() + 1)); 
                    officeRange.AddToNamed("Offices", XLScope.Workbook);


                    var wardGroups = wards.GroupBy(s => s.OfficeId);
                    foreach (var group in wardGroups)
                    {
                        var officeId = group.Key;
                        var wardsInOfficeRange = wardsWorksheet.Range(
                            $"A{wards.ToList().FindIndex(w => w.OfficeId == officeId) + 2}:A{wards.ToList().FindLastIndex(w => w.OfficeId == officeId) + 2}"
                        );
                        wardsInOfficeRange.AddToNamed($"Wards_office{officeId}", XLScope.Workbook);
                    }

                    var streetGroups = streets.GroupBy(s => s.WardId);
                    foreach (var group in streetGroups)
                    {
                        var wardId = group.Key;
                        var streetsInWardRange = streetWorksheet.Range(
                            $"A{streets.ToList().FindIndex(s => s.WardId == wardId) + 2}:A{streets.ToList().FindLastIndex(s => s.WardId == wardId) + 2}"
                        );
                        streetsInWardRange.AddToNamed($"Streets_Ward{wardId}", XLScope.Workbook);
                    }

                    var StreetRange = streetWorksheet.Range("A2:D" + (streets.Count() + 1)); // Adjust based on actual range
                    StreetRange.AddToNamed("Streets", XLScope.Workbook);

                    //var wardTableRange = wardsWorksheet.Range("A2:A" + (wards.Count() + 1)); // A2:A for PropertyTypeId, B2:B for PropertyTypeName
                    //wardTableRange.AddToNamed("WardIds", XLScope.Workbook);

                    var wardRange = wardsWorksheet.Range("A2:D" + (wards.Count() + 1)); // Adjust based on actual range
                    wardRange.AddToNamed("WardNames", XLScope.Workbook);

                    // Define named range for Property Types
                    // Define named range for Property Types including both PropertyTypeId and PropertyTypeName
                    var propertyTypeTableRange = propertyTypesWorksheet.Range("A2:A" + (propertyTypes.Count() + 1)); // A2:A for PropertyTypeId, B2:B for PropertyTypeName
                    propertyTypeTableRange.AddToNamed("PropertyTypeIds", XLScope.Workbook);


                    var propertyTypeRange = propertyTypesWorksheet.Range("A2:E" + (propertyTypes.Count() + 1)); // Adjust based on actual range
                    propertyTypeRange.AddToNamed("PropertyTypeNames", XLScope.Workbook);




                    // Define named range for descriptions
                    var descriptionTableRange = descriptionWorksheet.Range("A2:A" + (descriptions.Count() + 1));
                    descriptionTableRange.AddToNamed("DescriptionIds", XLScope.Workbook);

                    var descriptionRange = descriptionWorksheet.Range("A2:C" + (descriptions.Count() + 1));
                    descriptionRange.AddToNamed("Descriptions", XLScope.Workbook);

                    // New column name arrangement
                    assessmentWorksheet.Cell(1, 1).Value = "SabhaId";
                    assessmentWorksheet.Cell(1, 2).Value = "OfficeId";
                    assessmentWorksheet.Cell(1, 3).Value = "Office";
                    assessmentWorksheet.Cell(1, 4).Value = "WardId";
                    assessmentWorksheet.Cell(1, 5).Value = "WardNo";
                    assessmentWorksheet.Cell(1, 6).Value = "Ward";
                    assessmentWorksheet.Cell(1, 7).Value = "StreetId";
                    assessmentWorksheet.Cell(1, 8).Value = "StreetNo";
                    assessmentWorksheet.Cell(1, 9).Value = "StreetName";

                    assessmentWorksheet.Cell(1, 10).Value = "AssessmentNo";
                    assessmentWorksheet.Cell(1, 11).Value = "ObsoleteNumber";

                    assessmentWorksheet.Cell(1, 12).Value = "OwnerNIC";
                    assessmentWorksheet.Cell(1, 13).Value = "OwnerEmail";
                    assessmentWorksheet.Cell(1, 14).Value = "MobileNo";
                    assessmentWorksheet.Cell(1, 15).Value = "OwnerStreet1";
                    assessmentWorksheet.Cell(1, 16).Value = "OwnerName";
                    assessmentWorksheet.Cell(1, 17).Value = "SubOwnerName";

                    assessmentWorksheet.Cell(1, 18).Value = "PropertyAddress";
                    assessmentWorksheet.Cell(1, 19).Value = "";

                    assessmentWorksheet.Cell(1, 20).Value = "Allocation";
                    assessmentWorksheet.Cell(1, 21).Value = "PropertyTypeId";
                    assessmentWorksheet.Cell(1, 22).Value = "PropertyTypeNo";
                    assessmentWorksheet.Cell(1, 23).Value = "PropertyType";

                    assessmentWorksheet.Cell(1, 24).Value = "DescriptionId";
                    assessmentWorksheet.Cell(1, 25).Value = "DescriptionNo";
                    assessmentWorksheet.Cell(1, 26).Value = "Description";




                    for (int index = 0; index < 10000; index++)

                        //foreach (var (item, index) in assessments.Select((item, index) => (item, index)))
                    {
                        int row = index + 2;
                        assessmentWorksheet.Cell(row, 1).Value = token.sabhaId;

                        var officeIdCell = assessmentWorksheet.Cell(row, 2);
                        officeIdCell.CreateDataValidation().List("OfficeIds");

                        var officeCell = assessmentWorksheet.Cell(row, 3);
                        officeCell.FormulaA1 = $"=VLOOKUP(B{row}, Offices, 2, FALSE)";

                       

                        officeIdCell.Style.Protection.Locked = false;

                        var wardIdCell = assessmentWorksheet.Cell(row, 4);
                        //wardIdCell.CreateDataValidation().List("WardIds");
                        wardIdCell.CreateDataValidation().List($"=INDIRECT(\"Wards_office\" & B{row})");

                        var wardCell = assessmentWorksheet.Cell(row, 5);
                        wardCell.FormulaA1 = $"=VLOOKUP(D{row}, WardNames, 4, FALSE)";

                        var wardNoCell = assessmentWorksheet.Cell(row, 6);
                        wardNoCell.FormulaA1 = $"=VLOOKUP(D{row}, WardNames, 2, FALSE)";

                                              

                        wardIdCell.Style.Protection.Locked = false;



                        var streetIdCell = assessmentWorksheet.Cell(row, 7);

                        // Create a dependent data validation for StreetId
                        streetIdCell.CreateDataValidation().List($"=INDIRECT(\"Streets_Ward\" & D{row})");

                        // Use VLOOKUP to automatically fetch the street name and street number
                        var streetNameCell = assessmentWorksheet.Cell(row, 8);
                        streetNameCell.FormulaA1 = $"=VLOOKUP(G{row}, Streets, 3, FALSE)";

                        var streetNoCell = assessmentWorksheet.Cell(row, 9);
                        streetNoCell.FormulaA1 = $"=VLOOKUP(G{row}, Streets, 2, FALSE)";


                        streetIdCell.Style.Protection.Locked = false;

                        assessmentWorksheet.Cell(row, 10).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 11).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 12).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 13).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 14).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 15).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 16).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 17).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 18).Style.Protection.Locked = false;
                        //assessmentWorksheet.Cell(row, 19).Style.Protection.Locked= false;
                        assessmentWorksheet.Cell(row, 20).Style.Protection.Locked= false;

                        /*property types*/


                        var propertyTypeIdCell = assessmentWorksheet.Cell(row, 21);
                        propertyTypeIdCell.CreateDataValidation().List("PropertyTypeIds");

                        var propertyTypeNoCell = assessmentWorksheet.Cell(row, 22);
                        propertyTypeNoCell.FormulaA1 = $"=VLOOKUP(U{row}, PropertyTypeNames, 5, FALSE)";

                        var propertyTypeCell = assessmentWorksheet.Cell(row, 23);
                        propertyTypeCell.FormulaA1 = $"=VLOOKUP(U{row}, PropertyTypeNames, 2, FALSE)";

                        propertyTypeIdCell.Style.Protection.Locked = false;


                         /*description*/


                        var descriptionTypeIdCell = assessmentWorksheet.Cell(row, 24);
                        descriptionTypeIdCell.CreateDataValidation().List("DescriptionIds");


                        var descriptionTypeNoCell = assessmentWorksheet.Cell(row, 25);
                        descriptionTypeNoCell.FormulaA1 = $"=VLOOKUP(X{row}, Descriptions, 3, FALSE)";

                        var descriptionTypeCell = assessmentWorksheet.Cell(row, 26);
                        descriptionTypeCell.FormulaA1 = $"=VLOOKUP(X{row}, Descriptions, 2, FALSE)";

                        descriptionTypeIdCell.Style.Protection.Locked = false;


                    }

                    offficeWorksheet.SheetView.FreezeRows(1);
                    wardsWorksheet.SheetView.FreezeRows(1);
                    streetWorksheet.SheetView.FreezeRows(1);
                    propertyTypesWorksheet.SheetView.FreezeRows(1);
                    descriptionWorksheet.SheetView.FreezeRows(1);
                    assessmentWorksheet.SheetView.FreezeRows(1);

                    offficeWorksheet.Column(1).Style.Protection.SetLocked(true);
                    offficeWorksheet.Row(1).Style.Protection.SetLocked(true);
                    offficeWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    offficeWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    wardsWorksheet.Column(1).Style.Protection.SetLocked(true);
                    wardsWorksheet.Row(1).Style.Protection.SetLocked(true);
                    wardsWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    wardsWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    streetWorksheet.Column(1).Style.Protection.SetLocked(true);
                    streetWorksheet.Row(1).Style.Protection.SetLocked(true);
                    streetWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    streetWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);

                    propertyTypesWorksheet.Column(1).Style.Protection.SetLocked(true);
                    propertyTypesWorksheet.Row(1).Style.Protection.SetLocked(true);
                    propertyTypesWorksheet.CellsUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Justify);
                    propertyTypesWorksheet.CellsUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Justify);


                    foreach (IXLColumn column in offficeWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in wardsWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }
                    foreach (IXLColumn column in streetWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in propertyTypesWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in descriptionWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    foreach (IXLColumn column in assessmentWorksheet.ColumnsUsed())
                    {
                        column.AdjustToContents();
                    }

                    assessmentWorksheet.Column(1).Hide(); 
                    assessmentWorksheet.Column(19).Hide(); 

                    var maxOfficeColumnWidth = offficeWorksheet.Column(2).Width;
                    assessmentWorksheet.Column(3).Width = maxOfficeColumnWidth;

                    var maxWardColumnWidth = wardsWorksheet.Column(2).Width;
                    assessmentWorksheet.Column(4).Width = maxWardColumnWidth;
                    assessmentWorksheet.Column(5).Width = maxWardColumnWidth;
                    assessmentWorksheet.Column(6).Width = maxWardColumnWidth;

                    var maxStreetColumnWidth = streetWorksheet.Column(2).Width;
                    assessmentWorksheet.Column(9).Width = maxStreetColumnWidth;

                    assessmentWorksheet.Column(10).Width = 25;
                    var maxDescriptionColumnWidth = descriptionWorksheet.Column(2).Width;
                    assessmentWorksheet.Column(26).Width = maxDescriptionColumnWidth;

                    assessmentWorksheet.Column(12).Width = 16;
                    assessmentWorksheet.Column(13).Width = 30;
                    assessmentWorksheet.Column(14).Width = 20;
                    assessmentWorksheet.Column(15).Width = 25;
                    assessmentWorksheet.Column(16).Width =  maxDescriptionColumnWidth + 1;
                    assessmentWorksheet.Column(17).Width =  maxDescriptionColumnWidth + 1;
                    assessmentWorksheet.Column(18).Width = 25;
                    assessmentWorksheet.Column(23).Width = 25;

                    offficeWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightGreen;
                    wardsWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightBlue;
                    streetWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightCoral;
                    propertyTypesWorksheet.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                    using (var memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        var uniqueFileName = $"asmt_list_for_renewal_{sabhaId}_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";

                        return (true, uniqueFileName, fileBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null);
            }
        }
        public async Task<(bool, string)> CreateAssessmentRenewal(List<AssessmentRenewal> assessmentRenewals, HTokenClaim token)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetAssessmentForRenewal(token,null);

                bool hasToComit = false;
                foreach (var asmt in asmts)
                {
                   
                    if (asmt != null)
                    {



                        if (asmt.Allocation != null && asmt.AssessmentBalance != null)
                        {

                            var item = assessmentRenewals.Where(x => x.Id == asmt.Id).FirstOrDefault();

                            if (item != null)
                            {
                                AssessmentPropertyType assessmentPropertyType = asmt.AssessmentPropertyType!;


                                if (item.NewAllocation.HasValue)
                                {
                                    asmt.Allocation!.NextYearAllocationAmount = item.NewAllocation;

                                }
                                else
                                {
                                    asmt.Allocation!.NextYearAllocationAmount = asmt.Allocation!.AllocationAmount;
                                }



                                if (item.NewPropertyTypeId.HasValue)
                                {
                                 
                                    asmt.NextYearPropertyTypeId = item.NewPropertyTypeId.Value;
                                    assessmentPropertyType = await _unitOfWork.AssessmentPropertyTypes.GetByIdAsync(item.NewPropertyTypeId.Value);


                                }
                                else if(!asmt.NextYearPropertyTypeId.HasValue)
                                {
                                    asmt.NextYearPropertyTypeId = asmt.PropertyTypeId;
                                }
                                {

                                }
                               
                                if(item.NewDescriptionId.HasValue)
                                {
                                    asmt.NextYearDescriptionId = item.NewDescriptionId.Value;
                                }
                                else if(!asmt.NextYearDescriptionId.HasValue)
                                {
                                    asmt.NextYearDescriptionId = asmt.DescriptionId;
                                }
                               

                                if (assessmentPropertyType == null)
                                {
                                    throw new InvalidOperationException("Unable To Find Property Type");
                                }
                               

                                //decimal precision handling
                                var annualAmmount = (asmt.Allocation.NextYearAllocationAmount * (assessmentPropertyType.QuarterRate / 100));
                                var qAmount = annualAmmount / 4;

                                var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                                var remainder = annualAmmount - (roundedValue * 4);



                                if (asmt.AssessmentBalance.NQ1 != null)
                                {

                                    asmt.AssessmentBalance.NQ1.Amount = roundedValue;
                                }
                                else
                                {

                                    var nQ1 = new NQ1
                                    {
                                        Id = null,
                                        Amount = roundedValue,
                                        //ByExcessDeduction = 0,
                                        //Paid = 0,
                                        //Discount = 0,
                                        //Warrant = 0,
                                        //StartDate = todayTime,
                                        //EndDate = null,
                                        BalanceId = asmt.AssessmentBalance.Id,
                                        //IsCompleted = false,
                                        //IsOver = false,
                                    };

                                    await _unitOfWork.NQ1s.AddAsync(nQ1);

                                }
                                if (asmt.AssessmentBalance.NQ2 != null)
                                {
                                    asmt.AssessmentBalance.NQ2.Amount = roundedValue;
                                }
                                else
                                {
                                    var nQ2 = new NQ2
                                    {
                                        Id = null,
                                        Amount = roundedValue,
                                        //ByExcessDeduction = 0,
                                        //Paid = 0,
                                        //Discount = 0,
                                        //Warrant = 0,
                                        //StartDate = null,
                                        //EndDate = null,
                                        BalanceId = asmt.AssessmentBalance.Id,
                                        //IsCompleted = false,
                                        //IsOver = false,
                                    };
                                    await _unitOfWork.NQ2s.AddAsync(nQ2);

                                }

                                if (asmt.AssessmentBalance.NQ3 != null)
                                {
                                    asmt.AssessmentBalance.NQ3.Amount = roundedValue;
                                }
                                else
                                {
                                    var nQ3 = new NQ3
                                    {
                                        Id = null,
                                        Amount = roundedValue,
                                        //ByExcessDeduction = 0,
                                        //Paid = 0,
                                        //Discount = 0,
                                        //Warrant = 0,
                                        //StartDate = null,
                                        //EndDate = null,
                                        BalanceId = asmt.AssessmentBalance.Id,
                                        //IsCompleted = false,
                                        //IsOver = false,
                                    };

                                    await _unitOfWork.NQ3s.AddAsync(nQ3);
                                }

                                if (asmt.AssessmentBalance.NQ4 != null)
                                {
                                    asmt.AssessmentBalance.NQ4.Amount = roundedValue + remainder;
                            }
                                else
                                {


                                    var nQ4 = new NQ4
                                    {
                                        Id = null,
                                        Amount = roundedValue + remainder,
                                        //ByExcessDeduction = 0,
                                        //Paid = 0,
                                        //Discount = 0,
                                        //Warrant = 0,
                                        //StartDate = null,
                                        //EndDate = null,
                                        BalanceId = asmt.AssessmentBalance.Id,
                                        //IsCompleted = false,
                                        //IsOver = false,
                                    };
                                    await _unitOfWork.NQ4s.AddAsync(nQ4);
                                }


                                hasToComit = true;  

                            }
                          
                        }
                    }
                    else
                    {
                        throw new GeneralException("Unable Find Assessment");
                    }

                }


                if (hasToComit) {
                    await _unitOfWork.CommitAsync();
                }

                return (true, "Assessment Renewal Created Successfully");

            }catch(Exception ex)
            {
                return (false, ex.Message.ToString());
            }
        }


        public async Task<bool> UpdateAssessmentNextYearQuarters(HTokenClaim token, int? propertyTypeId)
        {
            try
            {
                var asmts = await _unitOfWork.Assessments.GetAssessmentForRenewal(token,propertyTypeId);

                bool hasToComit = false;
                foreach (var asmt in asmts)
                {

                    if (asmt != null)
                    {



                        if (asmt.Allocation != null && asmt.AssessmentBalance != null)
                        {

                            //var item = assessmentRenewals.Where(x => x.Id == asmt.Id).FirstOrDefault();




                            if (!asmt.Allocation!.NextYearAllocationAmount.HasValue)
                            {
                                asmt.Allocation!.NextYearAllocationAmount = asmt.Allocation!.AllocationAmount;

                            }

                            if (!asmt.NextYearPropertyTypeId.HasValue)
                            {

                                asmt.NextYearPropertyTypeId = asmt.PropertyTypeId;

                            }

                            if (!asmt.NextYearDescriptionId.HasValue)
                            {

                                asmt.NextYearDescriptionId = asmt.DescriptionId;

                            }

                            AssessmentPropertyType assessmentPropertyType = await _unitOfWork.AssessmentPropertyTypes.GetByIdAsync(asmt.NextYearPropertyTypeId!.Value);


                            //decimal precision handling
                            var annualAmmount = (asmt.Allocation.NextYearAllocationAmount * (assessmentPropertyType.NextYearQuarterRate / 100));
                            var qAmount = annualAmmount / 4;

                            var roundedValue = Math.Round((decimal)qAmount, 2, MidpointRounding.AwayFromZero);

                            var remainder = annualAmmount - (roundedValue * 4);


                            if (asmt.AssessmentBalance.NQ1 != null)
                            {

                                asmt.AssessmentBalance.NQ1.Amount = roundedValue;
                            }
                            else
                            {

                                var nQ1 = new NQ1
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = todayTime,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };

                                await _unitOfWork.NQ1s.AddAsync(nQ1);
                               
                            }
                            if (asmt.AssessmentBalance.NQ2 != null)
                            {
                                asmt.AssessmentBalance.NQ2.Amount = roundedValue;
                            }
                            else
                            {
                                var nQ2 = new NQ2
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = null,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };
                                await _unitOfWork.NQ2s.AddAsync(nQ2);
                             
                            }

                            if (asmt.AssessmentBalance.NQ3 != null)
                            {
                                asmt.AssessmentBalance.NQ3.Amount = roundedValue;
                            }
                            else
                            {
                                var nQ3 = new NQ3
                                {
                                    Id = null,
                                    Amount = roundedValue,
                                    //ByExcessDeduction = 0,
                                    //Paid = 0,
                                    //Discount = 0,
                                    //Warrant = 0,
                                    //StartDate = null,
                                    //EndDate = null,
                                    BalanceId = asmt.AssessmentBalance.Id,
                                    //IsCompleted = false,
                                    //IsOver = false,
                                };

                                await _unitOfWork.NQ3s.AddAsync(nQ3);
                            }

                            if (asmt.AssessmentBalance.NQ4 != null)
                            {
                                asmt.AssessmentBalance.NQ4.Amount = roundedValue + remainder;
                            }
                            else
                            {

                            
                            var nQ4 = new NQ4
                            {
                                Id = null,
                                Amount = roundedValue + remainder,
                                //ByExcessDeduction = 0,
                                //Paid = 0,
                                //Discount = 0,
                                //Warrant = 0,
                                //StartDate = null,
                                //EndDate = null,
                                BalanceId = asmt.AssessmentBalance.Id,
                                //IsCompleted = false,
                                //IsOver = false,
                            };
                                await _unitOfWork.NQ4s.AddAsync(nQ4);
                            }

                        
                              
                             

                                hasToComit = true;

                            

                        }
                    }
                    else
                    {
                        throw new GeneralException("Unable Find Assessment");
                    }

                }


                if (hasToComit)
                {
                    await _unitOfWork.CommitAsync();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
