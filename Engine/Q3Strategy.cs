using Game.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    class Q3Strategy : IBattleStrategy
    {
        private int q3;

        public void Execute(GameSession parentSession, Monster monster, KilledMonstersCounter _newCounter)
        {
            if (monster.Name == "monster0001")
            {

                q3 = _newCounter.RatsCounter;
                q3++;
                _newCounter.CheckQuestStatus();
                if ((q3 - 3) == 0)
                {
                    parentSession.SendText("Quest finished");
                }
                else if ((q3 - 3) < 0)
                {
                    parentSession.SendText($"It seems you killed the right one. You have to kill {3 - q3} more rats");
                }
                else if ((q3 - 3) > 0)
                {
                    _newCounter.RatsCounter = 0;
                }
                else
                {
                    // quest inactive
                }

            }


        }
    }
}
