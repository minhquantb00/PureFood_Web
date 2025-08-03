using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Config;

public static class ConfigSetting
{
    public static void Init(IConfiguration configuration)
    {
        var keys = Enum.GetValues(typeof(ConfigSettingEnum));
        foreach (ConfigSettingEnum key in keys)
        {
            if (Configs.ContainsKey(key)) continue;
            string keyConfig = key.GetDisplayName();
            var config = configuration.GetSection(keyConfig);
            string? valueConfig = config.Value;
            Configs.Add(key, valueConfig ?? string.Empty);
        }
    }

    public static readonly IDictionary<ConfigSettingEnum, string> Configs = new Dictionary<ConfigSettingEnum, string>();
}
