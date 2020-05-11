using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class StrongAttackDecorator : SkillDecorator
    {
        // decorator for Whirl
        public StrongAttackDecorator(Skill skill) : base("Fast Sword Attack", 30, 2, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Fast Sword Attack [requires sword]: 0.05*Str + 0.4*Pr damage [stab]  AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response1 = new StatPackage("incised");
            response1.HealthDmg = (int)(0.05 * player.Strength) + (int)(0.4 * player.Precision);
            response1.CustomText = "You use Fast Sword Attack!";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response1);
            return combo;
        }


    }
}
