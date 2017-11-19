using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Services.Interfaces;
using GraduationProject.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class SkillsController : Controller
    {
        private ISkillServices _skillSrv;

        //Comment

        public SkillsController(ISkillServices skillSrv)
        {
            _skillSrv = skillSrv;
        }
        [Route("api/createskill")]
        [HttpPost]
        public IActionResult CreateSkill([FromBody]Skill newskill)
        {
            if (newskill == null)
                throw new ArgumentNullException();
            var newSkill = _skillSrv.CreateSkill(newskill);
            return Ok(new { Status = "Success", Skill = new {name=newSkill.Name ,id = newSkill.Id } });
        }
        [Route("api/allskills")]
        [HttpGet]
        public IActionResult GetAllSkills()
        {
            return Ok(new {Status ="Success" ,Skills=_skillSrv.AllSkills() });
        }
        [Route("api/deleteskill")]
        [HttpGet]
        public IActionResult DeleteSkill(int id)
        {
            if(_skillSrv.DeleteSkill(id) > 0)
                return Ok(new { Status = "Success" });
            return Ok(new { Status = "Failed" });
        }
    }
}
