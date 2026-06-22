using Articles.Abstractions;
using Blocks.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;
using Submission.Persistence;

namespace Submission.Application.Features.CreateAndAssignAuthor;

public class CreateAndAssignAuthorCommandHandler(SubmissionDbContext dbContext)
    : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken cancellationToken)
    {
        var article = await dbContext.Articles.AsTracking()
            .FirstOrDefaultAsync(a => a.Id == command.ArticleId, cancellationToken: cancellationToken);

        if (article is null) throw new NotFoundException("Article not found");

        Author? author = command.UserId is null
            ? Author.Create(command.Email!, command.FirstName!, command.LastName!, command.Title, command.Affiliation)
            : null; //todo: author is an user

        article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}