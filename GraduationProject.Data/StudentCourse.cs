using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class StudentCourse :BaseEntity
    {
        public ApplicationUser Student { get; set; }
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
