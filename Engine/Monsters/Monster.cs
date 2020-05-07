using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game.Engine.Monsters
{
    [Serializable]
    public abstract class Monster : Subject
    {
        // abstract class representing a monster
        public int XPValue { get; protected set; }
        public string BattleGreetings { get; protected set; } 
        public abstract List<StatPackage> BattleMove(); // perform an action in the battle
        public virtual void React(List<StatPackage> packs) // receive the result of your opponent's action
        {
            foreach (StatPackage pack in packs)
            {
                Health -= pack.HealthDmg;
                Strength -= pack.StrengthDmg;
                Armor -= pack.ArmorDmg;
                Precision -= pack.PrecisionDmg;
                MagicPower -= pack.MagicPowerDmg;
            }
        }
    }
}
