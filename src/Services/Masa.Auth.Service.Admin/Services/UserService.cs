﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Services
{
    public class UserService : RestServiceBase
    {
        readonly IMemoryCacheClient _memoryCacheClient;

        public UserService(IServiceCollection services, IMemoryCacheClient memoryCacheClient) : base(services, "api/user")
        {
            _memoryCacheClient = memoryCacheClient;
            MapGet(FindByAccountAsync);
            MapGet(FindByPhoneNumberAsync);
            MapGet(FindByEmailAsync);
            MapGet(FindByIdAsync);
            MapPost(ValidateByAccountAsync);
            MapPost(Visit);
            MapGet(VisitedList);
            MapPut(ResetUserPasswordAsync);
            MapPost(UserPortraitsAsync, "portraits");
        }

        private async Task<PaginationDto<UserDto>> GetListAsync(IEventBus eventBus, GetUsersDto user)
        {
            var query = new UsersQuery(user.Page, user.PageSize, user.UserId, user.Enabled, user.StartTime, user.EndTime);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        private async Task<UserDetailDto> GetDetailAsync([FromServices] IEventBus eventBus, [FromQuery] Guid id)
        {
            var query = new UserDetailQuery(id);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        private async Task<List<UserSelectDto>> GetSelectAsync([FromServices] IEventBus eventBus, [FromQuery] string search)
        {
            var query = new UserSelectQuery(search);
            await eventBus.PublishAsync(query);
            return query.Result;
        }

        private async Task<UserDto> AddExternalAsync(IEventBus eventBus, [FromBody] AddUserModel model)
        {
            var dto = new AddUserDto()
            {
                Account = model.Account,
                Name = model.Name,
                DisplayName = model.DisplayName ?? "",
                IdCard = model.IdCard ?? "",
                CompanyName = model.CompanyName ?? "",
                PhoneNumber = model.PhoneNumber ?? "",
                Email = model.Email ?? "",
                Gender = Enum.Parse<GenderTypes>(model.Gender.ToString())
            };
            dto.Enabled = true;
            dto.Password = DefaultUserAttributes.Password;
            if (dto.Gender == default) dto.Gender = GenderTypes.Male;
            if (string.IsNullOrEmpty(dto.Avatar))
            {
                if (dto.Gender == GenderTypes.Male) dto.Avatar = DefaultUserAttributes.MaleAvatar;
                else dto.Avatar = DefaultUserAttributes.FemaleAvatar;
            }
            if (string.IsNullOrEmpty(dto.DisplayName)) dto.DisplayName = dto.Name;
            var command = new AddUserCommand(dto);
            await eventBus.PublishAsync(command);
            return command.NewUser;
        }

        private async Task AddAsync(IEventBus eventBus, [FromBody] AddUserDto dto)
        {
            await eventBus.PublishAsync(new AddUserCommand(dto));
        }

        private async Task UpdateAsync(
            IEventBus eventBus,
            [FromBody] UpdateUserDto dto)
        {
            await eventBus.PublishAsync(new UpdateUserCommand(dto));
        }

        public async Task UpdateAuthorizationAsync(IEventBus eventBus,
            [FromBody] UpdateUserAuthorizationDto dto)
        {
            await eventBus.PublishAsync(new UpdateUserAuthorizationCommand(dto));
        }

        public async Task ResetUserPasswordAsync(IEventBus eventBus,
            [FromBody] ResetUserPasswordDto dto)
        {
            await eventBus.PublishAsync(new ResetUserPasswordCommand(dto));
        }

        private async Task RemoveAsync(
            IEventBus eventBus,
            [FromBody] RemoveUserDto dto)
        {
            await eventBus.PublishAsync(new RemoveUserCommand(dto));
        }

        private async Task<bool> ValidateByAccountAsync(IEventBus eventBus, [FromBody] UserAccountValidateDto accountValidateDto)
        {
            var validateCommand = new ValidateByAccountCommand(accountValidateDto.Account, accountValidateDto.Password);
            await eventBus.PublishAsync(validateCommand);
            return validateCommand.Result;
        }

        private async Task<UserModel> FindByAccountAsync(IEventBus eventBus, [FromQuery] string account)
        {
            var query = new FindUserByAccountQuery(account);
            await eventBus.PublishAsync(query);
            return ConvertToModel(query.Result);
        }

        private async Task<UserModel> FindByEmailAsync(IEventBus eventBus, [FromQuery] string email)
        {
            var query = new FindUserByEmailQuery(email);
            await eventBus.PublishAsync(query);
            return ConvertToModel(query.Result);
        }

        private async Task<UserModel> FindByPhoneNumberAsync(IEventBus eventBus, [FromQuery] string phoneNumber)
        {
            var query = new FindUserByPhoneNumberQuery(phoneNumber);
            await eventBus.PublishAsync(query);
            return ConvertToModel(query.Result);
        }

        private async Task<UserModel> FindByIdAsync(IEventBus eventBus, [FromQuery] Guid id)
        {
            var query = new UserDetailQuery(id);
            await eventBus.PublishAsync(query);
            return ConvertToModel(query.Result);
        }

        private UserModel ConvertToModel(UserDetailDto user)
        {
            return new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Account = user.Account,
                DisplayName = user.DisplayName,
                IdCard = user.IdCard,
                CompanyName = user.CompanyName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Department = user.Department,
                Gender = user.Gender,
                Avatar = user.Avatar,
                Position = user.Position,
                Address = new AddressValueModel
                {
                    Address = user.Address.Address
                },
                Roles = user.RoleIds.Select(r => new RoleModel
                {
                    Id = r,
                    Name = _memoryCacheClient.Get<CacheRole>($"{CacheKey.ROLE_CACHE_KEY_PRE}{r}")?.Name ?? "",
                }).ToList(),
            };
        }

        private async Task Visit(IEventBus eventBus, [FromBody] AddUserVisitedDto addUserVisitedDto)
        {
            var visitCommand = new UserVisitedCommand(addUserVisitedDto.UserId, addUserVisitedDto.Url);
            await eventBus.PublishAsync(visitCommand);
        }

        private async Task<List<UserVisitedDto>> VisitedList(IEventBus eventBus, [FromQuery] Guid userId)
        {
            var visitListQuery = new UserVisitedListQuery(userId);
            await eventBus.PublishAsync(visitListQuery);
            return visitListQuery.Result;
        }

        public async Task UpdatePasswordAsync(IEventBus eventBus,
            [FromBody] UpdateUserPasswordModel user)
        {
            await eventBus.PublishAsync(new UpdateUserPasswordCommand(user));
        }

        public async Task UpdateBasicInfoAsync(IEventBus eventBus,
            [FromBody] UpdateUserBasicInfoModel user)
        {
            await eventBus.PublishAsync(new UpdateUserBasicInfoCommand(user));
        }

        public async Task<List<UserPortraitModel>> UserPortraitsAsync(IEventBus eventBus,
            [FromBody] List<Guid> userIds)
        {
            var query = new UserPortraitsQuery(userIds);
            await eventBus.PublishAsync(query);
            return query.Result;
        }
    }
}
