using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Answer :BaseEntity
    {
        public string QuestionAnswer { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
