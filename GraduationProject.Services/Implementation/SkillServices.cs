using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data;
using GraduationProject.Infrastructure;

namespace GraduationProject.Services.Implementation
{
    public class SkillServices : ISkillServices
    {
        private IRepository<Skill> _skillsRepo;

        public SkillServices(IRepository<Skill> skillsRepo)
        {
            _skillsRepo = skillsRepo;
        }
        public Skill CreateSkill(Skill skill)
        {
            return _skillsRepo.Insert(skill);
        }

        public int DeleteSkill(int id)
        {
            return _skillsRepo.Delete(_skillsRepo.Get(id));
        }

        public IEnumerable<Skill> AllSkills()
        {
            return _skillsRepo.GetAll();
        }
    }
}
