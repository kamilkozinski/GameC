using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.CharacterClasses;

namespace Game.Engine.Items.BasicArmor
{
    [Serializable]
    class Key : Item
    {
        private int berserkerBonus;
        private int berserkerSetBonus1;
        private int berserkerSetBonus2;

        public Key() : base("item2137")
        {
            PublicName = "Key";
            GoldValue = 50;
        }
       



    }
}