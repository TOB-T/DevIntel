using DevIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Infrastructure.Configurations
{
    public class IntelEntryConfiguration : IEntityTypeConfiguration<IntelEntry>
    {
        public void Configure(EntityTypeBuilder<IntelEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Tags).HasConversion<string>();
            builder.HasOne(e => e.CreatedBy)
                   .WithMany(u => u.IntelEntries)
                   .HasForeignKey(e => e.CreatedById)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
