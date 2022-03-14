﻿global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Masa.Auth.Service.Application.Organizations.Commands;
global using Masa.Auth.Service.Application.Organizations.Models;
global using Masa.Auth.Service.Application.Organizations.Queries;
global using Masa.Auth.Service.Application.Permissions.Commands;
global using Masa.Auth.Service.Application.Permissions.Models;
global using Masa.Auth.Service.Application.Permissions.Queries;
global using Masa.Auth.Service.Application.Subjects.Commands;
global using Masa.Auth.Service.Application.Subjects.Models;
global using Masa.Auth.Service.Application.Subjects.Queries;
global using Masa.Auth.Service.Domain.Organizations.Aggregates;
global using Masa.Auth.Service.Domain.Organizations.Factories;
global using Masa.Auth.Service.Domain.Organizations.Repositories;
global using Masa.Auth.Service.Domain.Permissions.Aggregates;
global using Masa.Auth.Service.Domain.Permissions.Repositories;
global using Masa.Auth.Service.Domain.Sso.Aggregates;
global using Masa.Auth.Service.Domain.Sso.Aggregates.Abstract;
global using Masa.Auth.Service.Domain.Subjects.Aggregates;
global using Masa.Auth.Service.Domain.Subjects.Repositories;
global using Masa.Auth.Service.Infrastructure;
global using Masa.Auth.Service.Infrastructure.Const;
global using Masa.Auth.Service.Infrastructure.Enums;
global using Masa.Auth.Service.Infrastructure.Middleware;
global using Masa.Auth.Service.Infrastructure.Models;
global using Masa.BuildingBlocks.Configuration;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Ddd.Domain.Values;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.Contrib.Configuration;
global using Masa.Contrib.Data.Contracts.EF;
global using Masa.Contrib.Data.UoW.EF;
global using Masa.Contrib.Ddd.Domain;
global using Masa.Contrib.Ddd.Domain.Repository.EF;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Commands;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Queries;
global using Masa.Contrib.Service.MinimalAPIs;
global using Masa.Utils.Caching.Redis.DependencyInjection;
global using Masa.Utils.Caching.Redis.Models;
global using Masa.Utils.Data.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using System.Reflection;
global using System.Linq.Expressions;
