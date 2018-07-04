using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeTheTrolls
{
    public class Player
    {
        public Player(char currentChar)
        {
            CurrentChar = currentChar;
        }

        public Player(Player player)
        {
            Location = player.Location;
            CurrentChar = player.CurrentChar;
        }

        public const char charUp = '^';
        public const char charRight = '>';
        public const char charDown = 'V';
        public const char charLeft = '<';
        public const char charDeath = 'D';
        public static List<char> AllCharacters { get; } = new List<char>() { charUp, charRight, charDown, charLeft };
        public Location Location { get; set; }
        public char CurrentChar { get; set; }
        
    }

    
}
