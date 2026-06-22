using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class JournalEntityConfiguration : IEntityTypeConfiguration<Journal>
{
    public void Configure(EntityTypeBuilder<Journal> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnOrder(0);

        builder.Property(e => e.Name).HasMaxLength(64).IsRequired();
        builder.Property(e => e.Abreviation).HasMaxLength(8).IsRequired();
    }
}