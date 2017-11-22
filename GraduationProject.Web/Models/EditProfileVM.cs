using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Web.Models
{
    public class EditProfileVM
    {
        public string School { get; set; }
        public string Universty { get; set; }
        public string SchholYearFrom { get; set; }
        public string SchholYearTo { get; set; }
        public string UniverstyYearFrom { get; set; }
        public string UniverstyYearTo { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public bool FirstVisit { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string ApplicationUserId { get; set; }
        public int Id { get; set; }

    }
}
