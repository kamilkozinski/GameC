using Game.Engine.Items.BasicArmor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items.ItemFactories
{
    class KeyFactory : ItemFactory
    {
        public Item CreateItem()
        {
            Key key = new Key();
            return key;
        }
        public Item CreateNonMagicItem()
        {
            Key key = new Key();
            return key;
        }
        public Item CreateNonWeaponItem()
        {
            Key key = new Key();
            return key;
        }
    }
}
