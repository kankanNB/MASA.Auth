﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Application.Subjects.Commands;

public class UpdateUserCommandValidator : MasaAbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(PhoneNumberValidator phoneValidator)
    {
        RuleFor(command => command.User.DisplayName).Required().MaximumLength(50);
        WhenNotEmpty(command => command.User.Name, r => r.ChineseLetterNumber().MaximumLength(20));
        WhenNotEmpty(command => command.User.PhoneNumber, r => r.SetValidator(phoneValidator));
        WhenNotEmpty(command => command.User.Email, r => r.Email());
        WhenNotEmpty(command => command.User.IdCard, r => r.IdCard());
        WhenNotEmpty(command => command.User.CompanyName, r => r.ChineseLetterNumber().MaximumLength(50));
        WhenNotEmpty(command => command.User.Position, r => r.ChineseLetterNumber().MaximumLength(20));
    }
}
