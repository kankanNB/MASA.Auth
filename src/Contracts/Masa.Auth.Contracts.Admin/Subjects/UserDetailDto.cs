﻿namespace Masa.Auth.Contracts.Admin.Subjects;

public class UserDetailDto : UserDto
{
    public AddressValueDto Address { get; set; }

    public string Department { get; set; }

    public string Position { get; set; }

    public string Password { get; set; }

    public List<string> ThirdPartyIdpAvatars { get; set; }

    public string Creator { get; set; }

    public string Modifier { get; set; }

    public DateTime? ModificationTime { get; set; }

    public UserDetailDto() : base()
    {
        Address = new();
        ThirdPartyIdpAvatars = new();
        Creator = "";
        Modifier = "";
        Department = "";
        Position = "";
        Password = "";
    }

    public UserDetailDto(Guid id, string name, string displayName, string avatar, string idCard, string account, string companyName, bool enabled, string phoneNumber, string email, DateTime creationTime, AddressValueDto address, List<string> thirdPartyIdpAvatars, string creator, string modifier, DateTime? modificationTime, string department, string position, string password, GenderTypes genderType) : base(id, name, displayName, avatar, idCard, account, companyName, enabled, phoneNumber, email, creationTime, genderType)
    {
        Address = address;
        Department = department;
        Position = position;
        Password = password;
        ThirdPartyIdpAvatars = thirdPartyIdpAvatars;
        Creator = creator;
        Modifier = modifier;
        ModificationTime = modificationTime;
    }
}

