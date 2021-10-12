using System.Collections.Generic;
using System.ComponentModel;

namespace PlayerUtilities.Features.Configs
{
    public class AfkConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("A value indicating the minimum duration in seconds to consider a player AFK.")]
        public int AfkTime { get; set; } = 70;
        [Description("A value indicating whether or not the plugin should ignore tutorials.")]
        public bool IgnoreTutorials { get; set; } = false;
        [Description("The options to configure if the player should be replaced with spectators.")]
        public ReplaceOptions ReplaceOptions { get; set; } = new ReplaceOptions();
        [Description("The options to configure what should be done to AFK players.")]
        public AfkActions Actions { get; set; } = new AfkActions();
        [Description("A list of RA groups that should be ignored by the plugin.")] 
        public List<string> GroupsIgnored { get; set; } = new List<string> { "owner", "admin" };
    }
}