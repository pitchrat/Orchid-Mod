using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OrchidMod;

namespace OrchidMod.Shaman.Projectiles.Thorium.OreOrbs.Large
{

	public class TerrariumScepterOrb : OrchidModShamanProjectile
	{
		float startX = 0;
		float startY = 0;
		int orbsNumber = 0;
		
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarium Orb");
        } 
		public override void SafeSetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.timeLeft = 12960000;
			projectile.scale = 1f;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 8;
		}
		
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		
		public override bool? CanCutTiles() {
			return false;
		}
		
        public override void AI()
        {
			Player player = Main.player[projectile.owner];
			
			if (player != Main.player[Main.myPlayer]) {
				projectile.active = false;
			}
			
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 65)
				projectile.frame = 1;
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 70)
				projectile.frame = 2;
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 75)
				projectile.frame = 3;
		   	if (player.GetModPlayer<OrchidModPlayer>().timer120 == 80)
				projectile.frame = 4;
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 85)
				projectile.frame = 5;
		    if (player.GetModPlayer<OrchidModPlayer>().timer120 == 90)
				projectile.frame = 6;
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 95)
				projectile.frame = 7;
			if (player.GetModPlayer<OrchidModPlayer>().timer120 == 100)
				projectile.frame = 0;
			
			if (player.GetModPlayer<OrchidModPlayer>().orbCountLarge < 5 || player.GetModPlayer<OrchidModPlayer>().orbCountLarge > 35 || player.GetModPlayer<OrchidModPlayer>().shamanOrbLarge != ShamanOrbLarge.TERRARIUM)
				projectile.Kill();
			else orbsNumber = player.GetModPlayer<OrchidModPlayer>().orbCountLarge;
			
			if (projectile.timeLeft == 12960000) {
				int nbOrb = player.GetModPlayer<OrchidModPlayer>().orbCountLarge;
				int offsetX = 7;
				
				if (nbOrb > 4) {
					startX = - 43 - offsetX;
					startY = - 38 - offsetX;
				}
				
				if (nbOrb > 9) {
					startX = - 30 - offsetX;
					startY = - 48 - offsetX;
				}
				
				if (nbOrb > 14) {
					startX = - 15 - offsetX;
					startY = - 53 - offsetX;
				}
				
				if (nbOrb > 19) {
					startX = - 0 - offsetX;
					startY = - 55 - offsetX;
				}
				
				if (nbOrb > 24) {
					startX = + 15 - offsetX;
					startY = - 53 - offsetX;
				}
				
				if (nbOrb > 29) {
					startX = + 30 - offsetX;
					startY = - 48 - offsetX;
				}
				
				if (nbOrb > 34) {
					startX = + 43 - offsetX;
					startY = - 38 - offsetX;
				}
				
				if (projectile.damage != 0) {
					projectile.damage = 0;
					startX = - 43 - offsetX;
					startY = - 38 - offsetX;
				}
			}
			
			projectile.velocity.X = player.velocity.X;
			projectile.position.X = player.position.X + player.width / 2 + startX;
			projectile.position.Y = player.position.Y + startY;
			
			if (Main.rand.Next(20) == 0) {
				int dustType = Main.rand.Next(6) + 59;
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity /= 2f;
				Main.dust[dust].scale *= 1.5f;
			}
        }
		
		public override void Kill(int timeLeft)
        {
            for(int i=0; i<5; i++)
            {
				int dust = Main.rand.Next(6) + 59;
                dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dust);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 10f;
            }
        }
    }
}
 