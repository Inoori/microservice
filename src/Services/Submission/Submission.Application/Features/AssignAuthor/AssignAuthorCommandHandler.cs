using Articles.Abstractions;
using Blocks.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Submission.Persistence;

namespace Submission.Application.Features.AssignAuthor;

public class AssignAuthorCommandHandler(SubmissionDbContext dbContext)
    : IRequestHandler<AssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AssignAuthorCommand command, CancellationToken cancellationToken)
    {
        var article = await dbContext.Articles.AsTracking()
            .FirstOrDefaultAsync(a => a.Id == command.ArticleId, cancellationToken);

        if (article is null) throw new NotFoundException("article not found");

        var author = await dbContext.Authors.FindAsync([command.AuthorId], cancellationToken);

        if (author is null) throw new NotFoundException("author not found");

        article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}