using Articles.Abstractions.Enums;
using Blocks.Exceptions;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CorAut : UserRoleType.Aut;

        if (Actors.Exists(a => a.PersonId == author.Id && a.Role == role))
            throw new DomainException($"Author {author.Id} already assigned to article");

        Actors.Add(new ArticleAuthor()
        {
            Person = author,
            ContributeAreas = contributionAreas,
            Role = role
        });

        //todo: create domain event
    }
}