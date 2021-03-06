using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace OrchidMod.Shaman.Weapons.Thorium.Hardmode
{
	public class StrangePlatingScepter : OrchidModShamanItem
    {
		public override void SafeSetDefaults()
		{
			item.damage = 65;
			item.width = 50;
			item.height = 50;
			item.useTime = 18;
			item.useAnimation = 18;
			item.knockBack = 1.25f;
			item.rare = 6;
			item.value = Item.sellPrice(0, 6, 0, 0);
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shootSpeed = 20f;
			item.shoot = mod.ProjectileType("StrangePlatingScepterProj");
			this.empowermentType = 1;
			this.empowermentLevel = 3;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prime's Laser");
			Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
			if (thoriumMod == null) {
				Tooltip.SetDefault("[c/FF0000:Thorium Mod is not loaded]"
								+ "\n[c/970000:This is a cross-content weapon]");
				return;
			}
			Tooltip.SetDefault("The weapon itself can critically strike, releasing a powerful blast"
							+ "\nThe more shamanic bonds you have, the higher the chances of critical strike");
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			OrchidModPlayer modPlayer = player.GetModPlayer<OrchidModPlayer>();
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 64f; 
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				position += muzzleOffset;
			
			if (Main.rand.Next(101) < 4 + OrchidModShamanHelper.getNbShamanicBonds(player, modPlayer, mod) * 4) {
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("StrangePlatingScepterProjAlt"), damage * 2, knockBack, player.whoAmI);
			} else {
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			Mod orchidMod = ModLoader.GetMod("OrchidMod");
			Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
			if (thoriumMod != null) {
				ModRecipe recipe = new ModRecipe(thoriumMod);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.AddIngredient(null, "StrangePlating", 12);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
        }
    }
}

