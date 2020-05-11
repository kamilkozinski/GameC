using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class Whirl : Skill
    {
        public Whirl() : base("Sword Whirl", 40, 5)
        {
            PublicName = "Sword whirl [requires sword]: 0.3*Str + 0.3*Pr damage [incised] ";
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("incised");
            response1.HealthDmg = (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision);
            StatPackage response2 = new StatPackage("incised");
            response2.HealthDmg = (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision);
            StatPackage response3 = new StatPackage("incised");
            response3.HealthDmg = (int)(0.1 * player.Strength) + (int)(0.1 * player.Precision);
            // applying CustomText only once is sufficient
            response3.CustomText = "You use Sword Whirl(3 fast attacks)! (" + ((int)(0.3 * player.Strength) + (int)(0.3 * player.Precision)) + " incised damage, " ;
            return new List<StatPackage>() { response1, response2, response3 };
        }

    }
}
