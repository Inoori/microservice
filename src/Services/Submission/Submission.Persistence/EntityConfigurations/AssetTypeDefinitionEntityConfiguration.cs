using Articles.Abstractions.Enums;
using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class AssetTypeDefinitionEntityConfiguration : IEntityTypeConfiguration<AssetTypeDefinition>
{
    public void Configure(EntityTypeBuilder<AssetTypeDefinition> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(definition => definition.Name).IsUnique();

        builder.Property(e => e.Name).HasConversion<EnumToStringConverter<AssetType>>()
            .HasMaxLength(MaxLength.C64).IsRequired().HasColumnOrder(1);

        builder.Property(e => e.MaxFileSizeInMb)
            .HasDefaultValue(5); //5MB
        builder.Property(e => e.DefaultFileExtension)
            .HasMaxLength(MaxLength.C8)
            .HasDefaultValue("pdf")
            .IsRequired();

        builder.ComplexProperty(e => e.AllowedFileExtensions,
            complexBuilder => complexBuilder.Property(c => c.Extensions)
                .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                .HasColumnType("json")
                .IsRequired());
    }
}