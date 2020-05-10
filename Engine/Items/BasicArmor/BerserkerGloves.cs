using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Items.BasicArmor
{
    [Serializable]
    class BerserkerGloves : Item
    {
        private int berserkerBonus;
        private bool berserkerSetBonus1;
        private bool berserkerSetBonus2;
        public BerserkerGloves() : base("item2144")
        {
            PublicName = "BerserkerGloves";
            PublicTip = "when you lose X health, receive a X/8 percentage bonus to physical damage you deal in this battle";
            GoldValue = 60;
            ArMod = 12;
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
            berserkerSetBonus1 = false;
            berserkerSetBonus2 = false;
            foreach (var obj in otherItems)
            {
                if (obj == "item0007" && berserkerSetBonus1 == false)
                {
                    berserkerSetBonus1 = true;
                    if (berserkerSetBonus1 == true && obj == "item2145")
                        berserkerSetBonus2 = true;
                }
                if (obj == "item2145" && berserkerSetBonus1 == false)
                {
                    berserkerSetBonus1 = true;
                    if (berserkerSetBonus1 == true && obj == "item0007")
                        berserkerSetBonus2 = true;

                }
            }
            if (berserkerSetBonus1 && !berserkerSetBonus2)
            {
                currentPlayer.ArmorBuff += (ArMod);
                currentPlayer.StaminaBuff += (StaMod + 10);

            }
            else if (berserkerSetBonus1 && berserkerSetBonus2)
            {
                currentPlayer.ArmorBuff += ArMod;
                currentPlayer.StaminaBuff += (StaMod + 15);
            }
            else
            {
                currentPlayer.ArmorBuff += ArMod;
                berserkerSetBonus1 = false;
                berserkerSetBonus2 = false;
            }
        
        

        }



    }
}
