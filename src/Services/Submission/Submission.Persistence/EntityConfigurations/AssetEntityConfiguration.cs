using Articles.Abstractions.Enums;
using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Type)
            .HasConversion<EnumToStringConverter<AssetType>>();

        builder.ComplexProperty(asset => asset.Name, complexBuilder =>
        {
            complexBuilder.Property(c => c.Value)
                .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                .HasMaxLength(MaxLength.C256)
                .IsRequired();
        });

        builder.ComplexProperty(asset => asset.File, FileEntityConfiguration.Configure);
    }
}