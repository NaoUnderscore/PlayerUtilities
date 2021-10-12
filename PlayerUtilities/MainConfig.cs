using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerUtilities.Features.Configs;

namespace PlayerUtilities
{
    public class MainConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public AfkConfig AfkChecker { get; set; } = new AfkConfig();
        public DisconnectOptions DisconnectReplace { get; set; } = new DisconnectOptions();
    }
}