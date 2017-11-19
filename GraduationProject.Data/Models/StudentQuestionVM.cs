using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data.Models
{
    public class StudentQuestionVM
    {
        public string QuestionHead { get; set; }
        public long Likes { get; set; }
        public long Dislikes { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public List<QuestionAnswerVM> Answers { get; set; }
    }
}
