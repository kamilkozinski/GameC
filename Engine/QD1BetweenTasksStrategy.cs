using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{
    class QD1BetweenTasksStrategy : IQD1Strategy
    {

        public QD1BetweenTasksStrategy()
        {
            Name = "QD1FirstQuestStartStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        {
            parentSession.SendText("\nGo kill monsters first!");
            
        }
    }
}