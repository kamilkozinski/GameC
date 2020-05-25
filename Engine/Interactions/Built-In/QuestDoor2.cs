using Game.Engine.Interactions.Built_In;
using Game.Engine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game.Engine.Interactions.Built_In
{
    [Serializable]
    class QuestDoor2 : ListBoxInteraction
    {
        protected IMediator mediator;

        public void SetMediator(IMediator med)
        {
            mediator = med;
        }

        private bool visited = false; // has this place been visited?
        public IQD1Strategy Strategy { get; set; } // store strategy 
        public QuestDoor2(GameSession ses) : base(ses)
        {
            Name = "interaction0006";
            Enterable = true;
            Strategy = new QD2DefaultStrategy();
        }
        private static QuestDoor2 instance;

        public static QuestDoor2 GetInstance(GameSession ses) 
        {
            if (instance == null)
            {
                instance = new QuestDoor2(ses);
            }
            return instance;
        }


        protected override void RunContent()
        {

             Strategy.Execute(parentSession, visited);
            if (Strategy.Name == "QD2FourthQuestStartStrategy")
            {
                mediator.Notify(this, "Q4P");
            }
    
            visited = true;

        }


    }
}
