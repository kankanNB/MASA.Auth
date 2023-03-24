﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Application.Permissions.Commands
{
    public record SyncPermissionRedisCommand : Command
    {
        public int Count { get; set; }
    }
}
