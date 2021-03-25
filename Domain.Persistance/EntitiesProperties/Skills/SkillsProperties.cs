using Domain.Entities.Entity.Skills;
using Domain.Persistance.EntitiesProperties.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistance.EntitiesProperties.Skills
{
    public class SkillsProperties : PropertiesBase<Tbl_Skills>
    {
        public override void Configure(EntityTypeBuilder<Tbl_Skills> builder)
        {
            builder.HasMany(e => e.Tbl_CandidateSkills)
               .WithOne(e => e.Tbl_Skills)
               .HasForeignKey(e => e.SkillId)
               .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
