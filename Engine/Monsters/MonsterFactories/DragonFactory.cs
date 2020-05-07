using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterFactories
{
    class DragonFactory : MonsterFactory
    {
        private int probabilityIndex;
        private int encounterNumber = 0; // how many times has this factory been used already?
        public DragonFactory()
        {
            probabilityIndex = Index.RNG(0, 10);
        }
        public override Monster Create(int playerLevel)
        {

            if (encounterNumber == 0) // if this is the first time, return a Dragon
            {
                encounterNumber++;
                return new Dragon(playerLevel);
            }
            else if ((encounterNumber == 1) && (probabilityIndex <= 1))
            {
                encounterNumber++;
                return new SilverDragon(playerLevel);

            }

            else return null; // no more dragons
        }
        public override System.Windows.Controls.Image Hint()
        {
            if (encounterNumber == 0) return new Dragon(0).GetImage();
            else if ((encounterNumber == 1) && (probabilityIndex <= 1)) return new SilverDragon(0).GetImage();
            else return null;
        }
        public override MonsterFactory Clone() // clone this factory so that there can be multiple instances in the map
        {
            return (DragonFactory)this.MemberwiseClone();
        }


    }
}
