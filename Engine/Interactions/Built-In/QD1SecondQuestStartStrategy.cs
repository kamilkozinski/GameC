using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{
    class QD1SecondQuestStartStrategy : IQD1Strategy
    {
        public QD1SecondQuestStartStrategy()
        {
            Name = "QD1SecondQuestStartStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        {
            parentSession.SendText("\nNice job. What do you say for more quests?");
            parentSession.SendText("\nPress ENTER to continue, any E to leave");
            while (true)
            {
                string key = parentSession.GetValidKeyResponse(new List<string>() { "Return", "E", }).Item1;
                if (key == "Return")
                {
                    parentSession.SendText("\nOk then, listen carefully.");
                    key = parentSession.GetValidKeyResponse(new List<string>() { "Return", "E", }).Item1;
                    if (key == "Return")
                    {
                        parentSession.SendText("\nKill 3 ghosts for me. Do you agree Y/N ?");
                        key = parentSession.GetValidKeyResponse(new List<string>() { "E", "Y", "N" }).Item1;
                        if (key == "Y")
                        {
                            parentSession.SendText("\nThen go to work.");

                        }
                        else if (key == "N")
                        {
                            parentSession.SendText("\nCome back when you change your mind");
                            break;
                        }
                    }
                    else if (key == "E")
                    {
                        break;
                    }
                }
                else if (key == "E")
                {
                    break;
                }
            }
        }
    }
}