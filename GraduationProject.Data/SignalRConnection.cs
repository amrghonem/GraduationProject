using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class SignalRConnection:BaseEntity
    {
        public string ConnectionId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
