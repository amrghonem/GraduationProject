using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Course :BaseEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
