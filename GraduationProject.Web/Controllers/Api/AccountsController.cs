using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GraduationProject.DataAccess;
using MailKit.Net.Smtp;
using MimeKit;
using GraduationProject.Web.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using GraduationProject.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class AccountsController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<ApplicationUser> _signinManager;
        private ApplicationDbContext _ctx;
        private IPasswordHasher<ApplicationUser> _passwordHasher;
        private IEmailSender _emailSender;
        public AccountsController(ApplicationDbContext ctx, 
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                SignInManager<ApplicationUser> signInManager,
                                IPasswordHasher<ApplicationUser> passwordHasher,
                                IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signinManager = signInManager;
            _ctx = ctx;
            _passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }
        [HttpPost]
        [Route("api/Signup")]
        public async Task<IActionResult> Signup([FromBody]SignupViewModel signupVM)
        {
            var user = new ApplicationUser()
            {
                Name =signupVM.Username,
                UserName = signupVM.Email,
                Email = signupVM.Email,
                BirthDate = signupVM.BirthDate,
                Gender = signupVM.Gender,
                UserType = signupVM.UserRole
            };
            var result = await _userManager.CreateAsync(user, signupVM.Password);
            if (result.Succeeded)
            {
               
                //Email Token For Email Confirmation
                string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                string confirmationTokenLink = Url.Action("EmailConfirmed", "Accounts", new
                {
                    userId = user.Id,
                    token = confirmationToken
                }, protocol: HttpContext.Request.Scheme);

                //Adding User Claim 
                await _userManager.AddClaimAsync(user, new Claim(signupVM.UserRole, "True"));

                //Send Email With Confirmation Token .
                try
                {
                    _emailSender.AccountConfirmationEmail(user.Email ,confirmationTokenLink);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }

                return Ok(new
                {
                    Status = "Success"
                });
            }

            return Ok(new { Status = "Failed", Errors = result.Errors.Where(e => e.Code != "DuplicateUserName") });
        }


        public async Task<IActionResult> EmailConfirmed(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Ok("Problem !!! Try Again");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(user);
        }

        [Route("api/Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user!= null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginVM.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userManager.GetClaimsAsync(user);
                        var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email)
                }.Union(userClaims);
                      
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERYLONGKEYVALUETHATISSECURITYGRADUATIONPROJECTDEMOFORSECURITY"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: "http://graduationProject.com",
                            audience: "http://graduationProject.com",
                            claims: claims,
                            expires: DateTime.UtcNow.AddDays(5),
                            signingCredentials: creds
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            Status = "Success",
                            UserType = user.UserType
                        });
                    }
                }
                return Ok(new {
                    Status = "Failed",
                    Msg ="Need Email Confirmation"
                });
            }

            return Ok(new {Status = "Failed" , Msg ="Wrong Email Or Password"});

        }

    }
}
