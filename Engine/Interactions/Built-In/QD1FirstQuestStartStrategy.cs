using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{

    class QD1FirstQuestStartStrategy : IQD1Strategy
    {

        public QD1FirstQuestStartStrategy()
        {
            Name = "QD1FirstQuestStartStrategy";
        }
        public override void Execute(GameSession parentSession, bool visited)
        { 
        parentSession.SendText("\nHello fellow traveler. You can't see me now, but I got job for you. Do you want to know what is it ?");
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
                        parentSession.SendText("\nKill 3 green dragons for me. Do you agree Y/N ?");
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
