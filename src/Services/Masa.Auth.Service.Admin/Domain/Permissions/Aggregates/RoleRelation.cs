﻿namespace Masa.Auth.Service.Admin.Domain.Permissions.Aggregates;

public class RoleRelation : AuditEntity<Guid, Guid>, ISoftDelete
{
    public bool IsDeleted { get; private set; }

    public Guid ParentId { get; private set; }

    public Guid RoleId { get; private set; }

    public Role Role { get; set; } = null!;

    public RoleRelation(Guid parentId)
    {
        ParentId = parentId;
    }
}

