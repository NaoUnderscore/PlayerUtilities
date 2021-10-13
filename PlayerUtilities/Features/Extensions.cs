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

            var oldHealth = oldPlayer.Health;
            var oldAhp = oldPlayer.ArtificialHealth;
            var oldItems = oldPlayer.Items;
            var oldAmmo = oldPlayer.Ammo;
            var oldPos = oldPlayer.Position;
            
            Timing.CallDelayed(0.5f, () =>
            {
                newPlayer.Health = oldHealth;
                newPlayer.ArtificialHealth = oldAhp;

                newPlayer.ClearInventory();
                foreach (var item in oldItems)
                    newPlayer.AddItem(item.Type);
                foreach (var ammo in oldAmmo)
                    newPlayer.Ammo[ammo.Key] = ammo.Value;

                newPlayer.Position = oldPos;
            });
        }

        public static Player GetRandom(this IEnumerable<Player> playerList)
        {
            var list = playerList.ToList();
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : null;
        }
    }
}