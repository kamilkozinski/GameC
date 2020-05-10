using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Items
{
    [Serializable]
    class Aerondight : Sword 
    {
        public Aerondight() : base("item2146")
        {
            StrMod = 5;
            PrMod = 5;
            GoldValue = 100;
            PublicName = "Aerondight";
        }
        public override StatPackage ModifyOffensive(StatPackage pack, List<string> otherItems)
        {
            pack.ArmorDmg = 5;
            return pack;
        }
    }
}
