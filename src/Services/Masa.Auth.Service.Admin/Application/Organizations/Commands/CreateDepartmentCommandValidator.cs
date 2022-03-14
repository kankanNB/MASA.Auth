﻿namespace Masa.Auth.Service.Application.Organizations.Commands;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.Name).Must(name => !string.IsNullOrEmpty(name) && name.Length <= 20)
            .WithMessage("Department Name can`t null and length must be less than 20.");
        RuleFor(command => command.Description).Must(description => description.Length <= 255)
            .WithMessage("Department Description length must be less than 255.");
    }
}

