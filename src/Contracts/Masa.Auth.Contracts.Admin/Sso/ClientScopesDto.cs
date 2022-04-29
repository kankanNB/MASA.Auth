﻿namespace Masa.Auth.Contracts.Admin.Sso;

public class ClientScopesDto
{
    List<string> _allowedScopes = new();

    public List<CheckItemDto<int>> IdentityScopes { get; set; } = new();

    public List<CheckItemDto<int>> ApiScopes { get; set; } = new();

    public List<string> AllowedScopes
    {
        get
        {
            return IdentityScopes.Union(ApiScopes).Where(s => s.Selected).Select(s => s.DisplayValue).ToList();
        }
        set
        {
            _allowedScopes = value;
        }
    }
}
