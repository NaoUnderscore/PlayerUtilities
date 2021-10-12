using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
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
            
            newPlayer.Position = oldPlayer.Position;
            newPlayer.Health = oldPlayer.Health;
            newPlayer.ArtificialHealth = oldPlayer.ArtificialHealth;

            newPlayer.ClearInventory();
            foreach (var item in oldPlayer.Items)
                newPlayer.AddItem(item.Type);
            foreach (var ammo in oldPlayer.Ammo)
                newPlayer.Ammo[ammo.Key] = ammo.Value;
        }

        public static Player GetRandom(this IEnumerable<Player> playerList)
        {
            var list = playerList.ToList();
            return list[Random.Range(0, list.Count)];
        }
    }
}