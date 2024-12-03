
using AutoMapper;
using CAT20.Common.Utility;
using CAT20.Core.Models.Control;
using CAT20.Core.Models.Interfaces;
using CAT20.Core.Models.Mixin;
using CAT20.Core.Models.User;
using CAT20.Core.Services.Control;
using CAT20.Core.Services.User;
using CAT20.Services.Control;
using CAT20.Services.User;
using CAT20.WebApi.Controllers.Control;
using CAT20.WebApi.Helpers;
using CAT20.WebApi.Models;
using CAT20.WebApi.Resources.Control;
using CAT20.WebApi.Resources.Mixin;
using CAT20.WebApi.Resources.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CAT20.Core;
using System.Text;

namespace CAT20.WebApi.Controllers.User
{
    [Route("api/Group")]
    [ApiController]
    public class GroupController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IGroupService _groupService;
        private readonly IRuleService _ruleService;
        private readonly IGroupUserService _groupUserService;
        private readonly IGroupRuleService _groupRuleService;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;

        public GroupController(
            IGroupService groupService,
            IMapper mapper,
            IUserDetailService userDetailService,
            IRuleService ruleService,
            IGroupUserService groupUserService,
            IGroupRuleService groupRuleService,
            IConfiguration config)
        {
            this._mapper = mapper;
            this._groupService = groupService;
            _userDetailService = userDetailService;
            _ruleService = ruleService;
            _groupUserService = groupUserService;
            _groupRuleService = groupRuleService;
            _configuration = config;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<ActionResult<Group>> GetById(int id)
        {
            var group = new Group();
            if (id != 0)
                group = await _groupService.GetGroupById(id);

            return Ok(group);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<Group>>> GetAll()
        {
            var returnList = await _groupService.GetAllGroups(); ;

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getAllForSabhaId/{id}")]
        public async Task<ActionResult<List<Group>>> GetAllForSabhaId(int id)
        {
            var returnList = await _groupService.GetAllGroupsForSabhaId(id); ;

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<List<Group>>> FindAll(Group group)
        {
            var returnList = await _groupService.GetAllGroups(); ;

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getForGroupUsers/{id}")]
        public async Task<ActionResult<Group>> GetForGroupUsers(int id)
        {
            var groupModel = new GroupModel();
            var dbGroup = await _groupService.GetGroupById(id);
            groupModel = (GroupModel)dbGroup;
            groupModel.assignedList = new List<SelectObjectModel>();
            groupModel.notAssignedList = new List<SelectObjectModel>();

            var employees = await _userDetailService.GetAllUserDetailsForSabhaId(dbGroup.SabhaId.Value);
            var groupUsers = await _groupUserService.GetAllForAsync(dbGroup);
            var employeeIDList = new List<int>();
            foreach (var item in groupUsers)
            {
                employeeIDList.Add(item.UserID);
                var employee = employees.FirstOrDefault(t => t.ID == item.UserID);

                var selectObject = new SelectObjectModel();
                selectObject.id = item.UserID.ToString();
                selectObject.name = employee.NameInFull;
                groupModel.assignedList.Add(selectObject);
            }

            var notSelectedList = employees.Where(t => !employeeIDList.Contains(t.ID));
            foreach (var item in notSelectedList)
            {
                if (string.IsNullOrWhiteSpace(item.NameInFull))
                    continue;
                var selectObject = new SelectObjectModel();
                selectObject.id = item.ID.ToString();
                selectObject.name = item.NameInFull;
                groupModel.notAssignedList.Add(selectObject);
            }

            return Ok(groupModel);
        }

        [HttpGet]
        [Route("getAllRIUsersForSabha/{sabhaid}")]
        public async Task<ActionResult<UserDetail>> GetAllRIUsersForSabha(int sabhaid)
        {
            try
            {
                var groupModel = new GroupModel();
                var dbGroup = await _groupService.GetRIGroupForSabhaAsync(sabhaid);
                groupModel = (GroupModel)dbGroup;
                groupModel.assignedList = new List<SelectObjectModel>();
                groupModel.notAssignedList = new List<SelectObjectModel>();

                var employees = await _userDetailService.GetAllUserDetailsForSabhaId(sabhaid);
                var groupUsers = await _groupUserService.GetAllForAsync(dbGroup);
                var employeeIDList = new List<int>();

                var riUserList = new List<UserDetail>();

                foreach (var item in groupUsers)
                {
                    employeeIDList.Add(item.UserID);
                    var employee = employees.FirstOrDefault(t => t.ID == item.UserID);
                    riUserList.Add(employee);
                    //var selectObject = new SelectObjectModel();
                    //selectObject.id = item.UserID.ToString();
                    //selectObject.name = employee.NameInFull;
                    //groupModel.assignedList.Add(selectObject);
                }

                //var notSelectedList = employees.Where(t => !employeeIDList.Contains(t.ID));
                //foreach (var item in notSelectedList)
                //{
                //    if (string.IsNullOrWhiteSpace(item.NameInFull))
                //        continue;
                //    var selectObject = new SelectObjectModel();
                //    selectObject.id = item.ID.ToString();
                //    selectObject.name = item.NameInFull;
                //    groupModel.notAssignedList.Add(selectObject);
                //}

                return Ok(riUserList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllMeterReaderUsersForSabha/{sabhaid}")]
        public async Task<ActionResult<UserDetail>> GetAllMeterReaderUsersForSabha(int sabhaid)
        {
            try
            {
                var groupModel = new GroupModel();
                var dbGroup = await _groupService.GetMeterReaderGroupForSabhaAsync(sabhaid);
                groupModel = (GroupModel)dbGroup;
                groupModel.assignedList = new List<SelectObjectModel>();
                groupModel.notAssignedList = new List<SelectObjectModel>();

                var employees = await _userDetailService.GetAllUserDetailsForSabhaId(sabhaid);
                var groupUsers = await _groupUserService.GetAllForAsync(dbGroup);
                var employeeIDList = new List<int>();

                var meterReaderUserList = new List<UserDetail>();

                foreach (var item in groupUsers)
                {
                    employeeIDList.Add(item.UserID);
                    var employee = employees.FirstOrDefault(t => t.ID == item.UserID);
                    meterReaderUserList.Add(employee);
                    //var selectObject = new SelectObjectModel();
                    //selectObject.id = item.UserID.ToString();
                    //selectObject.name = employee.NameInFull;
                    //groupModel.assignedList.Add(selectObject);
                }

                //var notSelectedList = employees.Where(t => !employeeIDList.Contains(t.ID));
                //foreach (var item in notSelectedList)
                //{
                //    if (string.IsNullOrWhiteSpace(item.NameInFull))
                //        continue;
                //    var selectObject = new SelectObjectModel();
                //    selectObject.id = item.ID.ToString();
                //    selectObject.name = item.NameInFull;
                //    groupModel.notAssignedList.Add(selectObject);
                //}

                return Ok(meterReaderUserList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("getForGroupRules/{id}")]
        public async Task<ActionResult<CAT20.Core.Models.User.Rule>> GetForGroupRules(int id)
        {
            var groupModel = new GroupModel();
            var dbGroup = _groupService.GetGroupById(id);
            groupModel = (GroupModel)dbGroup.Result;
            groupModel.assignedList = new List<SelectObjectModel>();
            groupModel.notAssignedList = new List<SelectObjectModel>();

            var rules = await _ruleService.GetAllRules();
            var groupRules = await _groupRuleService.GetAllForAsync(dbGroup.Result);
            var ruleIDList = new List<int>();
            foreach (var item in groupRules)
            {
                ruleIDList.Add(item.RuleID);
                var rule = rules.FirstOrDefault(t => t.ID == item.RuleID);

                var selectObject = new SelectObjectModel();
                selectObject.id = item.RuleID.ToString();
                selectObject.name = rule.Description;
                groupModel.assignedList.Add(selectObject);
            }

            var notSelectedList = rules.Where(t => !ruleIDList.Contains(t.ID.Value));
            foreach (var item in notSelectedList)
            {
                var selectObject = new SelectObjectModel();
                selectObject.id = item.ID.Value.ToString();
                selectObject.name = item.Description;
                groupModel.notAssignedList.Add(selectObject);
            }

            return Ok(groupModel);
        }

        //[HttpPost]
        //[Route("save")]
        //public async Task<ActionResult<GroupResource>> Save(GroupResource obj)
        //{
        //    //var identity = (ClaimsIdentity)User.Identity;
        //    //var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
        //    //var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

        //    try
        //    {
        //        if (obj.ID == 0 || obj.ID == null)
        //        {
        //            obj.DateCreated = DateTime.Now;
        //        }
        //        else
        //        {
        //            obj.DateModified = DateTime.Now;
        //        }
        //        var transObject = _groupService.CreateGroup(_mapper.Map<GroupResource, Group>(obj));
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Ok(obj);
        //}

        [HttpPost]
        [Route("deleteGroup/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (id == 0)
                return BadRequest();

            var group = await _groupService.GetGroupById(id);

            if (group == null)
                return NotFound();

            await _groupService.DeleteGroup(group);
            return NoContent();
        }


        [HttpPost("save")]
        public async Task<ActionResult<VoteAssignmentResource>> Save([FromBody] GroupResource objGroupResource)
        {
            try
            {
                if (objGroupResource.ID == 0)
                {
                    objGroupResource.DateCreated = DateTime.Now;

                    var groupToCreate = _mapper.Map<GroupResource, Group>(objGroupResource);
                    var newGroup = await _groupService.CreateGroup(groupToCreate);
                    //var group = await _groupService.GetGroupById(newGroup.ID.Value);
                    //var groupResource = _mapper.Map<Group, GroupResource>(group);

                    return Ok(newGroup);
                }
                else
                {
                    objGroupResource.DateModified = DateTime.Now;
                    var groupToBeUpdate = await _groupService.GetGroupById(objGroupResource.ID);
                    if (groupToBeUpdate == null)
                        return NotFound();
                    var group = _mapper.Map<GroupResource, Group>(objGroupResource);
                    await _groupService.UpdateGroup(groupToBeUpdate, group);
                    var updatedGroup = await _groupService.GetGroupById(objGroupResource.ID);
                    var updatedGroupResource = _mapper.Map<Group, GroupResource>(updatedGroup);

                    return Ok(updatedGroupResource);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        //[HttpPost]
        //[Route("saveUsers")]
        //public async Task<ActionResult<Group>> SaveUsers(List<GroupUserResource> model)
        //{
        //    try
        //    {
        //        Group obj = await _groupService.GetGroupById(model[0].GroupID);
        //        var dbGroupUsers = _groupUserService.GetAllForAsync(obj).Result;

        //        var newList = new List<GroupUser>();
        //        foreach (var item in model)
        //        {
        //            var groupUser = dbGroupUsers.FirstOrDefault(t => t.UserID == item.UserID);
        //            if (groupUser == null)
        //            {
        //                groupUser = new GroupUser();
        //                groupUser.GroupID = obj.ID.Value;
        //                groupUser.UserID = item.UserID;
        //                groupUser.DateCreated = DateTime.Now;
        //                newList.Add(groupUser);
        //            }
        //        }
        //        List<GroupUser> deleteList = dbGroupUsers.Where(p => model.All(p2 => p2.UserID != p.UserID && p.UserID.ToString() != string.Empty)).ToList();
        //        var transObject = _groupUserService.SaveUsers(newList, deleteList);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Ok(model);
        //}

        [HttpPost]
        [Route("saveUsers")]
        public async Task<ActionResult<Group>> SaveUsers(List<GroupUserResource> model)
        {
            try
            {
                Group obj = await _groupService.GetGroupById(model[0].GroupID);
                var dbGroupUsers = _groupUserService.GetAllForAsync(obj).Result;

                var newList = new List<GroupUser>();
                foreach (var item in model)
                {
                    var groupUser = dbGroupUsers.FirstOrDefault(t => t.UserID == item.UserID);
                    if (groupUser == null)
                    {
                        groupUser = new GroupUser();
                        groupUser.GroupID = obj.ID.Value;
                        groupUser.UserID = item.UserID;
                        groupUser.UserCreatedID = item.UserCreatedID;
                        groupUser.UserModifiedID = item.UserModifiedID;
                        groupUser.DateCreated = DateTime.Now;
                        newList.Add(groupUser);
                    }
                }
                List<GroupUser> deleteList = dbGroupUsers.Where(p => model.All(p2 => p2.UserID != p.UserID && p.UserID.ToString() != string.Empty)).ToList();
                //var transObject = _groupUserService.SaveUsers(newList, deleteList);

                if (newList.Count() > 0)
                {
                    foreach (var newGroupUser in newList)
                    {
                        if (newGroupUser.UserID != 0)
                        {
                            await _groupUserService.CreateGroupUser(newGroupUser);
                        }
                    }
                }

                if (deleteList.Count() > 0)
                {
                    foreach (var removeGroupUser in deleteList)
                    {
                        await _groupUserService.DeleteGroupUser(removeGroupUser);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            return Ok(model);
        }



        [HttpPost]
        [Route("saveRules")]
        public async Task<ActionResult<Group>> SaveRules(List<GroupRuleResource> model)
        {
            try
            {
                Group obj = await _groupService.GetGroupById(model[0].GroupID);
                var dbGroupRules = _groupRuleService.GetAllForAsync(obj).Result;

                var newList = new List<GroupRule>();
                foreach (var item in model)
                {
                    var groupRule = dbGroupRules.FirstOrDefault(t => t.RuleID == item.RuleID);
                    if (groupRule == null)
                    {
                        groupRule = new GroupRule();
                        groupRule.GroupID = obj.ID.Value;
                        groupRule.RuleID = item.RuleID;
                        groupRule.UserCreatedID = item.UserCreatedID;
                        groupRule.UserModifiedID = item.UserModifiedID;
                        groupRule.DateCreated = DateTime.Now;
                        newList.Add(groupRule);
                    }
                }
                List<GroupRule> deleteList = dbGroupRules.Where(p => model.All(p2 => p2.RuleID != p.RuleID && p.RuleID.ToString() != string.Empty)).ToList();
                //var transObject = _groupUserService.SaveUsers(newList, deleteList);

                if (newList.Count() > 0)
                {
                    foreach (var newGroupRule in newList)
                    {
                        if (newGroupRule.RuleID != 0)
                        {
                            await _groupRuleService.CreateGroupRule(newGroupRule);
                        }
                    }
                }

                if (deleteList.Count() > 0)
                {
                    foreach (var removeGroupRule in deleteList)
                    {
                        await _groupRuleService.DeleteGroupRule(removeGroupRule);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            return Ok(model);
        }

        //[HttpPost]
        //[Route("saveRules")]
        //public async Task<ActionResult<Group>> SaveRules(GroupModel model)
        //{
        //    try
        //    {
        //        Group obj = model;
        //        var dbGroupRules = _groupRuleService.GetAllForAsync(obj).Result;

        //        var newList = new List<GroupRule>();
        //        foreach (var item in model.assignedList)
        //        {
        //            var groupRule = dbGroupRules.FirstOrDefault(t => t.RuleID.ToString() == item.id);
        //            if (groupRule == null)
        //            {
        //                groupRule = new GroupRule();
        //                groupRule.GroupID = obj.ID.Value;
        //                groupRule.RuleID = int.Parse(item.id);
        //                groupRule.DateCreated = DateTime.Now;
        //                newList.Add(groupRule);
        //            }
        //        }

        //        List<GroupRule> deleteList = dbGroupRules.Where(p => model.assignedList.All(p2 => p2.id != p.RuleID.ToString() && p.RuleID.ToString() != string.Empty)).ToList();
        //        var transObject = _groupRuleService.SaveRules(newList, deleteList);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Ok(model);
        //}

        [HttpGet]
        [Route("rules/getAll")]
        public async Task<ActionResult<List<Group>>> GetAllRules()
        {
            var returnList = await _ruleService.GetAllRules();

            return Ok(returnList);
        }


        [HttpGet]
        [Route("getCheckAccessByRuleCode/{code}/{userId}")]
        public async Task<ActionResult<bool>> GetChackAccessByRuleCode(string code, int userId)
        {
            var responce = await _ruleService.GetChackAccessByRuleCode(userId, code);
            return Ok(responce);
        }

        [HttpGet]
        [Route("getPermittedRulesForUser/{userId}")]
        public async Task<ActionResult<List<Core.Models.User.Rule>>> getPermittedRulesForUser(int userId)
        {
            var newRuleList = new List<Core.Models.User.Rule>();
            var rulesList = await _ruleService.GetAllRules();
            foreach (var rule in rulesList)
            {
                var response = await _ruleService.GetChackAccessByRuleCode(userId, rule.Code);
                if (response == true)
                {
                    newRuleList.Add(rule);
                }
            }
            return Ok(newRuleList);
        }


        [HttpGet]
        [Route("getJWTTokenWithRulesAndModulesListForUser/{userid}")]
        public async Task<string> GetJWTTokenWithRulesAndModulesListForUser(int userid)
        {
            try
            {
                    var user = await _userDetailService.GetUserDetailById(userid);

                    if (user != null) //if user exists
                    {
                        //------ [Start - Get user permission codes]-----------------------------
                        var userAssignedRuleList = new List<string>();
                        userAssignedRuleList = await _ruleService.GetAllUserRuleCodes(user.ID);
                        //------ [End - Get user permission codes]-----------------------------

                        //default permission codes for all users
                        userAssignedRuleList.Add("DASHBOARD");
                        userAssignedRuleList.Add("DEFAULT");

                        //ADMIN RULE only for admin
                        if (user.IsAdmin == 1)
                        {
                            userAssignedRuleList.Add("ISADMIN");
                        }

                        //------ [Start - Get user Module codes]-----------------------------
                        var userAssignedModuleList = new List<string>();
                        userAssignedModuleList = await _ruleService.GetAllUserPermittedModules(user.ID);
                        //------ [End - Get user Module codes]-----------------------------

                        //---- [Start - create jwt claim] ------
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                        //---
                        new Claim("userAssignedRuleList", string.Join( ",", userAssignedRuleList.ToArray())),
                        new Claim("userAssignedModuleList", string.Join( ",", userAssignedModuleList.ToArray())),
                        };
                        //---- [End - create jwt claim] --------


                        //---- [Start - create jwt token] ------
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddYears(1),
                            signingCredentials: signIn
                        );

                    string jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwttoken;
                    }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
