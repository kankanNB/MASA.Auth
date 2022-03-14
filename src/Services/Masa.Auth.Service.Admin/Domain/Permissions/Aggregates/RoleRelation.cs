﻿namespace Masa.Auth.Service.Domain.Permissions.Aggregates;

public class RoleRelation : Entity<Guid>
{
    public Guid ParentId { get; private set; }

    public Role Role { get; set; } = null!;
}

