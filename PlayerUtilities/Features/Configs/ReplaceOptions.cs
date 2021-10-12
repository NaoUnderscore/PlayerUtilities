using System.ComponentModel;

namespace PlayerUtilities.Features.Configs
{
    public class ReplaceOptions
    {
        public bool ShouldReplace { get; set; } = true;
        [Description("A value containing the message showed to players replacing another player (empty to disable).")]
        public string ReplaceMessage { get; set; } = "<i>You have replaced an AFK player!</i>";
        [Description("A value indicating wheter or not the message above is a broadcast (no = hint).")]
        public bool BroadcastMsg { get; set; } = true;
        [Description("A value indicating the duration of the replace message.")]
        public ushort MessageDuration { get; set; } = 7;
    }
}