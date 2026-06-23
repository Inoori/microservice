using Articles.Abstractions.Enums;
using Blocks.Domain.ValueObject;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class AssetName : StringValueObject
{
    private AssetName(string value) => Value = value;

    public static AssetName AssetTypeDefinition(AssetTypeDefinition assetType) => new(value: assetType.Name.ToString());
}