﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

global using AlibabaCloud.SDK.Dypnsapi20170525.Models;
global using AlibabaCloud.TeaUtil.Models;
global using BlazorComponent;
global using BlazorComponent.I18n;
global using FluentValidation;
global using IdentityModel;
global using IdentityServer4;
global using IdentityServer4.Configuration;
global using IdentityServer4.Events;
global using IdentityServer4.Extensions;
global using IdentityServer4.Models;
global using IdentityServer4.Services;
global using IdentityServer4.Stores;
global using IdentityServer4.Test;
global using IdentityServer4.Validation;
global using Mapster;
global using Masa.Auth.Security.OAuth.Providers;
global using Masa.Auth.Web.Sso;
global using Masa.Auth.Web.Sso.Global;
global using Masa.Auth.Web.Sso.Infrastructure;
global using Masa.Auth.Web.Sso.Infrastructure.Aliyun;
global using Masa.Auth.Web.Sso.Infrastructure.Attributes;
global using Masa.Auth.Web.Sso.Infrastructure.Environment;
global using Masa.Auth.Web.Sso.Infrastructure.Events;
global using Masa.Auth.Web.Sso.Infrastructure.Options;
global using Masa.Auth.Web.Sso.Infrastructure.Services;
global using Masa.Auth.Web.Sso.Infrastructure.Stores;
global using Masa.Auth.Web.Sso.Infrastructure.Validations;
global using Masa.Auth.Web.Sso.Pages.Account.Login;
global using Masa.Auth.Web.Sso.Pages.Account.Login.Model;
global using Masa.Auth.Web.Sso.Pages.Account.Login.RegisterSections;
global using Masa.Auth.Web.Sso.Pages.Account.Logout.Model;
global using Masa.Blazor;
global using Masa.BuildingBlocks.Caching;
global using Masa.BuildingBlocks.StackSdks.Auth;
global using Masa.BuildingBlocks.StackSdks.Auth.Contracts.Consts;
global using Masa.BuildingBlocks.StackSdks.Auth.Contracts.Enum;
global using Masa.BuildingBlocks.StackSdks.Auth.Contracts.Model;
global using Masa.BuildingBlocks.StackSdks.Auth.Service;
global using Masa.BuildingBlocks.StackSdks.Config;
global using Masa.BuildingBlocks.StackSdks.Pm.Model;
global using Masa.Contrib.Caching.Distributed.StackExchangeRedis;
global using Masa.Contrib.Configuration.ConfigurationApi.Dcc.Options;
global using Masa.Contrib.StackSdks.Config;
global using Masa.Contrib.StackSdks.Tsc;
global using Masa.Utils.Ldap.Novell;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Server;
global using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.DataProtection;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.JSInterop;
global using StackExchange.Redis;
global using System.Collections.Concurrent;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Security.Claims;
global using System.Security.Cryptography.X509Certificates;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using static AlibabaCloud.SDK.Dypnsapi20170525.Models.GetAuthTokenResponseBody;
global using Masa.Auth.Web.Sso.Pages.Account.Logout;
