using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace OrchidMod.Shaman.Weapons
{
	public class Blum : OrchidModShamanItem
    {
		public override void SafeSetDefaults()
		{
			item.damage = 17;
			item.width = 30;
			item.height = 30;
			item.useTime = 18;
			item.useAnimation = 18;
			item.knockBack = 3.25f;
			item.rare = 2;
			item.value = Item.sellPrice(0, 0, 47, 0);
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.shootSpeed = 16f;
			item.shoot = mod.ProjectileType("BlumProj");
			this.empowermentType = 2;
			this.empowermentLevel = 2;
			OrchidModGlobalItem orchidItem = item.GetGlobalItem<OrchidModGlobalItem>();
			orchidItem.shamanWeaponNoUsetimeReforge = true;
		}

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Blum");
		  Tooltip.SetDefault("Rapidly shoots dangerous magical bolts"
							+ "\nThe weapon speed depends on the number of active shamanic bonds");
		}
		
		public override void UpdateInventory(Player player) {
			int BuffsCount = player.GetModPlayer<OrchidModPlayer>().getNbShamanicBonds();
			item.useTime = 18 - (BuffsCount * 2);
			item.useAnimation = 18 - (BuffsCount * 2);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 64f; 
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				position += muzzleOffset;
			
			return true;
		}
    }
}

