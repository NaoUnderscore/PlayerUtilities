using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using PlayerUtilities.Features;
using PlayerUtilities.Features.Components;
using UnityEngine;

namespace PlayerUtilities
{
    public class EventHandler
    {
        private Dictionary<Player, AfkManager> _afkManagers = new Dictionary<Player, AfkManager>();

        public void OnLeft(LeftEventArgs ev)
        {
            if (!_afkManagers.ContainsKey(ev.Player))
            {
                Object.Destroy(_afkManagers[ev.Player]);
                _afkManagers.Remove(ev.Player);
            }

            if (MainClass.Cfg.DisconnectReplace.ReplaceOptions.ShouldReplace && Round.IsStarted && !RoundSummary.singleton.RoundEnded && (ev.Player.Role != RoleType.Tutorial || !MainClass.Cfg.DisconnectReplace.IgnoreTutorials))
            {
                var newPly = Player.Get(Team.RIP).GetRandom();
                ev.Player.ReplaceWith(newPly);
                
                if (!string.IsNullOrWhiteSpace(MainClass.Cfg.DisconnectReplace.ReplaceOptions.ReplaceMessage))
                {
                    if (MainClass.Cfg.DisconnectReplace.ReplaceOptions.BroadcastMsg)
                        newPly.Broadcast(MainClass.Cfg.DisconnectReplace.ReplaceOptions.MessageDuration, MainClass.Cfg.DisconnectReplace.ReplaceOptions.ReplaceMessage);
                    else
                        newPly.ShowHint(MainClass.Cfg.DisconnectReplace.ReplaceOptions.ReplaceMessage, MainClass.Cfg.DisconnectReplace.ReplaceOptions.MessageDuration);
                }
            }
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            if (!MainClass.Cfg.AfkChecker.IsEnabled || MainClass.Cfg.AfkChecker.GroupsIgnored.Contains(ev.Player.GroupName)) return;
            
            var manager = ev.Player.GameObject.AddComponent<AfkManager>();
            _afkManagers.Add(ev.Player, manager);
        }
    }
}