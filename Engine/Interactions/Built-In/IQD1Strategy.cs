using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Interactions.Built_In
{

    abstract class IQD1Strategy
    {
       public abstract void Execute(GameSession ses, bool visited);
        public string Name
        {
            get;
            set;
        }
        
    }
}
