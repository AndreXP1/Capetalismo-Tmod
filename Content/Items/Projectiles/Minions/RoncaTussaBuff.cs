using System;
using CapetalismoTmod.Content.Items.Accessories;
using CapetalismoTmod.Content.Items.Projectiles;
using CapetalismoTmod.Content.Items.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CapetalismoTmod.Content.Items.Projectiles.Minions{
    public class RoncaTussaBuff : ModBuff{

			public override void SetStaticDefaults() {
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) { // This method gets called every frame your buff is active on your player.
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<RoncaTussaProjectile>());
		}
    //     public override void SetStaticDefaults()
    //     {
    //         Main.buffNoSave[Type] = true;
    //         Main.buffNoTimeDisplay[Type] = true;
    //     }

    //     public override void Update(Player player, ref int buffIndex) {
		// 	// If the minions exist reset the buff time, otherwise remove the buff from the player
		// 	if (player.ownedProjectileCounts[ModContent.ProjectileType<RoncaTussaProjectile>()] > 0) {
		// 		player.buffTime[buffIndex] = 18000;
		// 	}
		// 	else {
		// 		player.DelBuff(buffIndex);
		// 		buffIndex--;
		// 	}
		// }
	}
}

