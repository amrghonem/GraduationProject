using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class StudentSkill : BaseEntity
    {

        public ApplicationUser Student { get; set; }
        public virtual Skill Skill { get; set; }
        public string StudentId { get; set; }
        public int SkillId { get; set; }
        
    }
}
