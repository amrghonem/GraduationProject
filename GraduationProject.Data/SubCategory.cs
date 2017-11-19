using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Data
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
        public virtual MainCategory MainCategory { get; set; }
    }
}
