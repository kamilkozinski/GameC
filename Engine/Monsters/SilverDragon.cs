using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class SilverDragon : Monster
    {
        public SilverDragon(int dragonLevel)
        {
            Health = 40 + 5 * dragonLevel;
            Strength = 20 + dragonLevel;
            Armor = 5;
            Precision = 15;
            MagicPower = 25;
            Stamina = 50;
            XPValue = 45 + dragonLevel;
            Name = "monster2142";
            BattleGreetings = "I AM DROGON";

        }
        public override List<StatPackage> BattleMove()
        {
            if (MagicPower >= 25)
            {
                MagicPower -= 25;
                return new List<StatPackage>() { new StatPackage("fire", 40, "Silver dragon attacks you with fireball! (" + (40) + " fire damage)") };
            }

            if (Stamina >= 40 )
            {
                Stamina -= 15;
                MagicPower += 10;
                return new List<StatPackage>() { new StatPackage("claw", 15 + Strength, "Silver dragon scrathes you badly! (" + (15 + Strength) + " claw damage)") };
            }
            else if (Stamina > 0 && Stamina < 40 )
            {
                Stamina -= 15;
                MagicPower += 10;
                return new List<StatPackage>() { new StatPackage("claw", 10 + Strength, "Silver dragon scrathes you! (" + (5 + Strength) + " claw damage)") };
            }
            else
            {
                Stamina += 15;
                return new List<StatPackage>() { new StatPackage("none", 0, "Drogon has no energy to attack!") };
            }
        }





    }
}
