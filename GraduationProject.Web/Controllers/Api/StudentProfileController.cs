using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Data;
using Microsoft.EntityFrameworkCore;
using GraduationProject.DataAccess;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class StudentProfileController : Controller
    {
        private IStudentProfileService _profileSrv;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _env;
        private ApplicationDbContext _ctx;

        public StudentProfileController(IStudentProfileService profileSrv ,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext ctx,
            IHostingEnvironment env)
        {
            _profileSrv = profileSrv;
            _userManager = userManager;
            _env = env;
            _ctx = ctx;
        }

        [Authorize(policy: "Students")]
        [Route("api/studentprofile")]
        [HttpGet]
        public async Task<IActionResult> GetStudentProfile()
        {
            //Get Request's User 
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            if(!claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);

            //Get Student Profile
            var Profile = _profileSrv.GetStudentProfile(User);
            return Ok(new { Status = "Success", Profile });
        }

        [Authorize(policy: "Students")]
        [Route("api/editstudentprofile")]
        [HttpPost]
        public async Task<IActionResult> EditStudentProfile([FromBody]Student student)
        {
            //Get Request's User 
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            if (!claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }
            //Get Student Profile
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);
            student.ApplicationUserId = User.Id;
            var updatedProfileInfo = _profileSrv.EditProile(student);
            return Ok(new { Status = "Success", ProfileInfo = new {
                Image =updatedProfileInfo.Image,
                Info = updatedProfileInfo.Info,
                School =updatedProfileInfo.School,
                Universty = updatedProfileInfo.Universty
            } });
        }


        [Authorize(policy: "Students")]
        [Route("api/addstudentskill")]
        [HttpPost]
        public async Task<IActionResult> AddStudentSkill([FromBody]StudentSkill newStudentSkill)
        {
            //Get Request's User 
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            if (!claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }
            //Get Student Profile
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);
            newStudentSkill.StudentId = User.Id;
            if( _profileSrv.AddStudentSkill(newStudentSkill))
                return Ok(new { Status = "Success"});
            else
                return Ok(new { Status = "Failed",Msg="Skill Exist" });
        }

        [Authorize(policy: "Students")]
        [Route("api/deletestudentskill")]
        [HttpPost]
        public IActionResult DeleteStudentSkill([FromBody]StudentSkill studentSkill)
        {
            _profileSrv.DeleteStudentSkill(studentSkill.Id);
            return Ok(new { Status = "Success" });
        }

        [Authorize(policy: "Students")]
        [Route("api/addstudentquestion")]
        [HttpPost]
        public async Task<IActionResult> AddStudentQuestion([FromBody]Question newQuestion)
        {
            //Get Request's User 
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            if (!claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }
            //Get Student Profile
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);
            newQuestion.UserId = User.Id;
            newQuestion.Likes = 0;
            newQuestion.Dislikes = 0; 
            var Question = _profileSrv.AddQuestion(newQuestion);

            try
            {
                return Ok(new
                {
                    Status = "Success",
                    Question = new
                    {
                        QuestionHead = Question.QuestionHead,
                        QuestionId = Question.Id,
                        Likes = Question.Likes,
                        Dislikes = Question.Dislikes
                        //,
                        //                    Answers =
                        //     from a in Question.Answers
                        //     select new
                        //     {
                        //         Answer = a.QuestionAnswer,
                        //         AnswerId = a.Id,
                        //         UserId = a.UserId,
                        //         Username = a.User.Name,
                        //         UserImage = _profileSrv.GetStudentProfile(User).Image
                        //     }//End Answers
                                        }//End Questions
                                    }//End Response Object
                     );
            }
            catch
            {
                return Ok(new
                {
                    Status = "Success",
                    Question = new
                    {
                        QuestionHead = Question.QuestionHead,
                        QuestionId = Question.Id,
                        Likes = Question.Likes,
                        Dislikes = Question.Dislikes
                    ,
                        Answers =new { }
                     
                        }//End Questions
                    }//End Response Object
                 );
            }

        }

        [Authorize(policy: "Students")]
        [Route("api/deletestudentQuestion")]
        [HttpPost]
        public IActionResult DeleteStudentQuestion([FromBody]Question question)
        {
            _profileSrv.DeleteQuestion(question.Id);
            return Ok(new { Status = "Success" });
        }

        [Authorize(policy: "Students")]
        [Route("api/followfriend")]
        [HttpPost]
        public async Task<IActionResult> FollowFriend([FromBody]ApplicationUser friend)
        {
            //Get Request's User 
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            if (!claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }
            //Get Student Profile
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);
            var result = _profileSrv.FollowFriend(User, friend);
            return Ok(new { Status = "Success", Id = result.Id });
        }
        [Authorize(policy: "Students")]
        [Route("api/unfollowfriend")]
        [HttpGet]
        public IActionResult UnFollowFriend(int id)
        {
            var result = _profileSrv.UnFollowFriendint(id);
            return Ok(new { Status = "Success"});
        }

        [AllowAnonymous]
        //[Route("api/upload")]
        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            var files = Request.Form.Files;
            if (file.Length > 0 && file.Length < 5000 )
            {
                var checkExtension = Path.GetExtension(file.FileName).ToLower();
                var allowedExtentions = new[] {".png",".jpg",".jpeg" };

                if (!allowedExtentions.Contains(checkExtension))
                {
                    return Ok(new { Status = "Failed", Msg = "Wrong Image Type We Only Allow .png .jpg .jpeg" });
                }
                string path = Path.Combine(_env.WebRootPath, "uploadedimages");
                using (var fs = new FileStream(Path.Combine(path, file.FileName+Guid.NewGuid()), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                return Ok(new { Status = "Success" });
            }
            return Ok(new {Status ="Failed",Msg= "Exceed Sizes Limit" });
        }
    }
}
