using CAT20.Core.Models.Control;
using CAT20.Core.Models.Enums;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.ShopRental;
using CAT20.Core.Repositories.HRM.PersonalFile;
using CAT20.Core.Repositories.ShopRental;
using log4net;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Data.Repositories.HRM.PersonalFile
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {

        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await HRMDbContext.Employees
                .Include(e => e.Addresses.Where(a => a.StatusId == 1))
                .Include(e => e.SpouserInfos.Where(s => s.StatusId == 1))
                .Include(e => e.ChildrenInfos.Where(c => c.StatusId == 1))
                .Include(e => e.ServiceInfos.Where(s => s.StatusId == 1))
                .Include(e => e.SalaryInfos.Where(s => s.StatusId == 1))
                .Include(e => e.NetSalaryAgents.Where(n => n.StatusId == 1))
                .Include(e => e.OtherRemittanceAgents.Where(o => o.StatusId == 1))
                .Include(e => e.SupportingDocuments.Where(s => s.StatusId == 1))
                .Where(e => e.Id == id && e.StatusId == 1)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await HRMDbContext.Employees
                .Include(e => e.Addresses.Where(a => a.StatusId == 1))
                .Include(e => e.SpouserInfos.Where(s => s.StatusId == 1))
                .Include(e => e.ChildrenInfos.Where(c => c.StatusId == 1))
                .Include(e => e.ServiceInfos.Where(s => s.StatusId == 1))
                .Include(e => e.SalaryInfos.Where(s => s.StatusId == 1))
                .Include(e => e.NetSalaryAgents.Where(n => n.StatusId == 1))
                .Include(e => e.OtherRemittanceAgents.Where(o => o.StatusId == 1))
                .Include(e => e.SupportingDocuments.Where(s => s.StatusId == 1))
                .Where(e => e.StatusId == 1)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllForOffice(int officeId)
        {
            return await HRMDbContext.Employees
                .Include(e => e.Addresses.Where(a => a.StatusId == 1))
                .Include(e => e.SpouserInfos.Where(s => s.StatusId == 1))
                .Include(e => e.ChildrenInfos.Where(c => c.StatusId == 1))
                .Include(e => e.ServiceInfos.Where(s => s.StatusId == 1))
                .Include(e => e.SalaryInfos.Where(s => s.StatusId == 1))
                .Include(e => e.NetSalaryAgents.Where(n => n.StatusId == 1))
                .Include(e => e.OtherRemittanceAgents.Where(o => o.StatusId == 1))
                .Include(e => e.SupportingDocuments.Where(s => s.StatusId == 1))
                .Where(e => e.OfficeId == officeId && e.StatusId == 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllForSabha(int sabhaId)
        {
            return await HRMDbContext.Employees
                .Include(e => e.Addresses.Where(a => a.StatusId == 1))
                .Include(e => e.SpouserInfos.Where(s => s.StatusId == 1))
                .Include(e => e.ChildrenInfos.Where(c => c.StatusId == 1))
                .Include(e => e.ServiceInfos.Where(s => s.StatusId == 1))
                .Include(e => e.SalaryInfos.Where(s => s.StatusId == 1))
                .Include(e => e.NetSalaryAgents.Where(n => n.StatusId == 1))
                .Include(e => e.OtherRemittanceAgents.Where(o => o.StatusId == 1))
                .Include(e => e.SupportingDocuments.Where(s => s.StatusId == 1))
                .Where(e => e.SabhaId == sabhaId && e.StatusId == 1)
                .ToListAsync();
        }
        public async Task<Employee> GetEmployeesBySabhaAndEMPNo(int sabhaId, string empNumber)
        {
            return await HRMDbContext.Employees
                .Where(e => e.SabhaId == sabhaId && e.EmployeeNo.Trim() == empNumber.Trim())
                .FirstOrDefaultAsync();
        }
        public async Task<Employee> GetEmployeesBySabhaAndPhone(int sabhaId, string phoneNumber)
        {
            return await HRMDbContext.Employees
                .Where(e => e.SabhaId == sabhaId && e.MobileNo.Trim().Replace(" ", "") == phoneNumber.Trim().Replace(" ", ""))
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> GetEmployeesBySabhaAndNIC(int sabhaId, string nicNumber)
        {
            return await HRMDbContext.Employees
                 .Where(e => e.SabhaId == sabhaId && e.NICNumber.Trim().Replace(" ", "") == nicNumber.Trim().Replace(" ", ""))
                 .FirstOrDefaultAsync();
        }

        public async Task<Employee> GetBySabhaNICAsync(int sabhaId, string NIC)
        {
            return await HRMDbContext.Employees
                .Where(e => e.SabhaId == sabhaId && e.NICNumber.Trim() == NIC.Trim() && e.StatusId == 1).FirstOrDefaultAsync();
        }

        public async Task<Employee> GetBySabhaPhoneNoAsync(int sabhaId, string PhoneNo)
        {
            return await HRMDbContext.Employees
                .Where(e => e.SabhaId == sabhaId && e.MobileNo.Trim() == PhoneNo.Trim() && e.StatusId == 1).FirstOrDefaultAsync();
        }

        private HRMDbContext HRMDbContext
        {
            get { return Context as HRMDbContext; }
        }

    }
}