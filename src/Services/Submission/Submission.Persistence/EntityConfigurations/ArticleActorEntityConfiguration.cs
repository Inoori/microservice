using Articles.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
    public void Configure(EntityTypeBuilder<ArticleActor> builder)
    {
        // 配置继承
        builder.HasDiscriminator(a => a.TypeDiscriminator)
            .HasValue<ArticleActor>(nameof(ArticleActor))
            .HasValue<ArticleAuthor>(nameof(ArticleAuthor));

        builder.HasKey(e => new { e.ArticleId, e.PersonId, e.Role });

        builder.Property(e => e.Role)
            .HasConversion<EnumToStringConverter<UserRoleType>>()
            .HasDefaultValue(UserRoleType.Aut);

        builder.HasOne(a => a.Article)
            .WithMany(a => a.Actors)
            .HasForeignKey(a => a.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Person)
            .WithMany(p => p.ArticleActors)
            .HasForeignKey(a => a.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}