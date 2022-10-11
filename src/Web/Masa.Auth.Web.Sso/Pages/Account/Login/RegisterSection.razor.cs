﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

using Identity = Masa.Auth.Security.OAuth.Providers.Identity;

namespace Masa.Auth.Web.Sso.Pages.Account.Login;

public partial class RegisterSection
{
    [Inject]
    public IAuthClient AuthClient { get; set; } = null!;

    [Inject]
    public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

    [CascadingParameter]
    public string ReturnUrl { get; set; } = string.Empty;

    [Parameter]
    [MemberNotNullWhen(true, "Identity")]
    public bool UserBind { get; set; }

    Identity? Identity { get; set; }

    RegisterInputModel _inputModel = new();
    MForm _registerForm = null!;
    bool _registerLoading;
    Dictionary<RegisterFieldTypes, ComponentMetadata> _registerComponents = new();

    public bool CanRegister => _inputModel.Agreement && ValidateRegisterFields();

    protected override async Task OnInitializedAsync()
    {
        if (UserBind)
        {
            var httpContext = HttpContextAccessor.HttpContext!;
            Identity = await httpContext.GetExternalIdentityAsync();
            _inputModel.DisplayName = Identity.NickName ?? "";
            _inputModel.Account = Identity.Account;
            _inputModel.PhoneNumber = Identity.PhoneNumber ?? "";
            _inputModel.Email = Identity.Email ?? "";
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (UserBind) ReturnUrl = Identity.Properties["returnUrl"] ?? "~/";
            if (ReturnUrl == null || !ReturnUrl.Contains('?'))
            {
                return;
            }

            var splitIndex = ReturnUrl.IndexOf('?');
            var paramString = ReturnUrl[splitIndex..];
            var queryValues = HttpUtility.ParseQueryString(paramString);
            var clientId = queryValues["client_id"];
            if (string.IsNullOrEmpty(clientId))
            {
                return;
            }

            var customLoginModel = await AuthClient.CustomLoginService.GetCustomLoginByClientIdAsync(clientId);
            if (customLoginModel == null || !customLoginModel.RegisterFields.Any())
            {
                return;
            }

            var registerFields = customLoginModel.RegisterFields.OrderBy(r => r.Sort).ToList();

            foreach (var registerField in registerFields)
            {
                var componentParameters = new Dictionary<string, object>() {
                                { "Required",registerField.Required },
                                { "Value",_inputModel }
                            };
                switch (registerField.RegisterFieldType)
                {
                    case RegisterFieldTypes.Email:
                        _registerComponents[RegisterFieldTypes.Email] = new ComponentMetadata
                        {
                            ComponentType = typeof(Email),
                            ComponentParameters = componentParameters
                        };
                        break;
                    case RegisterFieldTypes.PhoneNumber:
                        _registerComponents[RegisterFieldTypes.PhoneNumber] = new ComponentMetadata
                        {
                            ComponentType = typeof(PhoneNumber),
                            ComponentParameters = componentParameters
                        };
                        break;
                    case RegisterFieldTypes.Password:
                        _registerComponents[RegisterFieldTypes.Password] = new ComponentMetadata
                        {
                            ComponentType = typeof(Password),
                            ComponentParameters = componentParameters
                        };
                        break;
                    case RegisterFieldTypes.DisplayName:
                        _registerComponents[RegisterFieldTypes.DisplayName] = new ComponentMetadata
                        {
                            ComponentType = typeof(DisplayName),
                            ComponentParameters = componentParameters
                        };
                        break;
                    default:
                        break;
                }
            }

            _inputModel.EmailRegister = _registerComponents.ContainsKey(RegisterFieldTypes.Email);
            StateHasChanged();
        }
    }

    private bool ValidateRegisterFields()
    {
        //todo 
        return true;
    }

    private async Task RegisterHandler()
    {
        if (!_registerForm.Validate())
        {
            return;
        }

        _registerLoading = true;
        if (UserBind)
        {
            var model = new RegisterThirdPartyUserModel
            {
                ThirdPartyIdpType = Enum.Parse<ThirdPartyIdpTypes>(Identity.Issuer),
                ExtendedData = JsonSerializer.Serialize(Identity),
                ThridPartyIdentity = Identity.Subject,
                UserRegisterType = UserRegisterTypes.PhoneNumber,
                PhoneNumber = _inputModel.PhoneNumber,
                Email = _inputModel.Email,
                SmsCode = _inputModel.SmsCode.ToString(),
                Account = _inputModel.Account,
                Password = _inputModel.Password,
                DisplayName = _inputModel.DisplayName,
                Avatar = Identity.Picture
            };
            await AuthClient.UserService.RegisterThirdPartyUserAsync(model);
            Navigation.NavigateTo(AuthenticationExternalConstants.CallbackEndpoint, true);
            _registerLoading = false;
            await PopupService.ToastSuccessAsync("Register success");
            return;
        }

        if (_inputModel.EmailRegister)
        {
            if (_inputModel.Email is null) throw new UserFriendlyException("Emai is required");

            await AuthClient.UserService.RegisterByEmailAsync(new RegisterByEmailModel
            {
                PhoneNumber = _inputModel.PhoneNumber,
                SmsCode = _inputModel.SmsCode.ToString(),
                Account = string.IsNullOrEmpty(_inputModel.Account) ? _inputModel.Email : _inputModel.Account,
                Email = _inputModel.Email,
                EmailCode = _inputModel.EmailCode.ToString() ?? throw new UserFriendlyException("Emai code is required"),
                Password = _inputModel.Password,
                DisplayName = string.IsNullOrEmpty(_inputModel.DisplayName) ? GenerateDisplayName(_inputModel) : _inputModel.DisplayName
            });
        }
        else
        {
            await AuthClient.UserService.RegisterByPhoneAsync(new RegisterByPhoneModel
            {
                PhoneNumber = _inputModel.PhoneNumber,
                SmsCode = _inputModel.SmsCode.ToString(),
                Account = string.IsNullOrEmpty(_inputModel.Account) ? _inputModel.PhoneNumber : _inputModel.Account,
                Avatar = "",
                DisplayName = string.IsNullOrEmpty(_inputModel.DisplayName) ? GenerateDisplayName(_inputModel) : _inputModel.DisplayName
            });
        }

        var loginInputModel = new LoginInputModel
        {
            PhoneLogin = true,
            SmsCode = _inputModel.SmsCode,
            Password = _inputModel.Password,
            UserName = _inputModel.Email ?? "",
            Environment = ScopedState.Environment,
            PhoneNumber = _inputModel.PhoneNumber,
            ReturnUrl = ReturnUrl,
            RegisterLogin = true
        };
        var msg = await _js.InvokeAsync<string>("login", loginInputModel);
        _registerLoading = false;
        if (!string.IsNullOrEmpty(msg))
        {
            await PopupService.AlertAsync(msg, AlertTypes.Error);
        }
        else
        {
            if (SsoUrlHelper.IsLocalUrl(ReturnUrl))
            {
                Navigation.NavigateTo(ReturnUrl, true);
            }
            else if (string.IsNullOrEmpty(ReturnUrl))
            {
                Navigation.NavigateTo("/", true);
            }
            else
            {
                await PopupService.AlertAsync("invalid return url", AlertTypes.Error);
            }
        }

        string GenerateDisplayName(RegisterInputModel _inputModel)
        {
            var _prefix = T("User");
            string? _suffix;
            if (_inputModel.EmailRegister)
            {
                _suffix = _inputModel.Email[.._inputModel.Email.IndexOf('@')];
            }
            else
            {
                _suffix = _inputModel.PhoneNumber[7..];
            }
            return _prefix + _suffix;
        }
    }
}
