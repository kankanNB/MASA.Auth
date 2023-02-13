﻿// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Auth.Service.Admin.Infrastructure.Sms;

public class Sms : IScopedDependency
{
    readonly IMcClient _mcClient;
    readonly IDistributedCacheClient _distributedCacheClient;
    readonly IOptions<SmsOptions> _smsOptions;
    readonly IMasaConfiguration _masaConfiguration;

    public Sms(
        IMcClient mcClient,
        IDistributedCacheClient distributedCacheClient,
        IOptions<SmsOptions> smsOptions,
        IMasaConfiguration masaConfiguration)
    {
        _mcClient = mcClient;
        _distributedCacheClient = distributedCacheClient;
        _smsOptions = smsOptions;
        _masaConfiguration = masaConfiguration;
    }

    public async Task<string> SendMsgCodeAsync(string key, string phoneNumber, TimeSpan? expiration = null)
    {
        var code = Random.Shared.Next(100000, 999999).ToString();
        await _mcClient.MessageTaskService.SendTemplateMessageByExternalAsync(new SendTemplateMessageByExternalModel
        {
            ChannelCode = _smsOptions.Value.ChannelCode,
            ChannelType = ChannelTypes.Sms,
            TemplateCode = _smsOptions.Value.TemplateCode,
            ReceiverType = SendTargets.Assign,
            Receivers = new List<ExternalReceiverModel>
            {
                new ExternalReceiverModel
                {
                    ChannelUserIdentity = phoneNumber
                }
            },
            Variables = new ExtraPropertyDictionary(new Dictionary<string, object>
            {
                ["code"] = code,
            })
        });
        var smsCaptchaExpired = _masaConfiguration.ConfigurationApi.GetDefault()
            .GetValue<int>("AppSettings:SmsCaptchaExpired", 300);
        await _distributedCacheClient.SetAsync(key, code, expiration ?? TimeSpan.FromSeconds(smsCaptchaExpired));

        return code;
    }

    public async Task<bool> VerifyMsgCodeAsync(string key, string code)
    {
        var codeCache = await _distributedCacheClient.GetAsync<string>(key);
        if (codeCache != code) return false;
        await _distributedCacheClient.RemoveAsync(key);
        return true;
    }

    public async Task<bool> CheckAlreadySendAsync(string key)
    {
        return await _distributedCacheClient.ExistsAsync<string>(key);
    }
}
