using Domain.Entities.Entity.Candidate;
using Domain.Persistance.EntitiesProperties.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistance.EntitiesProperties.Candidate
{
    public class CandidateProperties : PropertiesBase<Tbl_Candidate>
    {
        public override void Configure(EntityTypeBuilder<Tbl_Candidate> builder)
        {
            //builder.HasMany(e => e.Tbl_CandidateEmployers)
            //   .WithOne(e => e.Tbl_Candidate)
            //   .HasForeignKey(e => e.CandidateId)
            //   .OnDelete(DeleteBehavior.Restrict);
            //builder.HasMany(e => e.Tbl_CandidateSkills)
            //               .WithOne(e => e.Tbl_Candidate)
            //               .HasForeignKey(e => e.CandidateId)
            //               .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
