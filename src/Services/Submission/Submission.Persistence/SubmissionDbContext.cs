using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence;

public class SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : DbContext(options)
{
    /// <summary>
    /// 表示数据库上下文中的文章集合。
    /// </summary>
    public virtual DbSet<Article> Articles { get; set; }

    /// <summary>
    /// 表示数据库上下文中的期刊集合。
    /// </summary>
    public virtual DbSet<Journal> Journals { get; set; }

    /// <summary>
    /// 表示数据库上下文中的人员集合。
    /// </summary>
    public virtual DbSet<Person> Persons { get; set; }

    /// <summary>
    /// 表示数据库上下文中的作者集合。
    /// </summary>
    public virtual DbSet<Author> Authors { get; set; }

    /// <summary>
    /// 表示文章与参与者之间的关联，包括文章的相关信息和参与者的角色。
    /// </summary>
    public virtual DbSet<ArticleActor> ArticleActors { get; set; }


    /// <summary>
    /// 表示数据库上下文中的资产集合。
    /// </summary>
    public DbSet<Asset> Assets { get; set; }


    /// <summary>
    /// 表示资产类型定义的集合，用于存储提交上下文中的资产类型相关信息。
    /// </summary>
    public DbSet<AssetTypeDefinition> AssetTypeDefinitions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubmissionDbContext).Assembly);
    }
}