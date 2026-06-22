namespace Blocks.Domain;

/// <summary>
/// 代表可审计的操作，可追踪变更或审查以保持历史记录.
/// </summary>
public interface IAuditableAction
{
    DateTime CreatedAt { get; }

    int CreatedById { get; set; }
}

public interface IAuditableAction<out TActionType> : IAuditableAction where TActionType : Enum
{
    public TActionType ActionType { get; }
}