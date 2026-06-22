using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Blocks.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;
using Submission.Persistence;

namespace Submission.Application.Features.CreateArticle;

public class CreateArticleCommandHandler(SubmissionDbContext dbContext)
    : IRequestHandler<CreateArticleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var journal = await dbContext.Journals.FindAsync([command.JournalId], cancellationToken: cancellationToken);

        if (journal is null) throw new NotFoundException("journal not found");

        var article = journal.CreateArticle(command.Title, command.ArticleType, command.Scope);

        await AssignCurrentUserAsAuthor(article, command);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }

    private async Task AssignCurrentUserAsAuthor(Article article, CreateArticleCommand command)
    {
        var author = await dbContext.Authors.SingleOrDefaultAsync(author => author.UserId == command.CreatedById);

        if (author is not null)
            article.AssignAuthor(author, [ContributionArea.OriginalDraft], isCorrespondingAuthor: true);
    }
}