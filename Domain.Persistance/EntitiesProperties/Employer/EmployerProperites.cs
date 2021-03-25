using Domain.Entities.Entity.Employer;
using Domain.Persistance.EntitiesProperties.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistance.EntitiesProperties.Employer
{
    public class EmployerProperites : PropertiesBase<Tbl_Employer>
    {
        public override void Configure(EntityTypeBuilder<Tbl_Employer> builder)
        {
            builder.HasMany(e => e.Tbl_CandidateEmployers)
               .WithOne(e => e.Tbl_CurrentEmployer)
               .HasForeignKey(e => e.CurrentEmployerId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.Tbl_CandidateEmployers)
              .WithOne(e => e.Tbl_CurrentEmployer)
              .HasForeignKey(e => e.PreviousEmployerId)
              .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
