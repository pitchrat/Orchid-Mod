using Terraria;
using Terraria.ModLoader;

namespace OrchidMod.Alchemist.Buffs
{
    public class ReactiveVialsBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Reactive Elements");
			Description.SetDefault("10% increased chemical damage for your next chemical mixture");
            Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
        }
		
        public override void Update(Player player, ref int buffIndex)
		{
			OrchidModPlayer modPlayer = player.GetModPlayer<OrchidModPlayer>();
			modPlayer.alchemistDamage += 2.1f;
		}
    }
}