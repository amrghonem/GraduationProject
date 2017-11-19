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
    public class MainCategoriesController : Controller
    {
        private IMainCategoryService _mainCategorySrv;

        public MainCategoriesController(IMainCategoryService mainCategorySrv)
        {
            _mainCategorySrv = mainCategorySrv;
        }

        [Route("api/createmaincategory")]
        [HttpPost]
        public IActionResult CreateMainCategories([FromBody]MainCategory newcat)
        {
            if (newcat == null)
                throw new ArgumentNullException();
            return Ok(new { Status = "Success", MainCategories = _mainCategorySrv.CreateMainCategory(newcat) });
        }
        [Route("api/allmaincategories")]
        [HttpGet]
        public IActionResult GetAllMainCategories()
        {
            return Ok(new { Status = "Success", MainCategories = _mainCategorySrv.AllMainCategoreis() });
        }
        [Route("api/deletemaincategories")]
        [HttpGet]
        public IActionResult DeleteMainCategory(int id)
        {
            if (_mainCategorySrv.DeleteMainCategory(id) > 0)
                return Ok(new { Status = "Success" });
            return Ok(new { Status = "Failed" });
        }
    }
}
