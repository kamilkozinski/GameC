using Game.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    class KilledMonstersCounter
    {
        protected IMediator mediator;

        public void SetMediator(IMediator med)
        {
            mediator = med;
        }

        public int GreenDragonCounter
        {
            get;
            set;
        }
        public int GhostCounter
        {
            get;
            set;
        }
        public int RatsCounter
        {
            get;
            set;
        }
        public int BattleStrategy
        {
            get;
            set;
        }
        private KilledMonstersCounter()
        {
            GreenDragonCounter = 0;
            GhostCounter = 0;
            RatsCounter = 0;
            BattleStrategy = 0;
        }
        private static KilledMonstersCounter instance;

        public static KilledMonstersCounter GetInstance()
        {
            if (instance == null)
            {
                instance = new KilledMonstersCounter();
            }
            return instance;
        }
        public void CheckQuestStatus()
        {
            if (GreenDragonCounter == 2)
            {
                mediator.Notify(this, "Q1P");      
            }
            if (GhostCounter == 2)
            {
                mediator.Notify(this, "Q2P");    
            }
            if (RatsCounter == 2)
            {
                mediator.Notify(this, "Q3P");
               
            }
        }

     


    }
}
