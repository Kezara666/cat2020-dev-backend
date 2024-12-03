using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAT20.Api.Validators;
using CAT20.Core.Models.User;
using CAT20.Core.Services.User;
using CAT20.WebApi.Resources.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CAT20.Data;
using Microsoft.EntityFrameworkCore;
using CAT20.Services.User;
using CAT20.WebApi.Resources.User.Save;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using static CAT20.WebApi.Controllers.Control.UsersController;
using CAT20.Common.Utility;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : BaseController
    {
        public IConfiguration _configuration;
        private readonly IUserDetailService _userDetailService;
        private readonly IMapper _mapper;

        public TokenController(IUserDetailService userDetailService, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _userDetailService = userDetailService;
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginResource _loginData)
        {
            if (_loginData != null && _loginData.Username != null && _loginData.Password != null)
            {
                CryptoProvider crypto = new CryptoProvider();
                _loginData.Password = crypto.GetHash(_loginData.Password.Trim());

                var userDetailForLogin = await _userDetailService.Authenticate(_loginData.Username, _loginData.Password);
                var user = _mapper.Map<UserDetail, UserDetailResource>(userDetailForLogin);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("SabhaID", user.SabhaID.ToString()),
                        new Claim("OfficeID", user.OfficeID.ToString()),
                        new Claim("UserID", user.ID.ToString()),
                        new Claim("NameWithInitials", user.NameWithInitials),
                        new Claim("UserName", user.Username)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> Post(UserDetailResource _userData)
        //{
        //    if (_userData != null && _userData.Username != null && _userData.Password != null)
        //    {
        //        var userDetail = _mapper.Map<UserDetailResource, UserDetail>(_userData);
        //        var userDetailForLogin = await _userDetailService.GetUserDetailByUsernamePassword(userDetail);
        //        var user = _mapper.Map<UserDetail, UserDetailResource>(userDetailForLogin);

        //        if (user != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                new Claim("SabhaID", user.SabhaID.ToString()),
        //                new Claim("OfficeID", user.OfficeID.ToString()),
        //                new Claim("UserID", user.ID.ToString()),
        //                new Claim("NameWithInitials", user.NameWithInitials),
        //                new Claim("UserName", user.Username)
        //            };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //            var token = new JwtSecurityToken(
        //                _configuration["Jwt:Issuer"],
        //                _configuration["Jwt:Audience"],
        //                claims,
        //                expires: DateTime.UtcNow.AddMinutes(10),
        //                signingCredentials: signIn);

        //            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid credentials");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
