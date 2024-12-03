using CAT20.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Services.HRM.PersonalFile;
using log4net;
using static ClosedXML.Excel.XLPredefinedFormat;
using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.Control;
using Irony.Parsing;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using CAT20.Core.Models.Vote;

namespace CAT20.Services.HRM.PersonalFile
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMUnitOfWork _unitOfWork;
        private readonly IAuditTrailUnitOfWork _auditTrailUnitOfWork;
        public EmployeeService(IHRMUnitOfWork unitOfWork, IAuditTrailUnitOfWork auditTrailUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auditTrailUnitOfWork = auditTrailUnitOfWork;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Employees.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _unitOfWork.Employees.GetAllEmployees();
        }

        public async Task<IEnumerable<Employee>> GetAllForOffice(int officeid)
        {
            return await _unitOfWork.Employees.GetAllForOffice(officeid);
        }

        public async Task<IEnumerable<Employee>> GetAllForSabha(int sabhhaid)
        {
            return await _unitOfWork.Employees.GetAllForSabha(sabhhaid);
        }
        public async Task<Employee> GetEmployeesBySabhaAndEMPNo(int sabhaid, string empNumber)
        {
            return await _unitOfWork.Employees.GetEmployeesBySabhaAndEMPNo(sabhaid, empNumber);
        }
        public async Task<Employee> GetEmployeesBySabhaAndPhone(int sabhaid, string phoneNumber)
        {
            return await _unitOfWork.Employees.GetEmployeesBySabhaAndPhone(sabhaid, phoneNumber);
        }

        public async Task<Employee> GetEmployeesBySabhaAndNIC(int sabhaid, string nicNumber)
        {
            return await _unitOfWork.Employees.GetEmployeesBySabhaAndNIC(sabhaid, nicNumber);
        }

        //27.09.2024
        public async Task<Employee> GetEmployeeById(int? id)
        {
            return await _unitOfWork.Employees.GetByIdAsync(id);
        }
        //End

        public async Task<Employee> SaveEmployee(Employee newEmployee)
        {
            try
            {
                if (Enum.IsDefined(typeof(Title), newEmployee.Title) &&
                    Enum.IsDefined(typeof(GenderID), newEmployee.GenderID)
                    //&&
                    //Enum.IsDefined(typeof(CivilStatus), newEmployee.CivilStatus) &&
                    //Enum.IsDefined(typeof(RailwayWarrant), newEmployee.RailwayWarrant) &&
                    //newEmployee.Addresses.All(a => Enum.IsDefined(typeof(AddressType), a.AddressType)) &&
                    //newEmployee.ServiceInfos.All(a => Enum.IsDefined(typeof(ServiceLevel), a.ServiceLevelID))
                    )

                {
                    //newEmployee.SabhaId = token.sabhaId;
                    //newEmployee.OfficeId = token.officeId;
                    //newEmployee.CreatedBy = token.userId;

                    await _unitOfWork.Employees
                        .AddAsync(newEmployee);
                    await _unitOfWork.CommitAsync();

                    return newEmployee;
                }
                else
                {
                    throw new Exception("Invalid field");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating employee", ex);
            }
        }
        //public async Task<Employee> UpdateEmployee(Employee employeeToUpdate)
        public async Task UpdateEmployee(Employee employeeToBeUpdate, Employee employee)
        {
            try
            {
                if (employeeToBeUpdate.StatusId == 1)
                {
                    //employeeToBeUpdate.EmployeeTypeID = employee.EmployeeTypeID;
                    employeeToBeUpdate.CarderStausID = employee.CarderStausID;
                    employeeToBeUpdate.NICNumber = employee.NICNumber;
                    employeeToBeUpdate.PassportNumber = employee.PassportNumber;
                    employeeToBeUpdate.PersonalFileNumber = employee.PersonalFileNumber;
                    employeeToBeUpdate.EmployeeNo = employee.EmployeeNo;
                    employeeToBeUpdate.PayNo = employee.PayNo;
                    employeeToBeUpdate.Title = employee.Title;
                    employeeToBeUpdate.Initials = employee.Initials;
                    employeeToBeUpdate.FirstName = employee.FirstName;
                    employeeToBeUpdate.MiddleName = employee.MiddleName;
                    employeeToBeUpdate.LastName = employee.LastName;
                    employeeToBeUpdate.FullName = employee.FullName;
                    employeeToBeUpdate.GenderID = employee.GenderID;
                    employeeToBeUpdate.DateOfBirth = employee.DateOfBirth;
                    employeeToBeUpdate.CivilStatus = employee.CivilStatus;
                    employeeToBeUpdate.MarriedDate = employee.MarriedDate;
                    employeeToBeUpdate.RailwayWarrant = employee.RailwayWarrant;
                    employeeToBeUpdate.MobileNo = employee.MobileNo;
                    employeeToBeUpdate.PersonalEmail = employee.PersonalEmail;
                    employeeToBeUpdate.PhotographPath = employee.PhotographPath;
                    //employeeToBeUpdate.SabhaId = employee.SabhaId;
                    //employeeToBeUpdate.OfficeId = employee.OfficeId;
                    employeeToBeUpdate.ProgrammeId = employee.ProgrammeId;
                    employeeToBeUpdate.ProjectId = employee.ProjectId;
                    employeeToBeUpdate.SubProjectId = employee.SubProjectId;


                    //employeeToBeUpdate.UpdatedAt = DateTime.Now;

                    //employeeToBeUpdate.CreatedAt = employee.CreatedAt;
                    //employeeToBeUpdate.UpdatedAt = employee.UpdatedAt;
                    //employeeToBeUpdate.CreatedBy = employee.CreatedBy;
                    //employeeToBeUpdate.UpdatedBy = employee.UpdatedBy;
                    //employeeToBeUpdate.StatusId = employee.StatusId;
                    //employeeToBeUpdate.CarderStatusDatas = employee.CarderStatusDatas;
                    //employeeToBeUpdate.Addresses = employee.Addresses;
                    //employeeToBeUpdate.SpouserInfos = employee.SpouserInfos;
                    //employeeToBeUpdate.ChildrenInfos = employee.ChildrenInfos;
                    //employeeToBeUpdate.ServiceInfos = employee.ServiceInfos;
                    //employeeToBeUpdate.SalaryInfos = employee.SalaryInfos;
                    //employeeToBeUpdate.NetSalaryAgents = employee.NetSalaryAgents;
                    //employeeToBeUpdate.OtherRemittanceAgents = employee.OtherRemittanceAgents;
                    //employeeToBeUpdate.SupportingDocuments = employee.SupportingDocuments;
                }

                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                //throw new Exception("Error updating employee", ex);
            }
        }

        public async Task<Employee> GetBySabhaNICAsync(int sabhaId, string NIC)
        {
            return await _unitOfWork.Employees.GetBySabhaNICAsync(sabhaId, NIC);
        }

        public async Task<Employee> GetBySabhaPhoneNoAsync(int sabhaId, string PhoneNo)
        {
            return await _unitOfWork.Employees.GetBySabhaPhoneNoAsync(sabhaId, PhoneNo);
        }

        public async Task DeleteEmployee(Employee employeeToDelete)
        {
            try
            {
                if (employeeToDelete != null)
                {
                    employeeToDelete.StatusId = 0;
                    await _unitOfWork.CommitAsync();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while soft deleting the employee.", ex);
            }
        }


    }
}