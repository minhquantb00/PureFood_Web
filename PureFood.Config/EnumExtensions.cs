using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PureFood.Config;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum? enumValue)
    {
        try
        {
            if (enumValue == null)
            {
                return string.Empty;
            }

            var configName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            return string.IsNullOrEmpty(configName) ? enumValue.ToString() : configName;
        }
        catch (Exception e)
        {
            return enumValue!.ToString();
        }
    }

    public static int GetOrder(this Enum enumValue)
    {
        var orderConfig = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetOrder().GetValueOrDefault();
        return orderConfig.GetValueOrDefault(0);
    }

    public static string GetConfig(this ConfigSettingEnum enumValue)
    {
        if (ConfigSetting.Configs.TryGetValue(enumValue, out var configValue))
            return configValue;
        return string.Empty;
    }


    public static string SetCDNKeyInContent(this string content)
    {
        string cdnDomain = ConfigSettingEnum.CdnDomain.GetConfig();
        string cdnDomainKey = ConfigSettingEnum.CdnDomainKey.GetConfig();
        if (string.IsNullOrEmpty(content))
        {
            return string.Empty;
        }

        if (string.IsNullOrEmpty(cdnDomain))
        {
            return string.Empty;
        }


        content = content.Replace(cdnDomain, cdnDomainKey);
        return content;
    }

    public static string SetCDNKeyEmbedInContent(this string content)
    {
        string cdnDomainEmbed = ConfigSettingEnum.CdnDomainEmbed.GetConfig();
        string cdnDomainKeyEmbed = ConfigSettingEnum.CdnDomainKeyEmbed.GetConfig();
        if (string.IsNullOrEmpty(content))
        {
            return content;
        }

        if (string.IsNullOrEmpty(cdnDomainEmbed))
        {
            return content;
        }

        if (string.IsNullOrEmpty(cdnDomainKeyEmbed))
        {
            return content;
        }

        content = content.Replace(cdnDomainEmbed, cdnDomainKeyEmbed);
        return content;
    }

    public static string SetCDNKey(this string url)
    {
        string cdnDomain = ConfigSettingEnum.CdnDomain.GetConfig();
        string cdnDomainKey = ConfigSettingEnum.CdnDomainKey.GetConfig();
        if (string.IsNullOrEmpty(url))
        {
            return string.Empty;
        }

        if (!url.StartsWith(cdnDomain))
        {
            return url;
        }

        if (url.StartsWith(cdnDomainKey))
        {
            return url;
        }

        url = url.Remove(0, cdnDomain.Length);
        return url.StartsWith("/") ? $"{cdnDomainKey}{url}" : $"{cdnDomainKey}/{url}";
    }
}