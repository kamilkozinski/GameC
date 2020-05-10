using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.ItemFactories
{
    class SwordFactory :ItemFactory
    {
        public Item CreateItem()
        {
            List<Item> sword = new List<Item>()
            {
                new Aerondight(),
              
            };
            return sword[Index.RNG(0, sword.Count)];
        }
        public Item CreateNonMagicItem()
        {
            
            List<Item> sword = new List<Item>()
            {
               new Aerondight(),
            };
            return sword[Index.RNG(0, sword.Count)];
        }
        public Item CreateNonWeaponItem()
        {
            // BerserkerArmor only works for physical damage dealers
            List<Item> sword = new List<Item>()
            {
                
            };
            return null;
        }
    }
}
