using Game.Engine.Items.BasicArmor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{
    class QD2FourthQuestStartStrategy : IQD1Strategy
    {
        public QD2FourthQuestStartStrategy() : base()
        {
            Name = "QD2FourthQuestStartStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        {
            parentSession.SendText("\nNow we can talk. You passed all quests in brown doors. Now, I got something special for y... WAIT, Oh My God what is IT ?? KILL IT PLEASE...");
            int choice = GetListBoxChoice(new List<string>() { "Ok, let me help you *fight monster*", "*Watch him die*", "*Run away*" },parentSession);
            switch (choice)
            {
                case 0:
                    parentSession.FightRandomMonster();
                    parentSession.SendText("Thank you, you save my life");
                    parentSession.UpdateStat(7, 50); //add some exp
                    parentSession.UpdateStat(8, 100); // and gold
                    Key key = new Key();
                    parentSession.AddThisItem(key);
                    break;
                case 1:
                    parentSession.SendText("Person from doors died. ");

                    break;
                case 2:  
                    break;
            }


        }
    }
}
