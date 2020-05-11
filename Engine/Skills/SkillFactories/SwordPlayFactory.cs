using Game.Engine.CharacterClasses;
using System;
using System.Collections.Generic;
using Game.Engine.Skills.BasicWeaponMoves;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Skills.SkillFactories
{
    [Serializable]
    class SwordPlayFactory : SkillFactory
    {
       
        public Skill CreateSkill(Player player)
        {
            List<Skill> playerSkills = player.ListOfSkills;
            Skill known = CheckContent(playerSkills); 
            if (known == null) 
            {
                Whirl s1 = new Whirl();
                FastAttack s2 = new FastAttack();
                StrongAttack s3 = new StrongAttack();

                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); 
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (s3.MinimumLevel <= player.Level) tmp.Add(s3);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)]; // use Index.RNG for safe random numbers
            }
            else if (known.decoratedSkill == null) // a BasicSpell has been already learned, use decorator to create a combo
            {
                WhirlDecorator s1 = new WhirlDecorator(known);
                FastAttackDecorator s2 = new FastAttackDecorator(known);
                StrongAttack s3 = new StrongAttack();

                List<Skill> tmp = new List<Skill>();
                if (s1.MinimumLevel <= player.Level) tmp.Add(s1); // check level requirements
                if (s2.MinimumLevel <= player.Level) tmp.Add(s2);
                if (s3.MinimumLevel <= player.Level) tmp.Add(s3);
                if (tmp.Count == 0) return null;
                return tmp[Index.RNG(0, tmp.Count)]; // use Index.RNG for safe random numbers
               
            }
            else return null; 
        }
        private Skill CheckContent(List<Skill> skills) // wrapper method for checking
        {
            foreach (Skill skill in skills)
            {
                if (skill is Whirl || skill is WhirlDecorator || skill is FastAttack || skill is FastAttackDecorator || skill is StrongAttack || skill is StrongAttackDecorator) return skill;
            }
            return null;
        }


    }
}
