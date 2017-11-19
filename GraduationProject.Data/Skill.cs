using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<StudentSkill> StudentSkills { get; set; }
    }
}
