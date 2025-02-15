using CapetalismoTmod.Content.Items.Accessories;
using CapetalismoTmod.Content.Items.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CapetalismoTmod.Content.Items.Projectiles.Minions{
public class RoncaTussaItem : ModItem{
                public override void SetStaticDefaults() {
                ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
                ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
                }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.damage = 50;
            Item.knockBack = 10;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold:50);
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item8;
            Item.accessory = true;
            Item.noMelee = true;
            Item.buffType = ModContent.BuffType<RoncaTussaBuff>();
            Item.shoot = ModContent.ProjectileType<RoncaTussaProjectile>();
        }

        public override bool? UseItem(Player player)
        {
            if(player.whoAmI == Main.myPlayer){
              player.AddBuff(Item.buffTime, 3600);
            }
            return true;
            }
            public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
                // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
                position = Main.MouseWorld;
        }

            public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
                // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
                player.AddBuff(Item.buffType, 2);

                // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
                projectile.originalDamage = Item.damage;

                // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
                return false;
        }
        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Ronca>())
        .AddIngredient(ModContent.ItemType<Tussa>())
				.AddTile(TileID.Hellforge)
				.Register();
		}
    }
  }
