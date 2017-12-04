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
        private IQuestionLikeService _queLikeSrv;

        public NewsFeedController(INewsFeedService newsFeedSrv
            ,UserManager<ApplicationUser> userManager
            , IStudentProfileService studSrv,
            IQuestionLikeService queLikeSrv)
        {
            _newsFeedSrv = newsFeedSrv;
            _userManager = userManager;
            _studSrv = studSrv;
            _queLikeSrv = queLikeSrv;
        }
        [HttpPost]
        [Route("api/addanswer")]
        [Authorize]
        public async Task<IActionResult> CreateAnswer([FromBody]Answer answer)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userEmail = claim.Value;
            var User = await _userManager.FindByEmailAsync(userEmail);

            answer.UserId = User.Id;
            var result = _newsFeedSrv.AddAnswer(answer);
           if (result!=null)
                return Ok(new { Status = "Success" ,AddedAnswer = result });
            return Ok(new { Status = "False" });
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

            return Ok(new { status = "Success",StudentData = new
            {
                Username = User.Name,
                StudentId = User.Id,
                Image = studentData.Image,
                Title = studentData.Title,
                Followers = _studSrv.StudentFollowersCount(User.Id),
                Following = _studSrv.StudentFollowingCount(User.Id),
                Gender = studentData.User.Gender
            }
            , Feed = _newsFeedSrv.FollowingQuestions(User.Id).OrderByDescending(q=>q.Id) });
        }

        [Route("api/getquestionsbyid")]
        [HttpPost]
        public IActionResult GetQuestionsByIds([FromBody]List<int> ids)
        {
            var Questions = _newsFeedSrv.GetQuestionsByIds(ids);
            return Ok(new{Status ="Success", Questions });    
        }

        [HttpPost]
        [Route("api/likedislikequestion")]
        public async Task<IActionResult> LikeDislikeQuestionAsync([FromBody]QuestionLike like)
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
            like.UserId = User.Id;
            var result = _queLikeSrv.LikeDislikeQuestion(like);
            if(result.Item1  !=null)
            {
                return Ok(new { Status = "Success" ,Action = result.Item1.State,Likes = result.Item2,Dislikes = result.Item3 });
            }
            return Ok(new { Status = "Failed" });
        }
    }
}
