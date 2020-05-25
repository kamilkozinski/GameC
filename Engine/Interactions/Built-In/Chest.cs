using Game.Engine.Interactions.Built_In;
using Game.Engine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Game.Engine.Interactions.Built_In
{
    [Serializable]
    class Chest : ListBoxInteraction
    {
        private int visited;
        private static Chest instance;

        public static Chest GetInstance(GameSession ses)
        {
            if (instance == null)
            {
                instance = new Chest(ses);
            }
            return instance;
        }

      
        public Chest(GameSession ses) : base(ses)
        {
            Name = "interaction0007";
            Enterable = false;
            visited = 0;
            parentSession = ses;
        }



        protected override void RunContent()
        {
            if (visited == 0)
            {
                int choice = GetListBoxChoice(new List<string>() { "*Try to open chest*", "*Leave*" });
                switch (choice)
                {
                    case 0:
                        bool doYouHaveKey = parentSession.TestForItem("item2137");
                        if (doYouHaveKey)
                        {
                            parentSession.UpdateStat(8, 1000); // and gold
                            parentSession.SendText("\nYou found 1000 GOLD.");
                            visited = 1;
                        }
                        else 
                        {
                            parentSession.SendText("\nYou don't have key.Come back later");
                        }
                        
                        break;
                    case 1:
                        // you leave
                        break;
                   
                }
               
            }
            else if (visited == 1)
            {
             // nothing happens, chest stays inactive
            }

        }


    }
}