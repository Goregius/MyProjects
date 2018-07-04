using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EscapeTheTrolls
{
    public class Maze
    {
        public const char WallChar = '\u2593';
        public const char TrollChar = Troll.DefaultChar;
        public const char GroundChar = ' ';
        public const char FinishChar = 'X';

        public Maze(string fileName)
        {
            AssignCustomMap(fileName);
        }
        public Maze(char[,] map)
        {
            Map = map;
        }

        private void AssignCustomMap(string fileName)
        {
            string filePath = fileName;
            List<string> lines = File.ReadAllLines(filePath).ToList();

            Map = new char[lines[0].Length,lines.Count];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Map[x, y] = lines[y][x];
                    if (Map[x, y] == '#')
                    {
                        Map[x, y] = WallChar;
                    }
                    if (Map[x, y] == ' ')
                    {
                        Map[x, y] = GroundChar;
                    }
                }
            }
        }
        //private void AssignProperties()
        //{
        //    Map = new char[45, 25];
        //    for (int i = 0; i < Width; i++)
        //    {
        //        Map[i, 0] = "#############################################"[i];
        //        Map[i, 1] = "#############################################"[i];
        //        Map[i, 2] = "##### #       #       #     #         # #####"[i];
        //        Map[i, 3] = "##### # ##### # ### ##### ### ### ### # #####"[i];
        //        Map[i, 4] = "#####       #   # #     #     # # #   # #####"[i];
        //        Map[i, 5] = "######### # ##### ##### ### # # # ##### #####"[i];
        //        Map[i, 6] = "#####   # #       #     # # # # #     # #####"[i];
        //        Map[i, 7] = "##### # ####### # # ##### ### # ##### # #####"[i];
        //        Map[i, 8] = "##### #       # # #   #     #     #   # #####"[i];
        //        Map[i, 9] = "##### ####### ##  ### # ### ##### # ### #####"[i];
        //        Map[i, 10] = "#####     #   # #   # #   #     # #     #####"[i];
        //        Map[i, 11] = "##### ### ### # ### # ##### # # # ###########"[i];
        //        Map[i, 12] = "#####   #   # # #   #   #   # # #   #   #####"[i];
        //        Map[i, 13] = "########### # # # ##### # ### # ### ### #####"[i];
        //        Map[i, 14] = "#####     # #     #   # #   # #   #     #####"[i];
        //        Map[i, 15] = "##### ### # ##### ### # ### ### ####### #####"[i];
        //        Map[i, 16] = "##### #   #     #     #   # # #       # #####"[i];
        //        Map[i, 17] = "##### # ##### # ### ##### # # ####### # #####"[i];
        //        Map[i, 18] = "##### #     # # # # #     #       # #   #####"[i];
        //        Map[i, 19] = "##### ##### # # # ### ##### ##### # #########"[i];
        //        Map[i, 20] = "##### #   # # #     #     # #   #       #####"[i];
        //        Map[i, 21] = "##### # ### ### ### ##### ### # ##### # #####"[i];
        //        Map[i, 22] = "##### #         #     #       #       # #####"[i];
        //        Map[i, 23] = "#####X#######################################"[i];
        //        Map[i, 24] = "#############################################"[i];


        //        //for (int j = 0; j <= 24; j++)
        //        //{
        //        //    Map[i, j] = 'o';
        //        //}
        //        #region HashBackup
        //        //Map[i, 0] = "#######################################"[i];
        //        //Map[i, 1] = "#######################################"[i];
        //        //Map[i, 2] = "## #       #       #     #         # ##"[i];
        //        //Map[i, 3] = "## # ##### # ### ##### ### ### ### # ##"[i];
        //        //Map[i, 4] = "##       #   # #     #     # # #   # ##"[i];
        //        //Map[i, 5] = "###### # ##### ##### ### # # # ##### ##"[i];
        //        //Map[i, 6] = "##   # #       #     # # # # #     # ##"[i];
        //        //Map[i, 7] = "## # ####### # # ##### ### # ##### # ##"[i];
        //        //Map[i, 8] = "## #       # # #   #     #     #   # ##"[i];
        //        //Map[i, 9] = "## ####### ### ### # ### ##### # ### ##"[i];
        //        //Map[i, 10] = "##     #   # #   # #   #     # #     ##"[i];
        //        //Map[i, 11] = "## ### ### # ### # ##### # # # ########"[i];
        //        //Map[i, 12] = "##   #   # # #   #   #   # # #   #   ##"[i];
        //        //Map[i, 13] = "######## # # # ##### # ### # ### ### ##"[i];
        //        //Map[i, 14] = "##     # #     #   # #   # #   #     ##"[i];
        //        //Map[i, 15] = "## ### # ##### ### # ### ### ####### ##"[i];
        //        //Map[i, 16] = "## #   #     #     #   # # #       # ##"[i];
        //        //Map[i, 17] = "## # ##### # ### ##### # # ####### # ##"[i];
        //        //Map[i, 18] = "## #     # # # # #     #       # #   ##"[i];
        //        //Map[i, 19] = "## ##### # # # ### ##### ##### # ######"[i];
        //        //Map[i, 20] = "## #   # # #     #     # #   #       ##"[i];
        //        //Map[i, 21] = "## # ### ### ### ##### ### # ##### # ##"[i];
        //        //Map[i, 22] = "## #         #     #       #       # ##"[i];
        //        //Map[i, 23] = "##X####################################"[i];
        //        //Map[i, 24] = "#######################################"[i];
        //        #endregion
        //    }
        //    for (int y = 0; y < Height; y++)
        //    {
        //        for (int x = 0; x < Width; x++)
        //        {
        //            if (Map[x, y] == '#')
        //            {
        //               Map[x, y] = WallChar;
        //            }
                    
        //        }
        //    }
        //}

        public char[,] Map;
        public char[,] MapAtTurnStart;
        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);

        public void Write(int viewingRadius, int centreX, int centerY, char[,] map)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double maxX = centreX + viewingRadius;
            double maxY = centerY + viewingRadius;
            double minX = centreX - viewingRadius;
            double minY = centerY - viewingRadius;



            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    if (Settings.Colour.Value == true)
                    {
                        AddColour(x, y, centreX, centerY, viewingRadius, map);
                        Console.Write(map[x, y]);
                    }
                    else
                    {
                        //x > maxX || x < minX || y > maxY || y < minY
                        var distance = DistanceBetweenSquared(x, y, centreX, centerY, Settings.HorizontalViewMultiplier.Value, Settings.VerticalViewMultiplier.Value);

                        //x > maxX || x < minX || y > maxY || y < minY

                        if (distance > viewingRadius * viewingRadius)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(map[x, y]);

                    }
                }
                Console.WriteLine();
            }
            sw.Stop();
            Debug.WriteLine("Maze write: " + sw.Elapsed);
            sw.Reset();
        }
        private static void AddColour(int x, int y, int centreX, int centerY, int viewingRadius, char[,] map)
        {
            //x > maxX || x < minX || y > maxY || y < minY
            var distance = DistanceBetweenSquared(x, y, centreX, centerY, Settings.HorizontalViewMultiplier.Value, Settings.VerticalViewMultiplier.Value);
            
            //x > maxX || x < minX || y > maxY || y < minY

            if (distance > viewingRadius * viewingRadius)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {

                Console.BackgroundColor = ConsoleColor.DarkGray;
                if (map[x, y] == Troll.DefaultChar)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (Player.AllCharacters.Any(character => character == map[x, y]))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                if (map[x, y] == Maze.FinishChar)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (map[x, y] == Maze.WallChar)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (map[x, y] == Player.charDeath)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (map[x, y] == Maze.GroundChar)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

            }
        }
        private static float DistanceBetweenSquared(int x1, int y1, int x2, int y2, float xSqueeze, float ySqueeze)
        {
            return (x1 - x2)*(x1 - x2) * (1 / xSqueeze) * (1 / xSqueeze) + (y1 - y2)* (y1 - y2) * (1/ySqueeze)* (1 / ySqueeze);
        }

        //public void Write(int viewingRange, int centreX, int centerY)
        //{
        //    int maxX = centreX + viewingRange;
        //    int maxY = centerY + viewingRange;
        //    int minX = centreX - viewingRange;
        //    int minY = centerY - viewingRange;


        //    for (int i = minX; i < maxX; i++)
        //    {

        //    }
        //    for (int y = 0; y < Height; y++)
        //    {
        //        for (int x = 0; x < Width; x++)
        //        {
        //            if (x > maxX || x < minX || y > maxY || y < minY)
        //            {
        //                Console.ForegroundColor = ConsoleColor.Black;
        //            }

        //            Console.Write(Map[x, y]);
        //            Console.ForegroundColor = ConsoleColor.White;
        //        }
        //        Console.WriteLine();
        //    }
        //}
        private void WriteSpaces()
        {
            for (int i = 0; i < Console.WindowWidth / 2 - Width / 2; i++)
            {
                Console.Write(" ");
            }
        }

    }
}
