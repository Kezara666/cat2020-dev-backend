using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.WebApi.Resources.HRM.PersonalFile;
using Microsoft.AspNetCore.Mvc;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Services.HRM.PersonalFile;
using CAT20.WebApi.Controllers.Control;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Ocsp;
using CAT20.Core.Services.HRM.LoanManagement;
using CAT20.Services.HRM.LoanManagement;
using CAT20.Core.Services.Mixin;
using CAT20.Services.Mixin;

namespace CAT20.WebApi.Controllers.HRM.PersonalFile
{
    [Route("api/HRM/PersonalFile/Employees")]
    [ApiController]
    public class EmployeesController : BaseController

    {
        private readonly IEmployeeService _employeeService;
        private readonly IAdvanceBService _advanceBService;
        private readonly IMixinOrderService _mixinOrderService;
        private readonly IMapper _mapper;
        private EmployeeResource obj;

        public EmployeesController(IEmployeeService EmployeeService, IMapper mapper, IAdvanceBService advanceBService, IMixinOrderService mixinOrderService)
        {
            _employeeService = EmployeeService;
            _advanceBService = advanceBService;
            _mixinOrderService = mixinOrderService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getEmployeeById/{id}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeById(int id)
        {
            var Employee = await _employeeService.GetEmployeeById(id);
            var EmployeeResource = _mapper.Map<Employee, EmployeeResource>(Employee);

            if (Employee == null)
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeResource);
        }

        [HttpGet]
        [Route("getAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployees()
        {
            var Employees = await _employeeService.GetAllEmployees();
            var EmployeeResources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(Employees);

            if (Employees == null)
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeResources);
        }

        [HttpGet]
        [Route("getAllEmployeesForOffice/{officeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployeesForOffice(int officeId)
        {
            var Employees = await _employeeService.GetAllForOffice(officeId);
            var EmployeeResources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(Employees);

            if (Employees == null)
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeResources);
        }

        [HttpGet]
        [Route("getAllEmployeesForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<EmployeeResource>>> GetAllEmployeesForSabha(int sabhaId)
        {
            var Employees = await _employeeService.GetAllForSabha(sabhaId);
            var EmployeeResources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(Employees);

            if (Employees == null)
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeResources);
        }

        [HttpGet]
        [Route("getEmployeesBySabhaAndEMPNo/{sabhaId}/{empNumber}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeesBySabhaAndEMPNo(int sabhaId, string empNumber)
        {
            var employees = await _employeeService.GetEmployeesBySabhaAndEMPNo(sabhaId, empNumber);
            var employeeResources = _mapper.Map<Employee, EmployeeResource>(employees);

            if (employees == null)
            {
                return NotFound("No employees found");
            }

            return Ok(employeeResources);
        }

        [HttpGet]
        [Route("getEmployeesBySabhaAndPhone/{sabhaId}/{phoneNumber}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeesBySabhaAndPhone(int sabhaId, string phoneNumber)
        {
            var employees = await _employeeService.GetEmployeesBySabhaAndPhone(sabhaId, phoneNumber);
            var employeeResources = _mapper.Map<Employee, EmployeeResource>(employees);

            if (employees == null)
            {
                return NotFound("No employees found");
            }

            return Ok(employeeResources);
        }

        [HttpGet]
        [Route("getEmployeesBySabhaAndNIC/{sabhaId}/{nicNumber}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeesBySabhaAndNIC(int sabhaId, string nicNumber)
        {
            var employees = await _employeeService.GetEmployeesBySabhaAndNIC(sabhaId, nicNumber);
            var employeeResources = _mapper.Map<Employee, EmployeeResource>(employees);

            if (employees == null)
            {
                return NotFound("No employees found");
            }

            return Ok(employeeResources);
        }

        //[HttpPost]
        //[Route("saveEmployee")]
        //public async Task<ActionResult<EmployeeResource>> SaveEmployee([FromBody] EmployeeResource saveEmployeeResource)
        //{
        //    //var _token = new HTokenClaim
        //    //{

        //    //    userId = int.Parse(HttpContext.User.FindFirst("userid")!.Value),
        //    //    sabhaId = int.Parse(HttpContext.User.FindFirst("sabhaId")!.Value),
        //    //    officeId = int.Parse(HttpContext.User.FindFirst("officeID")!.Value),
        //    //};

        //    if (saveEmployeeResource.Id == 0 || saveEmployeeResource.Id == null)
        //    {
        //        // Create new employee
        //        saveEmployeeResource.Id = null;
        //        var newEmployee = _mapper.Map<EmployeeResource, Employee>(saveEmployeeResource);
        //        var savedEmployee = await _employeeService.SaveEmployee(newEmployee);
        //        var savedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(savedEmployee);

        //        return Ok(savedEmployeeResource);
        //    }
        //    else
        //    {
        //        var updateEmployeeResource = new EmployeeResource();
        //        var employeeToBeUpdate = await _employeeService.GetEmployeeById(obj.Id);
        //        if (employeeToBeUpdate == null)
        //            return NotFound();
        //        // Update existing employee
        //        //var existingEmployee = await _employeeService.GetEmployeeById(saveEmployeeResource.Id.Value);
        //        //if (existingEmployee == null)
        //        //{
        //        //    return NotFound();
        //        //}

        //            //var employeeToUpdate = _mapper.Map(saveEmployeeResource, existingEmployee);
        //            //var updatedEmployee = await _employeeService.UpdateEmployee(employeeToUpdate);
        //            //var updatedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(updatedEmployee);

        //            //return Ok(updatedEmployeeResource);
        //        var employee = _mapper.Map<EmployeeResource, Employee>(obj);
        //        await _employeeService.UpdateEmployee(employeeToBeUpdate, employee);
        //        var updateEmployee = await _employeeService.GetEmployeeById(obj.Id);
        //        updateEmployeeResource = _mapper.Map<Employee, EmployeeResource>(updateEmployee);

        //        return Ok(updateEmployeeResource);





        //    }
        //}

        [HttpPost]
        [Route("saveEmployee")]
        public async Task<ActionResult<EmployeeResource>> SaveEmployee([FromBody] EmployeeResource saveEmployeeResource)
        
        {
            if (saveEmployeeResource.Id == 0 || saveEmployeeResource.Id == null)
            {
                // Create new employee
                saveEmployeeResource.Id = null;
                var newEmployee = _mapper.Map<EmployeeResource, Employee>(saveEmployeeResource);
                var savedEmployee = await _employeeService.SaveEmployee(newEmployee);
                var savedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(savedEmployee);

                return Ok(savedEmployeeResource);
            }
            else
            {
                var employeeToBeUpdate = await _employeeService.GetEmployeeById(saveEmployeeResource.Id);
                if (employeeToBeUpdate == null)
                    return NotFound();

                // Update existing employee
                var employee = _mapper.Map<EmployeeResource, Employee>(saveEmployeeResource);
                await _employeeService.UpdateEmployee(employeeToBeUpdate, employee);

                var updatedEmployee = await _employeeService.GetEmployeeById(saveEmployeeResource.Id);
                var updatedEmployeeResource = _mapper.Map<Employee, EmployeeResource>(updatedEmployee);

                return Ok(updatedEmployeeResource);
            }
        }

        


        //---
        [HttpGet]
        [Route("getEmployeeByPhoneNo/{sabhaId}/{phoneNo}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeByPhoneNo([FromRoute] int sabhaId, string phoneNo)
        {
            try
            {
                if (phoneNo != null && phoneNo.Length >= 10)
                {
                    var employeeDetail = await _employeeService.GetBySabhaPhoneNoAsync(sabhaId, phoneNo);
                    var employeeDetailResource = _mapper.Map<Employee, EmployeeResource>(employeeDetail);

                    if (employeeDetailResource == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(employeeDetailResource);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //---

        //---
        [HttpGet]
        [Route("getEmployeeByNIC/{sabhaId}/{nic}")]
        public async Task<ActionResult<EmployeeResource>> GetEmployeeByNIC([FromRoute] int sabhaId, string nic)
        {
            try
            {
                if (nic != null && nic.Length >= 10)
                {
                    var employeeDetail = await _employeeService.GetBySabhaNICAsync(sabhaId, nic);
                    var employeeDetailResource = _mapper.Map<Employee, EmployeeResource>(employeeDetail);

                    if (employeeDetailResource == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(employeeDetailResource);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("deleteEmployee/{id}")]
        public async Task<ActionResult> DeleteEmployee( int id)
        {
            try
            {

                var employeeToDelete = await _employeeService.GetEmployeeById(id);
                if (employeeToDelete == null)
                {
                    return NotFound("Employee not found.");
                }

                var loans = await _advanceBService.GetAllLoansByEMPId(id);
                var mixinOrders = await _mixinOrderService.GetAllForEmployeeId(id);

                if (loans!=null && mixinOrders!=null)
                {
                    await _employeeService.DeleteEmployee(employeeToDelete);
                    return NoContent();
                }
                else
                {
                    return BadRequest("Delete Not Allowed Due to Child Records");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the employee.");
            }
        }

    }
}
