using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasDiscriminator(p => p.TypeDiscriminator)
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author));

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnOrder(0);

        builder.Property(p => p.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Title).HasMaxLength(64);
        builder.Property(p => p.Affiliation).HasMaxLength(512).IsRequired()
            .HasComment("Institution or organization they are associated with when they conduct their research.");

        builder.Property(p => p.UserId).IsRequired(false);

        builder.ComplexProperty(p => p.EmailAddress,
            complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(64);
            });

        //indexes
        builder.HasIndex(p => p.UserId).IsUnique();
    }
}