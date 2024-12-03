using AutoMapper;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Services.HRM.PersonalFile;
using CAT20.Services.HRM.PersonalFile;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.HRM.PersonalFile;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.HRM.PersonalFile
{
    [Route("api/HRM/PersonalFile/ReferenceData")]
    [ApiController]
    public class EmployeeReferenceDataController : BaseController
    {
        private readonly IEmployeeTypeDataService _employeeTypeDataService;
        private readonly ICarderStatusDataService _carderStatusDataService;
        private readonly ISalaryStructureDataService _salaryStructureDataService;
        private readonly IServiceTypeDataService _serviceTypeDataService;
        private readonly IJobTitleDataService _jobTitleDataService;
        private readonly IClassLevelDataService _classLevelDataService;
        private readonly IGradeLevelDataService _gradeLevelDataService;
        private readonly IAgraharaCategoryDataService _agraharaCategoryDataService;
        private readonly IAppointmentTypeDataService _appointmentTypeDataService;
        private readonly IFundingSourceDataService _fundingSourceDataService;
        private readonly ISupportingDocTypeDataService _supportingDocTypeDataService;
        private readonly IMapper _mapper;

        public EmployeeReferenceDataController
            (IEmployeeTypeDataService EmployeeTypeDataService,
            ICarderStatusDataService CarderStatusDataService,
            ISalaryStructureDataService SalaryStructureDataService,
            IServiceTypeDataService ServiceTypeDataService,
            IJobTitleDataService JobTitleDataService,
            IClassLevelDataService ClassLevelDataService,
            IGradeLevelDataService GradeLevelDataService,
            IAgraharaCategoryDataService AgraharaCategoryDataService,
            IAppointmentTypeDataService AppointmentTypeDataService,
            IFundingSourceDataService FundingSourceDataService,
            ISupportingDocTypeDataService SupportingDocTypeDataService,
            IMapper mapper)

        {
            _employeeTypeDataService = EmployeeTypeDataService;
            _carderStatusDataService = CarderStatusDataService;
            _salaryStructureDataService = SalaryStructureDataService;
            _serviceTypeDataService = ServiceTypeDataService;
            _jobTitleDataService = JobTitleDataService;
            _classLevelDataService = ClassLevelDataService;
            _gradeLevelDataService = GradeLevelDataService;
            _agraharaCategoryDataService = AgraharaCategoryDataService;
            _appointmentTypeDataService = AppointmentTypeDataService;
            _fundingSourceDataService = FundingSourceDataService;
            _supportingDocTypeDataService = SupportingDocTypeDataService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getEmployeeTypeDataById/{id}")]
        public async Task<ActionResult<EmployeeTypeDataResource>> GetEmployeeTypeDataById(int id)
        {
            var EmployeeTypeData = await _employeeTypeDataService.GetEmployeeTypeDataById(id);
            var EmployeeTypeDataResource = _mapper.Map<EmployeeTypeData, EmployeeTypeDataResource>(EmployeeTypeData);

            if (EmployeeTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeTypeDataResource);
        }

        [HttpGet]
        [Route("getAllEmployeeTypeData")]
        public async Task<ActionResult<IEnumerable<EmployeeTypeDataResource>>> GetAllEmployeeTypeData()
        {
            var EmployeeTypeData = await _employeeTypeDataService.GetAllEmployeeTypeData();
            var EmployeeTypeDataResources = _mapper.Map<IEnumerable<EmployeeTypeData>, IEnumerable<EmployeeTypeDataResource>>(EmployeeTypeData);

            if (EmployeeTypeData == null || !EmployeeTypeData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(EmployeeTypeDataResources);
        }

        [HttpGet]
        [Route("getCarderStatusDataById/{id}")]
        public async Task<ActionResult<CarderStatusDataResource>> GetCarderStatusDataById(int id)
        {
            var CarderStatusData = await _carderStatusDataService.GetCarderStatusDataById(id);
            var CarderStatusDataResource = _mapper.Map<CarderStatusData, CarderStatusDataResource>(CarderStatusData);

            if (CarderStatusData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(CarderStatusDataResource);
        }

        [HttpGet]
        [Route("getAllCarderStatusData")]
        public async Task<ActionResult<IEnumerable<CarderStatusDataResource>>> GetAllCarderStatusData()
        {
            var CarderStatusData = await _carderStatusDataService.GetAllCarderStatusData();
            var CarderStatusDataResources = _mapper.Map<IEnumerable<CarderStatusData>, IEnumerable<CarderStatusDataResource>>(CarderStatusData);

            if (CarderStatusData == null || !CarderStatusData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(CarderStatusDataResources);
        }

        [HttpGet]
        [Route("getSalaryStructureDataById/{id}")]
        public async Task<ActionResult<SalaryStructureDataResource>> GetSalaryStructureDataById(int id)
        {
            var SalaryStructureData = await _salaryStructureDataService.GetSalaryStructureDataById(id);
            var SalaryStructureDataResource = _mapper.Map<SalaryStructureData, SalaryStructureDataResource>(SalaryStructureData);

            if (SalaryStructureData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(SalaryStructureDataResource);
        }

        [HttpGet]
        [Route("getAllSalaryStructureData")]
        public async Task<ActionResult<IEnumerable<SalaryStructureDataResource>>> GetAllSalaryStructureData()
        {
            var SalaryStructureData = await _salaryStructureDataService.GetAllSalaryStructureData();
            var SalaryStructureDataResources = _mapper.Map<IEnumerable<SalaryStructureData>, IEnumerable<SalaryStructureDataResource>>(SalaryStructureData);

            if (SalaryStructureData == null || !SalaryStructureData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(SalaryStructureDataResources);
        }

        [HttpGet]
        [Route("getServiceTypeDataById/{id}")]
        public async Task<ActionResult<ServiceTypeDataResource>> GetServiceTypeDataById(int id)
        {
            var ServiceTypeData = await _serviceTypeDataService.GetServiceTypeDataById(id);
            var ServiceTypeDataResource = _mapper.Map<ServiceTypeData, ServiceTypeDataResource>(ServiceTypeData);

            if (ServiceTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(ServiceTypeDataResource);
        }

        [HttpGet]
        [Route("getServiceTypeDataBySalaryStructureId/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceTypeDataResource>>> GetServiceTypeDataBySalaryStructureId(int id)
        {
            var ServiceTypeData = await _serviceTypeDataService.GetServiceTypeDataBySalaryStructureId(id);
            var ServiceTypeDataResource = _mapper.Map<IEnumerable<ServiceTypeData>, IEnumerable<ServiceTypeDataResource>>(ServiceTypeData);

            if (ServiceTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(ServiceTypeDataResource);
        }

        [HttpGet]
        [Route("getAllServiceTypeData")]
        public async Task<ActionResult<IEnumerable<ServiceTypeDataResource>>> GetAllServiceTypeData()
        {
            var ServiceTypeData = await _serviceTypeDataService.GetAllServiceTypeData();
            var ServiceTypeDataResources = _mapper.Map<IEnumerable<ServiceTypeData>, IEnumerable<ServiceTypeDataResource>>(ServiceTypeData);

            if (ServiceTypeData == null || !ServiceTypeData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(ServiceTypeDataResources);
        }

        [HttpGet]
        [Route("getJobTitleDataById/{id}")]
        public async Task<ActionResult<JobTitleDataResource>> GetJobTitleDataById(int id)
        {
            var JobTitleData = await _jobTitleDataService.GetJobTitleDataById(id);
            var JobTitleDataResource = _mapper.Map<JobTitleData, JobTitleDataResource>(JobTitleData);

            if (JobTitleData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(JobTitleDataResource);
        }

        [HttpGet]
        [Route("getAllJobTitleDataByServiceTypeDataId/{id}")]
        public async Task<ActionResult<IEnumerable<JobTitleDataResource>>> GetAllJobTitleDataByServiceTypeDataId(int id)
        {
            var JobTitleData = await _jobTitleDataService.GetAllJobTitleDataByServiceTypeDataId(id);
            var JobTitleDataResources = _mapper.Map<IEnumerable<JobTitleData>, IEnumerable<JobTitleDataResource>>(JobTitleData);

            if (JobTitleData == null || !JobTitleData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(JobTitleDataResources);
        }

        [HttpGet]
        [Route("getAllJobTitleData")]
        public async Task<ActionResult<IEnumerable<JobTitleDataResource>>> GetAllJobTitleData()
        {
            var JobTitleData = await _jobTitleDataService.GetAllJobTitleData();
            var JobTitleDataResources = _mapper.Map<IEnumerable<JobTitleData>, IEnumerable<JobTitleDataResource>>(JobTitleData);

            if (JobTitleData == null || !JobTitleData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(JobTitleDataResources);
        }

        [HttpGet]
        [Route("getClassLevelDataById/{id}")]
        public async Task<ActionResult<ClassLevelDataResource>> GetClassLevelDataById(int id)
        {
            var ClassLevelData = await _classLevelDataService.GetClassLevelDataById(id);
            var ClassLevelDataResource = _mapper.Map<ClassLevelData, ClassLevelDataResource>(ClassLevelData);

            if (ClassLevelData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(ClassLevelDataResource);
        }

        [HttpGet]
        [Route("getAllClassLevelData")]
        public async Task<ActionResult<IEnumerable<ClassLevelDataResource>>> GetAllClassLevelData()
        {
            var ClassLevelData = await _classLevelDataService.GetAllClassLevelData();
            var ClassLevelDataResources = _mapper.Map<IEnumerable<ClassLevelData>, IEnumerable<ClassLevelDataResource>>(ClassLevelData);

            if (ClassLevelData == null || !ClassLevelData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(ClassLevelDataResources);
        }

        [HttpGet]
        [Route("getGradeLevelDataById/{id}")]
        public async Task<ActionResult<GradeLevelDataResource>> GetGradeLevelDataById(int id)
        {
            var GradeLevelData = await _gradeLevelDataService.GetGradeLevelDataById(id);
            var GradeLevelDataResource = _mapper.Map<GradeLevelData, GradeLevelDataResource>(GradeLevelData);

            if (GradeLevelData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(GradeLevelDataResource);
        }

        [HttpGet]
        [Route("getAllGradeLevelData")]
        public async Task<ActionResult<IEnumerable<GradeLevelDataResource>>> GetAllGradeLevelData()
        {
            var GradeLevelData = await _gradeLevelDataService.GetAllGradeLevelData();
            var GradeLevelDataResources = _mapper.Map<IEnumerable<GradeLevelData>, IEnumerable<GradeLevelDataResource>>(GradeLevelData);

            if (GradeLevelData == null || !GradeLevelData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(GradeLevelDataResources);
        }

        [HttpGet]
        [Route("getAgraharaCategoryDataById/{id}")]
        public async Task<ActionResult<AgraharaCategoryDataResource>> GetAgraharaCategoryDataById(int id)
        {
            var AgraharaCategoryData = await _agraharaCategoryDataService.GetAgraharaCategoryDataById(id);
            var AgraharaCategoryDataResource = _mapper.Map<AgraharaCategoryData, AgraharaCategoryDataResource>(AgraharaCategoryData);

            if (AgraharaCategoryData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(AgraharaCategoryDataResource);
        }

        [HttpGet]
        [Route("getAllAgraharaCategoryData")]
        public async Task<ActionResult<IEnumerable<AgraharaCategoryDataResource>>> GetAllAgraharaCategoryData()
        {
            var AgraharaCategoryData = await _agraharaCategoryDataService.GetAllAgraharaCategoryData();
            var AgraharaCategoryDataResources = _mapper.Map<IEnumerable<AgraharaCategoryData>, IEnumerable<AgraharaCategoryDataResource>>(AgraharaCategoryData);

            if (AgraharaCategoryData == null || !AgraharaCategoryData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(AgraharaCategoryDataResources);
        }

        [HttpGet]
        [Route("getAppointmentTypeDataById/{id}")]
        public async Task<ActionResult<AppointmentTypeDataResource>> GetAppointmentTypeDataById(int id)
        {
            var AppointmentTypeData = await _appointmentTypeDataService.GetAppointmentTypeDataById(id);
            var AppointmentTypeDataResource = _mapper.Map<AppointmentTypeData, AppointmentTypeDataResource>(AppointmentTypeData);

            if (AppointmentTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(AppointmentTypeDataResource);
        }

        [HttpGet]
        [Route("getAllAppointmentTypeData")]
        public async Task<ActionResult<IEnumerable<AppointmentTypeDataResource>>> GetAllAppointmentTypeData()
        {
            var AppointmentTypeData = await _appointmentTypeDataService.GetAllAppointmentTypeData();
            var AppointmentTypeDataResources = _mapper.Map<IEnumerable<AppointmentTypeData>, IEnumerable<AppointmentTypeDataResource>>(AppointmentTypeData);

            if (AppointmentTypeData == null || !AppointmentTypeData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(AppointmentTypeDataResources);
        }

        [HttpGet]
        [Route("getFundingSourceDataById/{id}")]
        public async Task<ActionResult<FundingSourceDataResource>> GetFundingSourceDataById(int id)
        {
            var FundingSourceData = await _fundingSourceDataService.GetFundingSourceDataById(id);
            var FundingSourceDataResource = _mapper.Map<FundingSourceData, FundingSourceDataResource>(FundingSourceData);

            if (FundingSourceData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(FundingSourceDataResource);
        }

        [HttpGet]
        [Route("getAllFundingSourceData")]
        public async Task<ActionResult<IEnumerable<FundingSourceDataResource>>> GetAllFundingSourceData()
        {
            var FundingSourceData = await _fundingSourceDataService.GetAllFundingSourceData();
            var FundingSourceDataResources = _mapper.Map<IEnumerable<FundingSourceData>, IEnumerable<FundingSourceDataResource>>(FundingSourceData);

            if (FundingSourceData == null || !FundingSourceData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(FundingSourceDataResources);
        }

        [HttpGet]
        [Route("getSupportingDocTypeDataById/{id}")]
        public async Task<ActionResult<SupportingDocTypeDataResource>> GetSupportingDocTypeDataById(int id)
        {
            var SupportingDocTypeData = await _supportingDocTypeDataService.GetSupportingDocTypeDataById(id);
            var SupportingDocTypeDataResource = _mapper.Map<SupportingDocTypeData, SupportingDocTypeDataResource>(SupportingDocTypeData);

            if (SupportingDocTypeData == null)
            {
                return NotFound("Not Found");
            }

            return Ok(SupportingDocTypeDataResource);
        }

        [HttpGet]
        [Route("getAllSupportingDocTypeData")]
        public async Task<ActionResult<IEnumerable<SupportingDocTypeDataResource>>> GetAllSupportingDocTypeData()
        {
            var SupportingDocTypeData = await _supportingDocTypeDataService.GetAllSupportingDocTypeData();
            var SupportingDocTypeDataResources = _mapper.Map<IEnumerable<SupportingDocTypeData>, IEnumerable<SupportingDocTypeDataResource>>(SupportingDocTypeData);

            if (SupportingDocTypeData == null || !SupportingDocTypeData.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(SupportingDocTypeDataResources);
        }
    }
}
