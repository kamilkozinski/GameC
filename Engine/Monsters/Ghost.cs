using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class Ghost : Monster
    {
        public int dodgeIndex = 0;
        public Ghost(int ghostLevel)
        {
            Health = 50 + 5 * ghostLevel;
            Strength = 20 + ghostLevel;
            Armor = 20;
            Precision = 0;
            MagicPower = 0;
            Stamina = 50;
            XPValue = 30 + ghostLevel;
            Name = "monster0003";
            BattleGreetings = "Frig off I got work to do !!";
        }
        public override List<StatPackage> BattleMove()
        {/*
            if (dodgeIndex > 4)
            {
                if (Stamina > 0)
                {
                    Stamina -= 15;
                    return new List<StatPackage>() { new StatPackage("melee", 10 + Strength, "Ghost dodged your prievious attack. You hit for 0!! Now Ghost hits you! (" + (10 + Strength) + " melee damage)") };
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Ghost has no energy to attack anymore!") };
                }
            }
            else 
            {
                */

                if (Stamina > 0)
                {
                    Stamina -= 15;
                    return new List<StatPackage>() { new StatPackage("melee", 10 + Strength, "Ghost hits you! (" + (10 + Strength) + " melee damage)") };
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage("none", 0, "Ghost has no energy to attack anymore!") };
                }
            }
       // }
        public override void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            dodgeIndex = Index.RNG(0, 10);

            if (dodgeIndex <= 1)
                foreach (StatPackage pack in packs)
                {
                    Health -= pack.HealthDmg;
                    Strength -= pack.StrengthDmg;
                    Armor -= pack.ArmorDmg;
                    Precision -= pack.PrecisionDmg;
                    MagicPower -= pack.MagicPowerDmg;
                }
            else

            {
                // dodging attack
            }
        }
    }
}

