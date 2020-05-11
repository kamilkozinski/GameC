using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class DragonFactory : MonsterFactory
    {
       public int ProbabilityIndex
        {
            get;
            set;
        }
        private int encounterNumber = 0; // how many times has this factory been used already?
        public DragonFactory()
        {
            ProbabilityIndex = Index.RNG(0, 10);
        }
        public override Monster Create(int playerLevel)
        {

            if (encounterNumber == 0) // if this is the first time, return a Dragon
            {
                encounterNumber++;
                return new Dragon(playerLevel);
            }
            else if ((encounterNumber == 1) && (ProbabilityIndex <= 5))
            {
                encounterNumber++;
                return new SilverDragon(playerLevel);

            }
            else if ((encounterNumber == 2) && (ProbabilityIndex == 0))
            {
                encounterNumber++;
                return new GoldenDragon(playerLevel);

            }

            else return null; // no more dragons
        }
        public override System.Windows.Controls.Image Hint()
        {
            if (encounterNumber == 0) return new Dragon(0).GetImage();
            else if ((encounterNumber == 1) && (ProbabilityIndex <= 5)) return new SilverDragon(0).GetImage();
            else if ((encounterNumber == 2) && (ProbabilityIndex == 0)) return new GoldenDragon(0).GetImage();
            else return null;
        }
        public override MonsterFactory Clone() 
        {

            return (new DragonFactory());
        }


    }
}
