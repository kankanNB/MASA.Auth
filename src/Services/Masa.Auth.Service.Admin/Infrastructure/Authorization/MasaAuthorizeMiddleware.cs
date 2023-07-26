﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Infrastructure.Authorization;

/// <summary>
/// all route into this,and all RequestDelegateFactory create route handler will Authorization code
/// </summary>
public class MasaAuthorizeMiddleware : IMiddleware, IScopedDependency
{
    readonly IMasaAuthorizeDataProvider _masaAuthorizeDataProvider;
    readonly EndpointRowDataProvider _endpointRowDataProvider;
    readonly ILogger<MasaAuthorizeMiddleware> _logger;
    readonly IMultiEnvironmentMasaStackConfig _multiEnvironmentMasaStackConfig;
    readonly IMultiEnvironmentUserContext _multiEnvironmentUserContext;

    public MasaAuthorizeMiddleware(IMasaAuthorizeDataProvider masaAuthorizeDataProvider,
        EndpointRowDataProvider endpointRowDataProvider,
        ILogger<MasaAuthorizeMiddleware> logger,
        IMultiEnvironmentMasaStackConfig multiEnvironmentMasaStackConfig,
        IMultiEnvironmentUserContext multiEnvironmentUserContext)
    {
        _masaAuthorizeDataProvider = masaAuthorizeDataProvider;
        _endpointRowDataProvider = endpointRowDataProvider;
        _logger = logger;
        _multiEnvironmentMasaStackConfig = multiEnvironmentMasaStackConfig;
        _multiEnvironmentUserContext = multiEnvironmentUserContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var routeEndpoint = endpoint as RouteEndpoint;
        if (routeEndpoint == null)
        {
            await next(context);
            return;
        }
        //exclude unprogrammed route such as dapr 
        if (!_endpointRowDataProvider.Endpoints.Contains(routeEndpoint.RoutePattern.RawText))
        {
            await next(context);
            return;
        }
        var allowAnonymousAttribute = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();
        if (endpoint != null && allowAnonymousAttribute == null)
        {
            var masaAuthorizeAttribute = endpoint?.Metadata.GetMetadata<MasaAuthorizeAttribute>();
            if (masaAuthorizeAttribute != null)
            {
                if (masaAuthorizeAttribute.Roles?.Split(',').ToList()
                .Intersect(await _masaAuthorizeDataProvider.GetRolesAsync()).Any() == true)
                {
                    _logger.LogInformation("----- authentication role passed");
                    await next(context);
                    return;
                }
            }
            var _masaStackConfig = _multiEnvironmentMasaStackConfig.SetEnvironment(_multiEnvironmentUserContext.Environment ?? "");
            var code = masaAuthorizeAttribute?.Code;
            if (string.IsNullOrWhiteSpace(code))
            {
                //dafault code rule
                code = Regex.Replace(context.Request.Path, @"\\", ".");
                code = Regex.Replace(code, "/", ".").Trim('.');
                code = $"{_masaStackConfig.GetServiceId(MasaStackProject.Auth)}.{code}";
            }
            if (!(await _masaAuthorizeDataProvider.GetAllowCodesAsync(_masaStackConfig.GetServiceId(MasaStackProject.Auth))).WildCardContains(code))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }
        await next(context);
    }
}

