using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data;
using GraduationProject.Infrastructure;
using System.Linq;

namespace GraduationProject.Services.Implementation
{
    public class SubCategoryService : ISubCategoryService
    {
        private IRepository<SubCategory> _subCatRepo;
        private IRepository<MainCategory> _MainCatRepo;

        public SubCategoryService(IRepository<SubCategory> subCatRepo, IRepository<MainCategory> mainCatRepo)
        {
            _subCatRepo = subCatRepo;
            _MainCatRepo = mainCatRepo;
        }

        public IQueryable<SubCategory> AllSubCategoreis()
        {
            return _subCatRepo.GetAll();
        }

        public SubCategory CreateSubCategory(SubCategory subcat, int maincatid)
        {
            var mainCat = _MainCatRepo.Get(maincatid);
            subcat.MainCategory = mainCat;
           return  _subCatRepo.Insert(subcat);
        }

        public int DeleteSubCategory(int id)
        {
            return _subCatRepo.Delete(_subCatRepo.Get(id));
        }
    }
}
