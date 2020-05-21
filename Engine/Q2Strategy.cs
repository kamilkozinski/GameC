using Game.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    class Q2Strategy : IBattleStrategy
    {
        private int q2;

        public void Execute(GameSession parentSession, Monster monster, KilledMonstersCounter _newCounter)
        {
            if (monster.Name == "monster2140")
            {

                q2 = _newCounter.GhostCounter;
                q2++;
                _newCounter.CheckQuestStatus();
                if ((q2 - 3) == 0)
                {
                    parentSession.SendText("Quest finished");
                }
                else if ((q2 - 3) < 0)
                {
                    parentSession.SendText($"It seems you killed the right one. You have to kill {3 - q2} more ghosts");
                }
                else if ((q2 - 3) > 0)
                {
                    _newCounter.GhostCounter = 0;
                }
                else
                {
                }

            }


        }
    }
}
