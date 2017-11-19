using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Data;
using GraduationProject.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class NewsFeedController : Controller
    {
        private INewsFeedService _newsFeedSrv;
        private UserManager<ApplicationUser> _userManager;

        public NewsFeedController(INewsFeedService newsFeedSrv,UserManager<ApplicationUser> userManager)
        {
            _newsFeedSrv = newsFeedSrv;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("api/addanswer")]
        public IActionResult CreateAnswer([FromBody]Answer answer)
        {
            _newsFeedSrv.AddAnswer(answer);
            return Ok(new { Status = "Success" });
        }
        [HttpGet]
        [Route("api/newsfeed")]
        [Authorize(policy: "Students")]
        public async Task<IActionResult> NewsFeed()
            {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);

            return Ok(new { status = "Success", Feed = _newsFeedSrv.FollowingQuestions(User.Id) });

        }
    }
}
