using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace CapetalismoTmod.Content.Items.Weapons
{
    public class ThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.value = 0;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<ThrowingKnifeProjectile>();
            Item.shootSpeed = 10f;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.damage = 50;

        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
