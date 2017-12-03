using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class QuestionLike : BaseEntity
    {
        public virtual ApplicationUser User { get; set; }
        public virtual Question Question { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime Date { get; set; }
        //Like-Dislike-NoAction
        public string State { get; set; }
    }
}
