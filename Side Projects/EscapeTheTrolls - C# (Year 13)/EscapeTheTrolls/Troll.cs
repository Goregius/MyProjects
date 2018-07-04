using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeTheTrolls
{
    class Troll
    {
        public Troll(Location location)
        {
            Location = location;
        }

        public Troll(Troll troll)
        {
            Location = troll.Location;
        }
        public const char DefaultChar = 'T';
        
        public Location Location { get; set; }
        public List<Direction> AvailableDirections { get; set; }
        public Direction NextDirection { get; set; }
        public Direction LastSeenPlayerDirection { get; set; } = Direction.NoDirection;
        
    }
}
