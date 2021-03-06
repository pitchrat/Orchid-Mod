using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace OrchidMod.Shaman.Weapons.Hardmode
{
    public class TheCore : OrchidModShamanItem
    {
		public override void SafeSetDefaults()
		{
			item.damage = 200;
			item.noUseGraphic = false;
			item.magic = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 24;
			item.useAnimation = 24;
			item.knockBack = 4.15f;
			item.rare = 10;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.UseSound = SoundID.Item117;
			item.autoReuse = true;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType("TheCoreProj");
			this.empowermentType = 4;
			this.empowermentLevel = 5;
		}

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("The Core");
		  Tooltip.SetDefault("Shoots life-seeking essence bolts"
							+"\nThe number of projectiles depends on the number of active shamanic bonds"
							+"\n'You can feel heartbeats emanating from the staff'");
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 64f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
			position += muzzleOffset;
			}
			int BuffsCount = 0;
			if (Main.player[Main.myPlayer].GetModPlayer<OrchidModPlayer>().shamanFireBuff != 0)
				BuffsCount ++;
			
			if (Main.player[Main.myPlayer].GetModPlayer<OrchidModPlayer>().shamanWaterBuff != 0)
				BuffsCount ++;
			
			if (Main.player[Main.myPlayer].GetModPlayer<OrchidModPlayer>().shamanAirBuff != 0)
				BuffsCount ++;
			
			if (Main.player[Main.myPlayer].GetModPlayer<OrchidModPlayer>().shamanEarthBuff != 0)
				BuffsCount ++;
				
			if (Main.player[Main.myPlayer].GetModPlayer<OrchidModPlayer>().shamanSpiritBuff != 0)
				BuffsCount ++;
			
			int numberProjectiles = 2 + BuffsCount;
		
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
    }
}
