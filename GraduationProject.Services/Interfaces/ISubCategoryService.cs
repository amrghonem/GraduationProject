using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface ISubCategoryService
    {
        SubCategory CreateSubCategory(SubCategory subcat,int maincatid);
        IQueryable<SubCategory> AllSubCategoreis();
        int DeleteSubCategory(int id);
    }
}
