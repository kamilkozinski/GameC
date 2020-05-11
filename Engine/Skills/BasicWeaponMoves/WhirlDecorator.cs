using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.BasicWeaponMoves
{
    [Serializable]
    class WhirlDecorator : SkillDecorator
    {
        // decorator for Whirl
        public WhirlDecorator(Skill skill) : base("Sword Whirl", 40, 5, skill)
        {
            MinimumLevel = Math.Max(1, skill.MinimumLevel) + 1;
            PublicName = "COMBO - Sword whirl [requires sword]: 3*( 0.1*Str + 0.1*Pr damage) [incised] AND " + decoratedSkill.PublicName.Replace("COMBO: ", "");
            RequiredItem = "Sword";
        }
        public override List<StatPackage> BattleMove(Player player)
        {
            StatPackage response = new StatPackage("incised");
            response.HealthDmg = ((int)(0.3 * player.Strength) + (int)(0.3 * player.Precision));
            response.CustomText = "You use Sword Whirl";
            List<StatPackage> combo = decoratedSkill.BattleMove(player);
            combo.Add(response);
            return combo;
        }
    }
}
