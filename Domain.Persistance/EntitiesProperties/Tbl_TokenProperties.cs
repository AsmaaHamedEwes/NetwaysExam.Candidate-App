using Domain.Entities.Entity;
using Domain.Persistance.EntitiesProperties.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace Domain.Persistance.EntitiesProperties
{
    public class Tbl_TokenProperties : PropertiesBase<Tbl_Token>
    {
        public override void Configure(EntityTypeBuilder<Tbl_Token> builder)
        {
            builder.HasIndex(e => e.Token).IsUnique();
            builder.HasIndex(e => e.TokenRefresh).IsUnique();
            base.Configure(builder);

        }
    }
}
