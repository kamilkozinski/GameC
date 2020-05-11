using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class FastAttack : Skill
    {
        public FastAttack() : base("Fast Sword Attack", 10, 2)
        {
            PublicName = "Fast Sword Attack [requires sword]: 0.05*Str + 0.4*Pr damage [stab] ";
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("stab");
            response1.HealthDmg = (int)(0.05 * player.Strength) + (int)(0.4 * player.Precision);
        
            // applying CustomText only once is sufficient
            response1.CustomText = "You use Fast Sword Attack! (" + ((int)(0.05 * player.Strength) + (int)(0.4 * player.Precision)) + " stab damage ";
            return new List<StatPackage>() { response1};
        }



    }
}
