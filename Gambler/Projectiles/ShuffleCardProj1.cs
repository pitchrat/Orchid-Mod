using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace OrchidMod.Gambler.Projectiles
{
	public class ShuffleCardProj1 : OrchidModGamblerProjectile
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond");
        } 
		
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		
		public override void SafeSetDefaults()
		{
			projectile.width = 18;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.aiStyle = 1;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
			this.gamblingChipChance = 10;
			this.projectileTrail = true;
		}
		
		public override void SafeAI()
		{
			if (Main.rand.Next(12) == 0) {
				int dustType = 60;
				Vector2 pos = new Vector2(projectile.position.X, projectile.position.Y);
				int index = Dust.NewDust(pos, projectile.width, projectile.height, dustType);
				Main.dust[index].velocity *= 0.25f;
				Main.dust[index].scale *= 1.5f;
				Main.dust[index].noGravity = true;
			}
			projectile.velocity.Y += this.initialized ? 0f : 0.05f;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (!this.initialized) {
				projectile.velocity *= 0f;
				this.initialized = true;
				projectile.position.Y += 5f;
			}
            return false;
        }
		
		public override void Kill(int timeLeft) {
			for (int i = 0 ; i < 3 ; i ++) {
				int dustType = 60;
				Vector2 pos = new Vector2(projectile.position.X, projectile.position.Y);
				int index = Dust.NewDust(pos, projectile.width, projectile.height, dustType);
				Main.dust[index].velocity *= 0.25f;
				Main.dust[index].scale *= 1.5f;
				Main.dust[index].noGravity = true;
			}
		}
	}
}