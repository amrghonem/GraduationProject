using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Services.Interfaces;
using GraduationProject.Web.Models;
using GraduationProject.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Web.Controllers.Api
{
    public class SubCategoriesController : Controller
    {
        private ISubCategoryService _subCatSrv;

        public SubCategoriesController(ISubCategoryService subCatSrv)
        {
            _subCatSrv = subCatSrv;
        }
        [HttpGet]
        [Route("api/allsubcategories")]
        public IActionResult GetAllSubCategories()
        {
           
                var subCats = from s in _subCatSrv.AllSubCategoreis()
                              select new
                              {
                                  Name = s.Name,
                                  Id = s.Id,
                                  MainCategory = s.MainCategory.Name
                              };
                return Ok(new { SubCategories = subCats, Status = "Success" });

        }

        [HttpPost]
        [Route("api/createsubcat")]
        public IActionResult CreateSubCategory([FromBody]SubCategoryViewModel VM)
        {
            SubCategory newSubCat = new SubCategory() {
                Name = VM.Name
            };
            var subCategory = _subCatSrv.CreateSubCategory(newSubCat, VM.MainCategoryId);
            if (subCategory != null)
                return Ok(new {Status="Success",SubCategory = new {Name = subCategory.Name , Id = subCategory.Id , MainCategory = subCategory.MainCategory.Name } });
            else
                return Ok(new { Status ="Failed"});
        }

        [HttpGet]
        [Route("api/deletesubcategory")]
        public IActionResult DeleteSubCategory(int id)
        {
            var result = _subCatSrv.DeleteSubCategory(id);
            if (result > 0)
                return Ok(new { Status = "Success" });
            else
                return Ok(new { Status = "Failed" });
        }
    }
}
