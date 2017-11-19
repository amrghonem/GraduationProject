using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data;
using GraduationProject.Infrastructure;

namespace GraduationProject.Services.Implementation
{
    public class MainCategoryService : IMainCategoryService
    {
        private IRepository<MainCategory> _mainCatRepo;

        public MainCategoryService(IRepository<MainCategory> mainCatRepo)
        {
            _mainCatRepo = mainCatRepo;
        }
        public IEnumerable<MainCategory> AllMainCategoreis()
        {
            return _mainCatRepo.GetAll();
        }

        public MainCategory CreateMainCategory(MainCategory cat)
        {
            if (cat == null)
                throw new ArgumentNullException();
            return _mainCatRepo.Insert(cat);
        }

        public int DeleteMainCategory(int id)
        {
            return _mainCatRepo.Delete(_mainCatRepo.Get(id));
        }
    }
}
