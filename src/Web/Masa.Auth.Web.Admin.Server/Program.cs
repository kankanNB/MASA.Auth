// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Masa.Auth.ApiGateways.Caller;
using Masa.Auth.Contracts.Admin.Subjects.Validator;
using Masa.Auth.Web.Admin.Rcl;
using Masa.Auth.Web.Admin.Rcl.Global;
using Masa.Stack.Components;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaStackComponentsForServer("wwwroot/i18n");
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddGlobalForServer();
builder.Services.AddAutoComplete();
builder.Services.AddAuthApiGateways(option => option.AuthServiceBaseAddress = builder.Configuration["AuthServiceBaseAddress"]);
builder.Services.AddSingleton<AddStaffValidator>();
builder.Services.AddTypeAdapter();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();