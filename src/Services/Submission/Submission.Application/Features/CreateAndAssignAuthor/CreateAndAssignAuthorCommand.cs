using Articles.Abstractions.Enums;
using Blocks.Core;
using Blocks.Core.FluentValidation;
using FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateAndAssignAuthor;

public record CreateAndAssignAuthorCommand(
    int? UserId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Title,
    string? Affiliation,
    bool IsCorrespondingAuthor,
    HashSet<ContributionArea> ContributionAreas
) : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

public class CreateAndAssignAuthorCommandValidator : ArticleCommandValidator<CreateAndAssignAuthorCommand>
{
    public CreateAndAssignAuthorCommandValidator()
    {
        When(c => c.UserId is null, () =>
        {
            RuleFor(c => c.Email)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Email))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.Email));

            RuleFor(c => c.FirstName)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.FirstName))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateAndAssignAuthorCommand.FirstName));


            RuleFor(c => c.LastName)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.LastName))
                .MaximumLengthWithMessage(MaxLength.C256, nameof(CreateAndAssignAuthorCommand.LastName));
            
            RuleFor(c => c.Title)
                .MaximumLengthWithMessage(MaxLength.C32, nameof(CreateAndAssignAuthorCommand.Title));
            
            RuleFor(c => c.Affiliation)
                .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Affiliation))
                .MaximumLengthWithMessage(MaxLength.C512, nameof(CreateAndAssignAuthorCommand.Affiliation));
        });
        
        RuleFor(c => c.ContributionAreas)
            .NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.ContributionAreas));
    }
}