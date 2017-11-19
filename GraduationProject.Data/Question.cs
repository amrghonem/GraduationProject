using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Question :BaseEntity
    {
        public string QuestionHead { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public long Likes { get; set; }
        public long Dislikes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }
}
