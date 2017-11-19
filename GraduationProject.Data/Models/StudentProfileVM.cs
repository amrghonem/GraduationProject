using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data.Models
{
    public class StudentProfileVM
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string SchholYearFrom { get; set; }
        public string SchholYearTo { get; set; }
        public string UniverstyYearFrom { get; set; }
        public string UniverstyYearTo { get; set; }
        public bool? FirstVisit { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Title { get; set; }
        public IEnumerable<StudentSkillVM> Skills { get; set; }
        public IEnumerable<StudentExamVM> Exams { get; set; }
        public IEnumerable<StudentCourseVM> Courses { get; set; }
        public IEnumerable<StudentFollowingVM> Friends { get; set; }
        public IEnumerable<StudentQuestionVM> Questions { get; set; }
    }
}
