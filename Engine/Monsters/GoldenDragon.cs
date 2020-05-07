using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class GoldenDragon : Monster
    {
        public GoldenDragon(int dragonLevel)
        {
            Health = 50 + 5 * dragonLevel;
            Strength = 10 + dragonLevel;
            Armor = 15;
            Precision = 15;
            MagicPower = 35;
            Stamina = 60;
            XPValue = 75 + dragonLevel;
            Name = "monster2143";
            BattleGreetings = "I AM VISERION";

        }
        public override List<StatPackage> BattleMove()
        {
            if (MagicPower >= 25)
            {
                MagicPower -= 25;
                return new List<StatPackage>() { new StatPackage("fire", 60, "Silver dragon attacks you with fireball! (" + (60) + " fire damage)") };
            }

            if (Stamina >= 40)
            {
                Stamina -= 15;
                MagicPower += 10;
                return new List<StatPackage>() { new StatPackage("claw", 25 + Strength, "Silver dragon scrathes you badly! (" + (25 + Strength) + " claw damage)") };
            }
            else if (Stamina > 0 && Stamina < 40)
            {
                Stamina -= 15;
                MagicPower += 10;
                return new List<StatPackage>() { new StatPackage("claw", 15 + Strength, "Silver dragon scrathes you! (" + (15 + Strength) + " claw damage)") };
            }
            else
            {
                Stamina += 15;
                return new List<StatPackage>() { new StatPackage("none", 0, "Viserion has no energy to attack!") };
            }
        }
    }
}