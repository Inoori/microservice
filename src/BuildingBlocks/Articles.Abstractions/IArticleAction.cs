using Blocks.Domain;

namespace Articles.Abstractions;

public interface IArticleAction : IAuditableAction
{
    int ArticleId { get; }
}

public interface IArticleAction<out TActionType> : IAuditableAction<TActionType>, IArticleAction
    where TActionType : Enum;