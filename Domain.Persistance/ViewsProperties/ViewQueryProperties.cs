using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Persistance.ViewsProperties
{
    public class ViewQueryProperties<T> : IEntityTypeConfiguration<T> where T : class
    {
        private readonly string viewName;

        public ViewQueryProperties(string ViewName)
        {
            viewName = ViewName;
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey().ToView(viewName);
        }
    }
}
