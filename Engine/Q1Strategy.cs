using Game.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    class Q1Strategy : IBattleStrategy
    {
        private int q1;
        
        public void Execute(GameSession parentSession, Monster monster, KilledMonstersCounter _newCounter)
        {
            if (monster.Name == "monster2141")
            {

                q1 = _newCounter.GreenDragonCounter;
                q1++;
                _newCounter.CheckQuestStatus();
                if ((q1 - 3) == 0)
                {
                    parentSession.SendText("Quest finished");
                }
                else if ((q1 - 3) < 0)
                {
                    parentSession.SendText($"It seems you killed the right one. You have to kill {3 - q1} more green dragons");
                }
                else if ((q1 - 3) > 0)
                {
                    _newCounter.GreenDragonCounter = 0;
                }
                else
                {
                }
            }
        
            
        }
    }
}
