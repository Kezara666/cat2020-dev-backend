using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CAT20.Api.Validators;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.WebApi.Resources.User;
using CAT20.WebApi.Resources.User.Save;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRecoverQuestionsController : BaseController
    {
        private readonly IUserRecoverQuestionService _userRecoverQuestionService;
        private readonly IMapper _mapper;

        public UserRecoverQuestionsController(IUserRecoverQuestionService userRecoverQuestionService, IMapper mapper)
        {
            _mapper = mapper;
            _userRecoverQuestionService = userRecoverQuestionService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserRecoverQuestion>>> GetAllProducts()
        {
            var userRecoverQuestions = await _userRecoverQuestionService.GetAllUserRecoverQuestions();
            var userRecoverQuestionResources = _mapper.Map<IEnumerable<UserRecoverQuestion>, IEnumerable<UserRecoverQuestionResource>>(userRecoverQuestions);

            return Ok(userRecoverQuestionResources);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRecoverQuestionResource>> GetUserRecoverQuestionById(int id)
        {
            var userRecoverQuestion = await _userRecoverQuestionService.GetUserRecoverQuestionById(id);
            var userRecoverQuestionResource = _mapper.Map<UserRecoverQuestion, UserRecoverQuestionResource>(userRecoverQuestion);
            return Ok(userRecoverQuestionResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserRecoverQuestionResource>> CreateUserRecoverQuestion([FromBody] SaveUserRecoverQuestionResource saveUserRecoverQuestionResource)
        {
            var validator = new SaveUserRecoverQuestionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserRecoverQuestionResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userRecoverQuestionToCreate = _mapper.Map<SaveUserRecoverQuestionResource, UserRecoverQuestion>(saveUserRecoverQuestionResource);

            var newUserRecoverQuestion = await _userRecoverQuestionService.CreateUserRecoverQuestion(userRecoverQuestionToCreate);

            var userRecoverQuestion = await _userRecoverQuestionService.GetUserRecoverQuestionById(newUserRecoverQuestion.ID);

            var userRecoverQuestionResource = _mapper.Map<UserRecoverQuestion, UserRecoverQuestionResource>(userRecoverQuestion);

            return Ok(userRecoverQuestionResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserRecoverQuestionResource>> UpdateProduct(int id, [FromBody] SaveUserRecoverQuestionResource saveUserRecoverQuestionResource)
        {
            var validator = new SaveUserRecoverQuestionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserRecoverQuestionResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var userRecoverQuestionToBeUpdate = await _userRecoverQuestionService.GetUserRecoverQuestionById(id);

            if (userRecoverQuestionToBeUpdate == null)
                return NotFound();

            var product = _mapper.Map<SaveUserRecoverQuestionResource, UserRecoverQuestion>(saveUserRecoverQuestionResource);

            await _userRecoverQuestionService.UpdateUserRecoverQuestion(userRecoverQuestionToBeUpdate, product);

            var updatedUserRecoverQuestion = await _userRecoverQuestionService.GetUserRecoverQuestionById(id);
            var updatedUserRecoverQuestionResource = _mapper.Map<UserRecoverQuestion, UserRecoverQuestionResource>(updatedUserRecoverQuestion);

            return Ok(updatedUserRecoverQuestionResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            var userRecoverQuestion = await _userRecoverQuestionService.GetUserRecoverQuestionById(id);

            if (userRecoverQuestion == null)
                return NotFound();

            await _userRecoverQuestionService.DeleteUserRecoverQuestion(userRecoverQuestion);

            return NoContent();
        }
    }
}
