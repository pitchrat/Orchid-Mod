using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace OrchidMod.Shaman.Weapons.Thorium.Hardmode
{
	public class LichScepter : OrchidModShamanItem
    {
		public override void SafeSetDefaults()
		{
			item.damage = 75;
			item.width = 50;
			item.height = 50;
			item.useTime = 42;
			item.useAnimation = 42;
			item.knockBack = 2.75f;
			item.rare = 6;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shootSpeed = 1f;
			item.shoot = mod.ProjectileType("LichScepterProj");
			this.empowermentType = 3;
			this.empowermentLevel = 4;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reliquary Candle");
			Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
			if (thoriumMod == null) {
				Tooltip.SetDefault("[c/FF0000:Thorium Mod is not loaded]"
								+ "\n[c/970000:This is a cross-content weapon]");
				return;
			}
			Tooltip.SetDefault("Fires out a bolt of spiritual fire, dividing upon hitting a foe"
							+ "\nIf you have 4 or more active shamanic bonds, the bonus projectiles will home at nearby enemies");
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

