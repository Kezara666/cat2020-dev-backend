using AutoMapper;
using CAT20.Core.DTO.Vote;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Vote;
using CAT20.Core.Services.Vote;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Resources.Control;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Vote
{
    [Route("api/vote/Classification")]
    [ApiController]
    public class ClassificationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IClassificationService _classificationService;

        public ClassificationController( IMapper mapper ,IClassificationService classificationService)
        {
            _mapper = mapper;
            _classificationService = classificationService;

        }

        [HttpGet]
        [Route("getAllClassifications")]
        public async Task<ActionResult<IEnumerable<Classification>>> GetAllClassificationsWithLedgerAccountsForSabha()
        {
            var classifications = await _classificationService.GetAllClassifications();
            var classificatioResources = _mapper.Map<IEnumerable<Classification>, IEnumerable<ClassificatioResource>>(classifications);

            return Ok(classificatioResources);
        }

        [HttpGet]
        [Route("getClassificationById/{id}")]
        public async Task<ActionResult<ClassificatioResource>> GetClassificationById([FromRoute] int id)
        {
            var classification = await _classificationService.GetClassificationById(id);
            var BankBranchResource = _mapper.Map<Classification, ClassificatioResource>(classification);
            return Ok(BankBranchResource);
        }

        [HttpGet]
        [Route("getAllClassificationsWithLedgerAccountsForSabha/{sabhaId}")]
        public async Task<ActionResult<IEnumerable<ClassificationAdvancedModel>>> GetAllClassificationsWithLedgerAccountsForSabha(int sabhaId)
        {
            var classifications = await _classificationService.GetAllClassificationsWithLedgerAccountsForSabha(sabhaId);
            return Ok(classifications);
        }
    }
}
