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
using GraduationProject.Web.Models;

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
        public async Task<IActionResult> EditStudentProfile([FromBody]EditProfileVM student)
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

            Student studentToEdit = new Student();
            studentToEdit.Id = student.Id;
            studentToEdit.Info = student.Info;
            studentToEdit.Title = student.Title;
            studentToEdit.Universty = student.Universty;
            studentToEdit.School = student.School;
            studentToEdit.SchholYearFrom = student.SchholYearFrom;
            studentToEdit.SchholYearTo = student.SchholYearTo;
            studentToEdit.UniverstyYearFrom = student.UniverstyYearFrom;
            studentToEdit.UniverstyYearTo = student.UniverstyYearTo;
            studentToEdit.ApplicationUserId = User.Id;
            ApplicationUser userToEdit = new ApplicationUser();
            userToEdit = User;
            userToEdit.Gender = student.Gender;
            userToEdit.BirthDate =Convert.ToDateTime(student.Birthdate);
            userToEdit.Name = student.Username;
            var updatedProfileInfo = _profileSrv.EditProile(studentToEdit,userToEdit);

            return Ok(new { Status = "Success", ProfileInfo = new {
                Image =updatedProfileInfo.Image,
                Info = updatedProfileInfo.Info,
                School =updatedProfileInfo.School,
                Universty = updatedProfileInfo.Universty,
                Username = User.Name,
                SchoolYearFrom = student.SchholYearFrom,
                SchoolYearTo = student.SchholYearTo,
                UniverstyYearFrom = student.UniverstyYearFrom,
                UniverstyYearTo = student.UniverstyYearTo,
                Gender = User.Gender,
                BirthDate = User.BirthDate.ToString("MM/dd/yyyy")
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
            var skill = _profileSrv.AddStudentSkill(newStudentSkill); 
            if ( skill !=null)
                return Ok(new { Status = "Success", Id =skill.Id,SkillName = skill.SkillName});
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

            //SignalR Layer .
            // 1) Get List Of Followings Connections Ids .

            // 2) Get Question Object .

            // 3) Call SignalR Api Pass Parameters To It .

            try
            {
                return Ok(new
                {
                    Status = "Success",
                    Question = new
                    {
                        QuestionHead = Question.QuestionHead,
                        Id = Question.Id,
                        Likes = Question.Likes,
                        Dislikes = Question.Dislikes,
                        Username = User.Name,
                        UserId = User.Id,
                        Image = "",
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
                        Id = Question.Id,
                        Likes = Question.Likes,
                        Dislikes = Question.Dislikes,
                        Username= User.Name,
                        UserId = User.Id,
                        Image = "",
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

        [Authorize(policy: "Students")]
        [Route("api/uploadstudentimage")]
        [HttpPost]
        public async Task<IActionResult> UploadStudentImage(IFormFile file)
        {
            var files = Request.Form.Files;
            if (file.Length > 0 && file.Length < 5000)
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

                var result = _profileSrv.UpdateStudentImage(User.Id, path);
                return Ok(new { Status = "Success", ImagePath = result });
            }
            return Ok(new {Status ="Failed",Msg= "Exceed Sizes Limit" });
        }
    }
}
