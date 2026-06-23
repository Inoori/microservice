using Articles.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// Configures the entity framework metadata for the <see cref="Article"/> entity.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface
/// to enforce configurations for the <see cref="Article"/> entity in the database context.
/// It defines the schema, table mapping, and constraints for the entity.
/// </remarks>
internal class ArticleEntityConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnOrder(0);
        builder.Property(e => e.Title).HasMaxLength(256).IsRequired();
        builder.Property(e => e.Scope).HasMaxLength(2048).IsRequired();
        builder.Property(e => e.Type)
            .HasConversion<EnumToStringConverter<ArticleType>>();

        builder.HasOne(e => e.Journal)
            .WithMany(j => j.Articles)
            .HasForeignKey(e => e.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Assets)
            .WithOne(a => a.Article)
            .HasForeignKey(a => a.ArticleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}