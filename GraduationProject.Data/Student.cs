using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class Student : BaseEntity
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
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
