using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{

    class QD2DefaultStrategy : IQD1Strategy
    {

        public QD2DefaultStrategy()
        {
            Name = "Q2DefaultStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        {
            parentSession.SendText("\nCome back later.");
            
        }
    }
}