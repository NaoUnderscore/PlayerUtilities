using System.ComponentModel;

namespace PlayerUtilities.Features.Configs
{
    public class AfkActions
    {
        public bool SetSpectator { get; set; } = true;
        [Description("A value indicating the action message (If [set_spectator = false] this will be the kick msg).")]
        public string ActionMsg { get; set; } = "You were moved to spectator because you were AFK";
        public bool ShowCounter { get; set; } = true;

        public string CounterMsg { get; set; } = " <b><color=red>You will be moved to spectator in</color> <color=white>{seconds_left} seconds</color> <color=red>if you dont move.</color></b>";

    }
}