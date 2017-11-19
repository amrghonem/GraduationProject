using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class MainCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
