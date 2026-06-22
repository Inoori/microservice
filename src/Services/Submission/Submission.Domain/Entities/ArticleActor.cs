using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

/// <summary>
/// 文章参与者
/// </summary>
public class ArticleActor
{
    public int ArticleId { get; init; }

    public Article Article { get; init; } = null!;

    public int PersonId { get; init; }

    public Person Person { get; init; } = null!;

    public UserRoleType Role { get; init; }

    /// <summary>
    /// ef 类型鉴别
    /// </summary>
    public string TypeDiscriminator { get; init; } = null!;
}