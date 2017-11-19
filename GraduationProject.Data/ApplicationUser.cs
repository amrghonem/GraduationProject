using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserType { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<StudentSkill> StudentSkills { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        
        public virtual ICollection<Friend> Friends { get; set; }
        
        public virtual ICollection<Friend> Friends1 { get; set; }
    }
}
