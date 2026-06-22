using System.Text.Json.Serialization;
using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Blocks.Core.FluentValidation;
using Blocks.Domain;
using FluentValidation;
using MediatR;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateArticle;

/// <summary>
/// 创建文章的命令对象，包含了创建文章所需的基本信息，如期刊ID、标题、范围和文章类型。
/// </summary>
/// <param name="JournalId"></param>
/// <param name="Title"></param>
/// <param name="Scope"></param>
/// <param name="ArticleType"></param>
public record CreateArticleCommand(int JournalId, string Title, string Scope, ArticleType ArticleType)
    : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateArticleCommandValidator : ArticleCommandValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Title));

        RuleFor(x => x.Scope)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Scope));

        RuleFor(x => x.JournalId)
            .GreaterThan(0)
            .WithMessageForInvalidId(nameof(CreateArticleCommand.JournalId));
    }
}