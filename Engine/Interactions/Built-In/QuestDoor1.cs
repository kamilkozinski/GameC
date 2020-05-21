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
    class QuestDoor1 : ImageInteraction
    {
        protected IMediator mediator;

        public void SetMediator(IMediator med)
        {
            mediator = med;
        }

        private bool visited = false; // has this place been visited?
        public IQD1Strategy Strategy { get; set; } // store strategy 
        public QuestDoor1(GameSession ses) : base(ses)
        {
            Name = "interaction0005";
            displayedImageName = "interaction0005display";
            Enterable = true;
            Strategy = new QD1FirstQuestStartStrategy();
        }



        protected override void RunContent()
        {
            QD1BetweenTasksStrategy strat1 = new QD1BetweenTasksStrategy();
            if (Strategy.Name == "QD1FirstQuestStartStrategy")
            {
                mediator.Notify(this, "Q1S");
            }

            Strategy.Execute(parentSession, visited);
            Strategy = strat1; // change strategy between tasks
            visited = true;

        }


    }
}
