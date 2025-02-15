using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Build.Evaluation;
using Terraria.GameContent.Creative;


namespace Capetalismo.Content.Items.Accessories{

    public class TussaFragment: ModItem{

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}
