using System.Text.Json.Serialization;
using Articles.Abstractions;
using Blocks.Core.FluentValidation;
using Blocks.Domain;
using FluentValidation;
using MediatR;
using Submission.Domain.Enums;

namespace Submission.Application.Features.Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, IRequest<TResponse>
    where TActionType : Enum
{
    [JsonIgnore] public int ArticleId { get; init; }

    public string? Comment { get; init; }

    [JsonInclude] public abstract TActionType ActionType { get; }

    [JsonIgnore] public string Action => ActionType.ToString();

    [JsonIgnore] public DateTime CreatedAt => DateTime.UtcNow;

    [JsonIgnore] public int CreatedById { get; set; }
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType, IdResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : AbstractValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    protected ArticleCommandValidator()
    {
        RuleFor(a => a.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }
}