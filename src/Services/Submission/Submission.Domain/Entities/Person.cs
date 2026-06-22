using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public class Person
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public string FullName => $"{FirstName} {LastName}";

    public string? Title { get; init; }

    public required EmailAddress EmailAddress { get; init; }

    /// <summary>
    /// 所属机构
    /// </summary>
    public required string Affiliation { get; init; }

    public int? UserId { get; init; }

    /// <summary>
    /// 文章参与者导航属性
    /// </summary>
    public IReadOnlyList<ArticleActor> ArticleActors { get; private set; } = [];

    /// <summary>
    /// ef 鉴别器，用于ef core能够区分继承类型
    /// </summary>
    public string TypeDiscriminator { get; init; } = null!;
}