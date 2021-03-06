using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OrchidMod.Gambler.Accessories
{
    public class GamblerTest : OrchidModGamblerEquipable
    {
        public override void SafeSetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 30, 0);
            item.rare = -11;
            item.accessory = true;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gambler Test Accessory");
			Tooltip.SetDefault("Allows you to see 3 next cards you will draw"
							+  "\n50% chance not to consume chips"
							+  "\nMaximum chips increased by 45"
							+  "\nRedraws cooldown reduced by 29 seconds"
							+  "\nMaximum redraws increased by 4"
							+  "\n[c/FF0000:Test Item]");
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			OrchidModPlayer modPlayer = player.GetModPlayer<OrchidModPlayer>();
			modPlayer.gamblerSeeCards += 3;
			modPlayer.gamblerRedrawsMax += 4;
			modPlayer.gamblerRedrawCooldownMax -= 30 * 60 - 1;
			modPlayer.gamblerChipsConsume += 50;
			modPlayer.gamblerChipsMax += 45;
        }
    }
}