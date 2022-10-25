﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Web.Sso.Infrastructure.Validations;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    readonly IAuthClient _authClient;

    public ResourceOwnerPasswordValidator(IAuthClient authClient)
    {
        _authClient = authClient;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _authClient.UserService
                                           .ValidateCredentialsByAccountAsync(context.UserName, context.Password);
        if (user != null)
        {
            context.Result = new GrantValidationResult(
                 subject: user!.Id.ToString(),
                 authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                 claims: GetUserClaims(context.UserName));
        }
        else
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "invalid custom credential");
        }
    }

    private Claim[] GetUserClaims(string account)
    {
        return new Claim[]
        {
            new Claim("account", account)
        };
    }
}
