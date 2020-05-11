using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.BasicArmor
{
    [Serializable]
    class BerserkerArmor : Item
    {
        // special passive: receive physical damage bonus after losing health

        private int berserkerBonus;
        private int berserkerSetBonus1;
        private int berserkerSetBonus2;
        public BerserkerArmor() : base("item0007")
        {
            PublicName = "BerserkerArmor";
            PublicTip = "when you lose X health, receive a X/8 percentage bonus to physical damage you deal in this battle";
            GoldValue = 70;
            ArMod = 20;
            StaMod = 0;
        }
        public override StatPackage ModifyOffensive(StatPackage pack, List<string> otherItems)
        {
           
                pack.HealthDmg = (100 + berserkerBonus / 8) * pack.HealthDmg / 100;
            
            return pack;
        }
        public override StatPackage ModifyDefensive(StatPackage pack, List<string> otherItems)
        {
            berserkerBonus += pack.HealthDmg;
            return pack;
        }
        public override void ApplyBuffs(Player currentPlayer, List<string> otherItems)
        {
            berserkerSetBonus1 = 0;
            berserkerSetBonus2 = 0;
            foreach (var obj in otherItems)
            {
                if (obj == "item2144")
                {
                    berserkerSetBonus1++;
                }
                else if (obj == "item2145")
                {
                    berserkerSetBonus2++;
                }
            }
            if (berserkerSetBonus1 > 0 && berserkerSetBonus2 > 0)
            {
                currentPlayer.ArmorBuff += (ArMod);
                currentPlayer.StaminaBuff += (StaMod + 15);

            }
            else if ((berserkerSetBonus1 > 0 && berserkerSetBonus2 == 0) || (berserkerSetBonus2 > 0 && berserkerSetBonus1 == 0))
            {
                currentPlayer.ArmorBuff += ArMod;
                currentPlayer.StaminaBuff += (StaMod + 10);
            }
            else
            {
                currentPlayer.ArmorBuff += ArMod;

            }


        }


    }
}
