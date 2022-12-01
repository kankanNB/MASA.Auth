﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Contracts.Admin.Subjects.Validator;

public class AddStaffValidator : AbstractValidator<AddStaffDto>
{
    public AddStaffValidator()
    {
        RuleFor(staff => staff.JobNumber).Required().LetterNumber().MinLength(4).MaxLength(12);
        RuleFor(staff => staff.DisplayName)
            .NotEmpty().WithMessage("NickName is required")
            .ChineseLetterNumber().MinLength(2, "NickName").MaxLength(50, "NickName");      
        RuleFor(staff => staff.Name).ChineseLetterNumber().MinLength(2).MaxLength(50);
        RuleFor(staff => staff.PhoneNumber).Required().Phone();
        RuleFor(staff => staff.Email).Email();
        RuleFor(staff => staff.IdCard).IdCard();
        RuleFor(staff => staff.Address.Address).MinLength(8, "Address").MaxLength(100, "Address");
        RuleFor(staff => staff.CompanyName).ChineseLetter().MaxLength(50);
        RuleFor(staff => staff.Position).ChineseLetterNumber().MinLength(2).MaxLength(50);
        RuleFor(staff => staff.Password).Required().AuthPassword();
        RuleFor(staff => staff.Avatar).Required().Url();
    }
}

