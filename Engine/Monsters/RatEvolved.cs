using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters
{
    [Serializable]
    class RatEvolved : Monster
    {
        // evolved rat - this time also with venom
        public RatEvolved(int ratLevel)
        {
            Health = 50 + 5 * ratLevel;
            Strength = 10 + ratLevel;
            Armor = 15;
            Precision = 50;
            MagicPower = 0;
            Stamina = 70;
            XPValue = 40 + ratLevel;
            Name = "monster0002";
            BattleGreetings = "You defeated me but now I'm back with venom!"; // this rat actually has something to say
        }
        public override List<StatPackage> BattleMove()
        {
            if (Stamina > 0)
            {
                Stamina -= 10;
                return new List<StatPackage>()
                { 
                    // the same bite move as in Rat, but also with 15 poison damage
                    new StatPackage("stab", 5 + Strength, "Rat uses Bite! ("+ (5 + Strength) +" stab damage)"),
                    new StatPackage("poison", 15, "Venom burns in your veins (15 poison damage)")
                };
            }
            else
            {
                return new List<StatPackage>() { new StatPackage("none", 0, "Rat has no energy to attack anymore!") };
            }
        }
    }
}
