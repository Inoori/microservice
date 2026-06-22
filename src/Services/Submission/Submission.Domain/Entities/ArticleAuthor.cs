using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

/// <summary>
/// 文章作者
/// </summary>
public class ArticleAuthor : ArticleActor
{
    public HashSet<ContributionArea> ContributeAreas { get; init; } = null!;
}