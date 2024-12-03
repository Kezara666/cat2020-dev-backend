using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.HRM.PersonalFile;

namespace CAT20.Core.Repositories.HRM.PersonalFile
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllForOffice(int officeid);
        Task<Employee> GetEmployeesBySabhaAndEMPNo(int sabhaid, string empNumber);
        Task<Employee> GetEmployeesBySabhaAndPhone(int sabhaid, string phoneNumber);
        Task<Employee> GetEmployeesBySabhaAndNIC(int sabhaid, string nicNumber);
        Task<IEnumerable<Employee>> GetAllForSabha(int sabhaid);

        //--------
        Task<Employee> GetBySabhaNICAsync(int sabhaId, string NIC);
        Task<Employee> GetBySabhaPhoneNoAsync(int sabhaId, string PhoneNo);
        //--------
    }
}
