using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace PlayerUtilities.Features
{
    public static class Extensions
    {
        public static void ReplaceWith(this Player oldPlayer, Player newPlayer)
        {
            newPlayer.SetRole(oldPlayer.Role);
            
            if (oldPlayer.Role == RoleType.Scp079)
            {
                newPlayer.Abilities = oldPlayer.Abilities;
                newPlayer.Camera = oldPlayer.Camera;
                newPlayer.Experience = oldPlayer.Experience;
                newPlayer.Level = oldPlayer.Level;
                newPlayer.Energy = oldPlayer.Energy;
                return;
            }

            Timing.CallDelayed(0.5f, () =>
            {
                newPlayer.Health = oldPlayer.Health;
                newPlayer.ArtificialHealth = oldPlayer.ArtificialHealth;

                newPlayer.ClearInventory();
                foreach (var item in oldPlayer.Items)
                    newPlayer.AddItem(item.Type);
                foreach (var ammo in oldPlayer.Ammo)
                    newPlayer.Ammo[ammo.Key] = ammo.Value;

                newPlayer.Position = oldPlayer.Position;
            });
        }

        public static Player GetRandom(this IEnumerable<Player> playerList)
        {
            var list = playerList.ToList();
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : null;
        }
    }
}