﻿namespace MASA.Auth.Service.Domain.Aggregate.Permissions;

public class Permission : AuditAggregateRoot<Guid, Guid>
{
    public string AppId { get; private set; } = "";

    public string Name { get; private set; } = "";

    public string Code { get; private set; } = "";

    public string Description { get; private set; } = "";

    public bool Enabled { get; private set; }

    public string Url { get; private set; } = "";

    public string Icon { get; private set; } = "";

    public PermissionType Type { get; private set; }

    private List<PermissionItem> permissionItems = new();

    public IReadOnlyCollection<PermissionItem> PermissionItems => permissionItems;
}

public enum PermissionType
{
    Menu,
    Element,
    Api,
    Data
}