using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class Dragon : Monster
    {
        public Dragon(int dragonLevel)
        {
            Health = 40 + 5 * dragonLevel;
            Strength = 10 + dragonLevel;
            Armor = 5;
            Precision = 15;
            MagicPower = 0;
            Stamina = 50;
            XPValue = 25 + dragonLevel;
            Name = "monster2141";
            BattleGreetings = "I AM RHAEGAL";
        }
        public override List<StatPackage> BattleMove()
        {
            if (Stamina >= 40)
            {
                Stamina -= 15;
                return new List<StatPackage>() { new StatPackage("claw", 15 + Strength, "Dragon scrathes you badly! (" + (15 + Strength) + " claw damage)") };
            }
            else if (Stamina > 0 && Stamina < 40)
            {
                Stamina -= 15;
                return new List<StatPackage>() { new StatPackage("claw", 10 + Strength, "Dragon scrathes you! (" + (5 + Strength) + " claw damage)") };
            }
            else
            {
                Stamina += 15;
                return new List<StatPackage>() { new StatPackage("none", 0, "Rhaegal has no energy to attack!") };
            }
        }


        }
}
