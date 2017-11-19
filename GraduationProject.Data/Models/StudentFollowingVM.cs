using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data.Models
{
    public class StudentFollowingVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public string FriendId { get; set; }
        public string FriendImage { get; set; }
        public string Gender { get; set; }

    }
}
