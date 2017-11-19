using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class StudentExam :BaseEntity
    {
        public ApplicationUser Student { get; set; }
        public Exam Exam { get; set; }
        public string StudentId { get; set; }
        public int ExamId { get; set; }
        public Decimal Degree { get; set; }
    }
}
