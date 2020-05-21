using Game.Engine.Interactions.Built_In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    class QuestDoorMediator : IMediator
    {
        private KilledMonstersCounter component1;
        private QuestDoor1 component2;
      

       public QuestDoorMediator(KilledMonstersCounter comp1, QuestDoor1 comp2)
        {
            component1 = comp1;
            component2 = comp2;
            component1.SetMediator(this);
            component2.SetMediator(this);
           
        }
       
        public void Notify(object sender, string eventName)
        {

            if (eventName == "Q1S") 
            {
                component1.BattleStrategy = 1;     
            }

            if (eventName == "Q1P") //what happens if we pass quest 1
            {
                component1.BattleStrategy = 2;
                component1.GhostCounter = 0;
                component2.Strategy = new QD1SecondQuestStartStrategy();
                
            }
            if (eventName == "Q2P") //what happens if we pass quest 2
            {
                component1.BattleStrategy = 3;
                component1.RatsCounter = 0;
                component2.Strategy = new QD1ThirdQuestStartStrategy();
            }
            if (eventName == "Q3P") //what happens if we pass quest 3
            {
               
            }
         




        }
    }
}
