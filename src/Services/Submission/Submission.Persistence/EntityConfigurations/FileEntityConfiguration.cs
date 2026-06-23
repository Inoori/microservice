using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Submission.Domain.ValueObjects.File;

namespace Submission.Persistence.EntityConfigurations;

public static class FileEntityConfiguration
{
    public static void Configure(ComplexPropertyBuilder<File> builder)
    {
        builder.Property(f => f.OriginalName).HasMaxLength(MaxLength.C256)
            .HasComment("Original file name,with extension");

        builder.Property(f => f.FileServerId).HasMaxLength(MaxLength.C64);

        builder.Property(f => f.Size).HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(f => f.Extension,
            complexBuilder =>
            {
                complexBuilder.Property(c => c.Value).HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C8);
            });

        builder.ComplexProperty(f => f.Name, complexBuilder =>
        {
            complexBuilder.Property(c => c.Value).HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                .HasMaxLength(MaxLength.C64)
                .HasComment("Final name of the file after renaming");
        });
    }
}