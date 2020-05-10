using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class Rat : Monster
    {
        // example monster: rat
        public Rat(int ratLevel)
        {
            Health = 30 + 5 * ratLevel;
            Strength = 10 + ratLevel;
            Armor = 10;
            Precision = 50;
            MagicPower = 0;
            Stamina = 50;
            XPValue = 20 + ratLevel;
            Name = "monster0001";
            BattleGreetings = null; // rat doesn't say anything
        }
        public override List<StatPackage> BattleMove()
        {
            if(Stamina>0)
            {
                Stamina -= 10;
                // a simple bite move dealing 5 + (rat strength statistic) damage
                return new List<StatPackage>() { new StatPackage("stab", 5 + Strength, "Rat uses Bite! ("+ (5 + Strength) +" stab damage)") };
            }
            else
            {
                return new List<StatPackage>() { new StatPackage("none", 0, "Rat has no energy to attack anymore!") };
            }
        }
    }
}
