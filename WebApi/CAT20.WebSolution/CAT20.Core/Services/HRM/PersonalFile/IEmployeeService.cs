using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Common.Envelop;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Services.HRM.PersonalFile
{
    public interface IEmployeeService 
    {
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllForOffice(int officeid);
        Task<IEnumerable<Employee>> GetAllForSabha(int sabhaid);
        Task<Employee> GetEmployeesBySabhaAndEMPNo(int sabhaid, string empNumber);
        Task<Employee> GetEmployeesBySabhaAndPhone(int sabhaid, string phoneNumber);
        Task<Employee> GetEmployeesBySabhaAndNIC(int sabhaid, string nicNumber);
        Task<Employee> SaveEmployee(Employee newEmployee);
        //Task<Employee> UpdateEmployee(Employee newEmployee);
        Task UpdateEmployee(Employee employeeToBeUpdate, Employee employee);

        //--------
        Task<Employee> GetBySabhaNICAsync(int sabhaId, string NIC);
        Task<Employee> GetBySabhaPhoneNoAsync(int sabhaId, string PhoneNo);
        Task<Employee> GetEmployeeById(int? id);
        //--------
        Task DeleteEmployee(Employee employeeToDelete);

    }
}
