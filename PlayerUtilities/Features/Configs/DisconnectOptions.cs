using System.ComponentModel;

namespace PlayerUtilities.Features.Configs
{
    public class DisconnectOptions
    {
        [Description("A value indicating whether or not the plugin should ignore tutorials.")]
        public bool IgnoreTutorials { get; set; } = false;
        [Description("The options to configure if the player should be replaced with spectators.")]
        public ReplaceOptions ReplaceOptions { get; set; } = new ReplaceOptions();
    }
}