using Articles.Abstractions.Enums;
using Submission.Domain.ValueObjects;
using File = Submission.Domain.ValueObjects.File;

namespace Submission.Domain.Entities;

public partial class Asset
{
    public int Id { get; init; }
    public AssetName Name { get; private set; } = null!;

    public AssetType Type { get; private set; }

    public int ArticleId { get; private set; }

    public Article Article { get; private set; } = null!;

    public File File { get; set; } = null!;
}