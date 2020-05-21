using Game.Engine.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine
{
    interface IBattleStrategy
    {
        void Execute(GameSession ses, Monster monster, KilledMonstersCounter _newCounter);
    }
}
