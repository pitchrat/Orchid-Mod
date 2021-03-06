using Terraria;
using Terraria.ModLoader;

namespace OrchidMod.Gambler.Buffs
{
    public class ConquerorsPennantBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Conqueror's Thrill");
			Description.SetDefault("10% increased gambling critical strike chance and movement speed");
        }
        public override void Update(Player player, ref int buffIndex)
		{
			Player modPlayer = Main.player[Main.myPlayer];
			modPlayer.GetModPlayer<OrchidModPlayer>().gamblerCrit += 10;
			player.moveSpeed += 0.1f;
		}
    }
}