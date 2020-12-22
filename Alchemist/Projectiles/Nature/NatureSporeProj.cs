﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace OrchidMod.Alchemist.Projectiles.Nature
{
    public class NatureSporeProj: OrchidModAlchemistProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Spore");
        } 
		
        public override void SafeSetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = 0;
			projectile.alpha = 126;
			projectile.timeLeft = 600;
			ProjectileID.Sets.Homing[projectile.type] = true;
			this.projectileTrail = true;
        }
		
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		
		public override void AI()
        {
			if (projectile.timeLeft == 600) {
				projectile.ai[1] = Main.rand.Next(2) == 0 ? -1 : 1;
				projectile.timeLeft -= Main.rand.Next(15);
				projectile.netUpdate = true;
			}
			projectile.velocity = (projectile.velocity.RotatedByRandom(MathHelper.ToRadians(5)));
			
			if (projectile.timeLeft % 50 == 0) {
				for(int i=0; i<5; i++)
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 163);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}

			if (projectile.timeLeft <= 550) {
				
				if (projectile.timeLeft == 550) {
					projectile.velocity *= (float)((4 + Main.rand.Next(3)) / 10f);
					projectile.netUpdate = true;
				}
				
				projectile.friendly = true;
				
				Vector2 move = Vector2.Zero;
				float distance = 2000f;
				bool target = false;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].HasBuff(mod.BuffType("Attraction")))
					{
						Vector2 newMove = Main.npc[k].Center - projectile.Center;
						float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
						if (distanceTo < distance)
						{
							move = newMove;
							distance = distanceTo;
							target = true;
						}
					}
				}
				
				if (target) {
					AdjustMagnitude(ref move);
					projectile.velocity = (5 * projectile.velocity + move) / 1f;
					AdjustMagnitude(ref projectile.velocity);
					
					if (projectile.timeLeft % 50 == 0) {
						projectile.timeLeft --;
					}
					projectile.timeLeft ++;
					if (Main.rand.Next(4) == 0) {
						int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 163);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].noLight = true;
						Main.dust[dust].velocity *= 0.75f;
					}
				}

				projectile.ai[0] = target ? Main.rand.Next(10) - 5 : 5 * projectile.ai[1];
				projectile.netUpdate = true;
				Vector2 projectileVelocity = ( new Vector2(projectile.velocity.X, projectile.velocity.Y ).RotatedBy(MathHelper.ToRadians(projectile.ai[0])));
				projectile.velocity = projectileVelocity;
			} else {
				projectile.friendly = false;
			}
        }
		
		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 6f / magnitude;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y;
			projectile.ai[1] = projectile.ai[1] == -1 ? 1 : -1;
            return false;
        }
		
		public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 163);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
            }
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
        }
    }
}