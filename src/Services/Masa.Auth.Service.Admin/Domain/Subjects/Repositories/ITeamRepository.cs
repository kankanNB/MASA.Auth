﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Domain.Subjects.Repositories;

public interface ITeamRepository : IRepository<Team, Guid>
{
    Task<Team> GetByIdAsync(Guid id);

    bool Any(Expression<Func<Team, bool>> predicate);
}
