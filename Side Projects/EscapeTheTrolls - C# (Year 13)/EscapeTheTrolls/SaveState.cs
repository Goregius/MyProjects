using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeTheTrolls
{
    class SaveState
    {
        public SaveState(Player player, char[,] map, List<Troll> trolls)
        {
            Player = player;
            Map = map;
            Trolls = trolls;
        }        
        public char[,] Map { get; set; }
        public Player Player { get; set; }
        public List<Troll> Trolls { get; set; } = new List<Troll>();
    }
}
