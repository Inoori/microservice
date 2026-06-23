using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

/// <summary>
/// 文章
/// </summary>
public partial class Article
{
    public int Id { get; init; }

    public required string Title { get; set; }

    public required string Scope { get; set; }

    public required ArticleType Type { get; set; }

    public ArticleStage Stage { get; internal set; }


    /// <summary>
    /// 期刊ID
    /// </summary>
    public int JournalId { get; init; }

    /// <summary>
    /// 期刊实体
    /// </summary>
    public required Journal Journal { get; init; }


    /// <summary>
    /// 获取文章的参与者列表。
    /// </summary>
    public List<ArticleActor> Actors { get; } = [];

    /// <summary>
    /// 表示与文章相关的资源集合。
    /// </summary>
    private readonly List<Asset> _assets = [];

    /// <summary>
    /// 文章的资源集合
    /// </summary>
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();
}