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


    /// <summary>
    /// 创建一个新的资产实例，并将其添加到文章的资产集合中。
    /// 如果已达到当前资产类型的最大数量限制，则抛出异常。
    /// </summary>
    /// <param name="assetTypeDefinition">资产类型定义，描述了资产类型及其相关限制。</param>
    /// <returns>创建的资产实例。</returns>
    /// <exception cref="DomainException">当同一资产类型的数量超过允许的最大限制时抛出。</exception>
    public Asset CreateAsset(AssetTypeDefinition assetTypeDefinition)
    {
        var assetCount = _assets.Count(a => a.Type == assetTypeDefinition.Id);

        if (assetCount >= assetTypeDefinition.MaxAssetCount)
            throw new DomainException(
                $"The maximum number of files,{assetTypeDefinition.MaxAssetCount}, allowed for {assetTypeDefinition.Name.ToString()} was already reached");

        var asset = Asset.Create(this, assetTypeDefinition);

        _assets.Add(asset);

        return asset;
    }
}