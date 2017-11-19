using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GraduationProject.Data
{
    public class Friend :BaseEntity
    {
        //[ForeignKey("FriendOneId")]
        //public ApplicationUser FriendOne { get; set; }
        //[ForeignKey("FriendTwoId")]
        //public ApplicationUser FriendTwo { get; set; }


        [StringLength(450)]
        public string FriendOneId { get; set; }

        [StringLength(450)]
        public string FriendTwoId { get; set; }

        public virtual ApplicationUser FriendOne { get; set; }

        public virtual ApplicationUser FriendTwo { get; set; }
    }
}
