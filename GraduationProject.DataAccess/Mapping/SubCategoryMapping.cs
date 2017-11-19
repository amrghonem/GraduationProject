using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GraduationProject.Data;

namespace GraduationProject.DataAccess.Mapping
{
    public class SubCategoryMapping : EntityTypeConfiguration<SubCategory>
    {
        public override void Map(EntityTypeBuilder<SubCategory> builder)
        {
            //builder.HasOne(c => c.MainCategory).WithMany(c => c.SubCategories).HasForeignKey(c => c.MainCategoryId);
        }
    }
}
