using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface ISkillServices
    {
        Skill CreateSkill(Skill skill);
        IEnumerable<Skill> AllSkills();
        int DeleteSkill(int id);
    }
}
