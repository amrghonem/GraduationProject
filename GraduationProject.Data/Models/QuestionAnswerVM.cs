using System;

namespace GraduationProject.Data.Models
{
    public class QuestionAnswerVM
    {
        public string QuestionAnswer { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
    }
}