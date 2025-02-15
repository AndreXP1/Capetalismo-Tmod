using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Capetalismo.Content.Items.Accessories;

namespace Capetalismo.Content.Items.Accessories{

    public class Ronca : ModItem{

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 20000;
            Item.rare = ItemRarityID.LightPurple;
        }

                public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RoncaFragment>(10);
            recipe.AddIngredient(ItemID.Obsidian ,15);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}