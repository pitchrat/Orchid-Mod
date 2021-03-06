using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OrchidMod.Shaman.Armors.OreHelms
{
	[AutoloadEquip(EquipType.Head)]
    public class AdamantiteSpangenhelm : OrchidModShamanEquipable
    {

        public override void SafeSetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.defense = 11;
        }

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Adamantite Spangenhelm");
		  Tooltip.SetDefault("11% increased shamanic damage and critical chance");
		}

        public override void UpdateEquip(Player player)
        {
			OrchidModPlayer modPlayer = player.GetModPlayer<OrchidModPlayer>();
            modPlayer.shamanCrit += 11;
			modPlayer.shamanDamage += 0.11f;
        }
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == 403 && legs.type == 404;
        }
		
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Your shamanic bonds will last 5 seconds longer";
			OrchidModPlayer modPlayer = player.GetModPlayer<OrchidModPlayer>();
			modPlayer.shamanBuffTimer += 5;
        }
		
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = drawAltHair = false;
        }
		
		public override bool DrawHead()
        {
            return true;
        }
		
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AdamantiteBar, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
