using Game.Engine.Interactions.Built_In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.InteractionFactories
{
    class QuestDoorFactory : InteractionFactory 
    {
        public List<Interaction> CreateInteractionsGroup(GameSession parentSession)
        {
            KilledMonstersCounter counter = KilledMonstersCounter.GetInstance();
            QuestDoor1 door1 = new QuestDoor1(parentSession);
            new QuestDoorMediator(counter, door1);

            return new List<Interaction>() { door1 };
        }


    }
}
