﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Application.Subjects.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.User.DisplayName).Required().MaxLength(50);
        RuleFor(command => command.User.Name).ChineseLetterNumber().MaxLength(20);
        RuleFor(command => command.User.PhoneNumber).Phone();
        RuleFor(command => command.User.Email).Email();
        RuleFor(command => command.User.IdCard).IdCard();
        RuleFor(command => command.User.CompanyName).ChineseLetterNumber().MaxLength(50);
        RuleFor(command => command.User.Position).ChineseLetterNumber().MaxLength(20);
    }
}
