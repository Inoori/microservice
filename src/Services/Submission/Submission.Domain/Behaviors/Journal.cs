using Articles.Abstractions.Enums;

namespace Submission.Domain.Entities;

public partial class Journal
{
    /// <summary>
    /// 创建文章
    /// </summary>
    /// <param name="title"></param>
    /// <param name="type"></param>
    /// <param name="scop"></param>
    /// <returns></returns>
    public Article CreateArticle(string title, ArticleType type, string scop)
    {
        var article = new Article()
        {
            Title = title,
            Scope = scop,
            Type = type,
            Journal = this,
            Stage = ArticleStage.Created
        };

        _articles.Add(article);
        
        //todo: add domain event later

        return article;
    }
}