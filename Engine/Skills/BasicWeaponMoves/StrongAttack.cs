using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class StrongAttack : Skill
    {
        public StrongAttack() : base("Strong Sword Attack", 30, 2)
        {
            PublicName = "Strong Sword Attack [requires sword]: 0.6*Str + 0.05*Pr damage [stab] ";
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("stab");
            response1.HealthDmg = (int)(0.05 * player.Strength) + (int)(0.6 * player.Precision);

            // applying CustomText only once is sufficient
            response1.CustomText = "You use Strong Sword Attack! (" + ((int)(0.05 * player.Strength) + (int)(0.6 * player.Precision)) + " stab damage ";
            return new List<StatPackage>() { response1 };
        }
    }
}