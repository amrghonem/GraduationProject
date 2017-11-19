using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Exam :BaseEntity
    {
        public string Title { get; set; }
        public decimal Degree { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
