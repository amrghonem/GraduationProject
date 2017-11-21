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
        private IStudentProfileService _studSrv;

        public NewsFeedController(INewsFeedService newsFeedSrv
            ,UserManager<ApplicationUser> userManager
            , IStudentProfileService studSrv)
        {
            _newsFeedSrv = newsFeedSrv;
            _userManager = userManager;
            _studSrv = studSrv;
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

            var studentData = _studSrv.GetStudent(User.Id);

            return Ok(new { status = "Success",StudentData = new {
                Username = User.Name,
                StudentId = User.Id,
                Image = studentData.Image,
                Title = studentData.Title
            }
            , Feed = _newsFeedSrv.FollowingQuestions(User.Id).OrderByDescending(q=>q.Id) });

        }
    }
}
