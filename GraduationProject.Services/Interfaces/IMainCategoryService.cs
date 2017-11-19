using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface IMainCategoryService
    {
        MainCategory CreateMainCategory(MainCategory cat);
        IEnumerable<MainCategory> AllMainCategoreis();
        int DeleteMainCategory(int id);
    }
}
