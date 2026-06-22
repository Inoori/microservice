using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence;

public class SubmissionDbContext(DbContextOptions<SubmissionDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Represents the collection of articles in the database context.
    /// </summary>
    public virtual DbSet<Article> Articles { get; set; }

    /// <summary>
    /// Represents the collection of journals in the database context.
    /// </summary>
    public virtual DbSet<Journal> Journals { get; set; }

    /// <summary>
    /// Represents the collection of persons in the database context.
    /// </summary>
    public virtual DbSet<Person> Persons { get; set; }

    /// <summary>
    /// Represents the collection of authors in the database context.
    /// </summary>
    public virtual DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Represents the collection of article participants in the database context.
    /// </summary>
    public virtual DbSet<ArticleActor> ArticleActors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubmissionDbContext).Assembly);
    }
}