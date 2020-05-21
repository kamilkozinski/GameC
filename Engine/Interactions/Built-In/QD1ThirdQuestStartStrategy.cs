using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{
    class QD1ThirdQuestStartStrategy : IQD1Strategy
    {

        public QD1ThirdQuestStartStrategy()
        {
            Name = "QD1ThirdQuestStartStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        {
            parentSession.SendText("\nNice, last quest and you get juicy reward. What do you say ?");
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
                        parentSession.SendText("\nKill 3 rats  for me. Do you agree Y/N ?");
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