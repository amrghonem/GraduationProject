using GraduationProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Web.Models
{
    public class NewQuestionSignalrVM
    {
        public List<string> Connection { get; set; }
        public string QuestionHead { get; set; }
        public long Likes { get; set; }
        public long Dislikes { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
        public List<object> Answers { get; set; }
    }
}
