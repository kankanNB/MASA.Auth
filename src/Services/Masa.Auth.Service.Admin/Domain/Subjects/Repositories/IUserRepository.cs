﻿namespace Masa.Auth.Service.Admin.Domain.Subjects.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetDetail(Guid id);
}
