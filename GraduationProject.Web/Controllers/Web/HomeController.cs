using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Data;
using System.Security.Claims;
using GraduationProject.Services.Interfaces;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _usermanage;
        private ISkillServices _skillSrv;
        private INewsFeedService _newsFeedSrv;

        public HomeController(UserManager<ApplicationUser> usermager , ISkillServices skillsSrv, INewsFeedService newsFeedSrv)
        {
            _usermanage = usermager;
            _skillSrv = skillsSrv;
            _newsFeedSrv = newsFeedSrv;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(policy: "Students")]
        [HttpGet]
        [Route("api/Data")]
        public IActionResult ApiAsync()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            return Ok("hi from api");
        }

        [HttpGet]
        [Route("api/test")]
        public async Task<IActionResult> TestMethod()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _usermanage.FindByEmailAsync(userEmail);

            return Ok(new { status = "Success", Feed = _newsFeedSrv.FollowingQuestions(User.Id) });

        }
    }
}
