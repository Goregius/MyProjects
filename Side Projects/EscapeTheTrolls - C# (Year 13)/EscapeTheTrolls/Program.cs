using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EscapeTheTrolls
{
    class Program
    {
        static Random Rnd => new Random();
        static Maze MazeDefault { get; set; }
        static Player PlayerDefault { get; set; }
        static List<Troll> Trolls { get; set; }
        static LinkedList<SaveState> SaveStates { get; set; }
        static Stopwatch sw = new Stopwatch();
        static void Main()
        {
            Menu();
        }
        static void ResetProperties()
        {
            PlayerDefault = new Player(Player.charRight);
            Trolls = new List<Troll>();
            SaveStates = new LinkedList<SaveState>();
        }
        static void Menu()
        {
            StartGame();

        }
        static void StartGame()
        {
            SetMenuConsoleProperties();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            ResetProperties();
            Settings.ReadSettingsFromFile();

            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Enter map file name:");
            Console.WriteLine("--------------------");
            Assembly ass = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(ass.Location) + "\\Mazes";
            //Console.WriteLine(path);
            string[] filePaths = Directory.GetFiles(path, "*.txt",
                                         SearchOption.TopDirectoryOnly);
            //filePaths = filePaths.OrderBy(name => name.Substring(0)).ToArray();
            int fileNameIndex = 1;
            foreach (var filepath in filePaths)
            {
                string fileName = filepath.Substring(path.Length + 1);
                fileName = fileName.Substring(0, fileName.Length - 4);
                Console.WriteLine("[" + fileNameIndex + "] " + fileName);
                fileNameIndex++;
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("[0] for settings");
            Console.WriteLine("--------------------");
            string mazeInput = Console.ReadLine();

            int mazeInputInt;
            bool mazeInputSuccess = int.TryParse(mazeInput, out mazeInputInt);

            if (mazeInput == string.Empty)
            {
                Console.WriteLine("Do you want to quit? (y/n)");
                if (Console.ReadLine().StartsWith("y"))
                {
                    Environment.Exit(0);
                }
                else
                {
                    StartGame();
                }
                
            }
            if (!mazeInputSuccess || mazeInputInt > filePaths.Length || mazeInputInt < 0)
            {
                Console.WriteLine("Error! The input entered doesn't correspond to any of the filenames.");
                Console.WriteLine("Press a key to continue...");
                Console.ReadKey();
                StartGame();
            }
            if (mazeInput.StartsWith("0"))
            {
                Console.Clear();
                ShowSettingsMenu();
                StartGame();
            }
            
            //if (!mazeInput.EndsWith(".txt"))
            //{
            //    mazeInput += ".txt";
            //}

            //if (!File.Exists(path + @"\" + mazeInput))
            //{
            //    Console.WriteLine("The filename entered doesn't exist.");
            //    Console.WriteLine("Press a key to continue...");
            //    Console.ReadKey();
            //    StartGame();
            //}

            MazeDefault = new Maze(filePaths[mazeInputInt -1]);


            Console.Clear();
            PutPlayerOnMaze();
            PutTrollsOnMaze();
            Console.CursorVisible = false;
            try
            {
                Console.SetWindowSize(MazeDefault.Width, MazeDefault.Height);
                Console.SetBufferSize(Console.WindowWidth + 1, Console.WindowHeight + 1);
            }
            catch (Exception)
            {

            }
            
            
            RefreshMaze();
            while (true)
            {
                MakeMove();
            }
        }
        static void RefreshMaze()
        {
            Console.CursorVisible = false;

            try
            {
                Console.SetWindowSize(MazeDefault.Width, MazeDefault.Height);
                Console.SetBufferSize(Console.WindowWidth + 1, Console.WindowHeight + 1);
            }
            catch (Exception)
            {

            }
            

            int viewRange = Settings.PlayerViewingRadius.Value;
            MazeDefault.Write(viewRange, PlayerDefault.Location.X, PlayerDefault.Location.Y, MazeDefault.Map);
            Console.SetCursorPosition(0, 0);
        }
        static void SetMazeConsoleProperties()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(MazeDefault.Width, MazeDefault.Height);
            Console.SetBufferSize(Console.WindowWidth + 1, Console.WindowHeight + 1);
        }
        static void SetMenuConsoleProperties()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
            Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight + 1);
        }

        #region Settings
        static void ShowSettingsMenu()
        {
            SetMenuConsoleProperties();
            Console.WriteLine("Settings");
            Console.WriteLine("--------");
            Console.WriteLine("[1] " + Settings.TrollCount.DisplayName);
            Console.WriteLine("[2] " + Settings.PlayerViewingRadius.DisplayName);
            Console.WriteLine("[3] " + Settings.HorizontalViewMultiplier.DisplayName);
            Console.WriteLine("[4] " + Settings.VerticalViewMultiplier.DisplayName);
            Console.WriteLine("[5] " + Settings.Colour.DisplayName);
            Console.WriteLine("[6] " + Settings.MaxUndo.DisplayName);
            Console.WriteLine("[0] Quit settings");
            Console.WriteLine("--------");
            Console.CursorVisible = true;
            //Settings stuff
            string settingsInput = Console.ReadLine();
            switch (settingsInput.ToLower())
            {
                case "0":
                    Console.Clear();
                    StartGame();
                    break;
                case "1":
                    Console.Clear();
                    ShowTrollSettings();
                    break;
                case "2":
                    Console.Clear();
                    ShowViewingRadiusSettings();
                    break;
                case "3":
                    Console.Clear();
                    ShowHorizontalViewMultiplierSettings();
                    break;
                case "4":
                    Console.Clear();
                    ShowVerticalViewMultiplierSettings();
                    break;
                case "5":
                    Console.Clear();
                    ShowColourSettings();
                    break;
                case "6":
                    Console.Clear();
                    ShowMaxUndoSettings();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("That was not a valid option.");
                    Console.WriteLine();
                    ShowSettingsMenu();
                    break;
            }

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
        }
        static void ShowTrollSettings()
        {
            Console.WriteLine("Current " + Settings.TrollCount.DisplayName.ToLower() + " is " + Settings.TrollCount.Value);
            Console.WriteLine("Enter how many trolls there will be on screen:");
            string trollCountInput = Console.ReadLine();
            if (trollCountInput == "0" || trollCountInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            int trollCountInt = Settings.TrollCount.Value;

            try
            {
                trollCountInt = int.Parse(trollCountInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid number.");
                ShowTrollSettings();
            }

            Settings.TrollCount.Value = trollCountInt;
            Console.WriteLine(Settings.TrollCount.DisplayName + " changed to " + Settings.TrollCount.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void ShowHorizontalViewMultiplierSettings()
        {
            Console.WriteLine("Current " + Settings.HorizontalViewMultiplier.DisplayName.ToLower() + " is " + Settings.HorizontalViewMultiplier.Value);
            Console.WriteLine("Enter the " + Settings.HorizontalViewMultiplier.DisplayName.ToLower() + " value you wish to change to:");
            string horizontalViewMultiplierInput = Console.ReadLine();
            if (horizontalViewMultiplierInput == "0" || horizontalViewMultiplierInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            float horizontalViewMultiplierDouble = Settings.HorizontalViewMultiplier.Value;

            try
            {
                horizontalViewMultiplierDouble = float.Parse(horizontalViewMultiplierInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid number.");
                ShowHorizontalViewMultiplierSettings();
            }

            Settings.HorizontalViewMultiplier.Value = horizontalViewMultiplierDouble;
            Console.WriteLine(Settings.HorizontalViewMultiplier.DisplayName + " changed to " + Settings.HorizontalViewMultiplier.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void ShowVerticalViewMultiplierSettings()
        {
            Console.WriteLine("Current " + Settings.VerticalViewMultiplier.DisplayName.ToLower() + " is " + Settings.VerticalViewMultiplier.Value);
            Console.WriteLine("Enter the " + Settings.VerticalViewMultiplier.DisplayName.ToLower() + " value you wish to change to:");
            string verticalViewMultiplierInput = Console.ReadLine();
            if (verticalViewMultiplierInput == "0" || verticalViewMultiplierInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            float verticalViewMultiplierDouble = Settings.VerticalViewMultiplier.Value;

            try
            {
                verticalViewMultiplierDouble = float.Parse(verticalViewMultiplierInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid number.");
                ShowVerticalViewMultiplierSettings();
            }

            Settings.VerticalViewMultiplier.Value = verticalViewMultiplierDouble;
            Console.WriteLine(Settings.VerticalViewMultiplier.DisplayName + " changed to " + Settings.VerticalViewMultiplier.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void ShowColourSettings()
        {
            
            Console.WriteLine("Note: Setting colour to true may slow down performance.");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Current " + Settings.Colour.DisplayName.ToLower() + " is " + Settings.Colour.Value);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Enter the " + Settings.Colour.DisplayName.ToLower() + " value you wish to set to: (true/false)");
            string verticalViewMultiplierInput = Console.ReadLine();
            if (verticalViewMultiplierInput == "0" || verticalViewMultiplierInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            bool verticalViewMultiplierBool = Settings.Colour.Value;

            try
            {
                verticalViewMultiplierBool = bool.Parse(verticalViewMultiplierInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That was not a valid boolean value.");
                Console.WriteLine("--------------------------");
                ShowColourSettings();
            }

            Settings.Colour.Value = verticalViewMultiplierBool;
            Console.WriteLine(Settings.Colour.DisplayName + " changed to " + Settings.Colour.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void ShowViewingRadiusSettings()
        {
            Console.WriteLine("Current " + Settings.PlayerViewingRadius.DisplayName.ToLower() + " is " + Settings.PlayerViewingRadius.Value);
            Console.WriteLine("Enter the " + Settings.PlayerViewingRadius.DisplayName.ToLower() + " value you wish to change to:");
            Console.WriteLine("Cancel [0]");
            string viewingRadiusInput = Console.ReadLine();

            if (viewingRadiusInput == "0" || viewingRadiusInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            int viewingRadiusInt = Settings.TrollCount.Value;

            try
            {
                viewingRadiusInt = int.Parse(viewingRadiusInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid number.");
                ShowTrollSettings();
            }

            Settings.PlayerViewingRadius.Value = viewingRadiusInt;
            Console.WriteLine("Players viewing radius changed to " + Settings.PlayerViewingRadius.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void ShowMaxUndoSettings()
        {
            Console.WriteLine("Current " + Settings.MaxUndo.DisplayName.ToLower() + " is " + Settings.MaxUndo.Value);
            Console.WriteLine("Enter the " + Settings.MaxUndo.DisplayName.ToLower() + " value you wish to change to:");
            Console.WriteLine("Cancel [0]");
            string maxUndoInput = Console.ReadLine();

            if (maxUndoInput == "0" || maxUndoInput == string.Empty)
            {
                Console.Clear();
                ShowSettingsMenu();
            }
            int maxUndoInt = Settings.MaxUndo.Value;

            try
            {
                maxUndoInt = int.Parse(maxUndoInput);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid number.");
                ShowTrollSettings();
            }

            Settings.MaxUndo.Value = maxUndoInt;
            Console.WriteLine(Settings.MaxUndo.DisplayName + " changed to " + Settings.MaxUndo.Value);
            OnSettingsChanged();
            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowSettingsMenu();
        }
        static void OnSettingsChanged()
        {
            Settings.ApplySettingsToFile();
        }
        
        #endregion

        #region Starting Methods
        static Location GetEntityLocationStart(Maze maze)
        {
            //Guid.NewGuid().GetHashCode() is used as a random seed.
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            var availableLocations = GetAvailableLocations();
            Location locationStart;
            try
            {
                locationStart = availableLocations[rnd.Next(availableLocations.Count)];
                return locationStart;
            }
            catch (Exception)
            {
                Console.WriteLine("Error in loading the map.");
                Console.WriteLine("Press any key to go back to the menu screen...");
                Console.ReadKey();
                StartGame();
            }
            return null;
        }
        private static List<Location> GetAvailableLocations()
        {
            var availableLocations = new List<Location>();

            for (int x = 0; x < MazeDefault.Width; x++)
            {
                for (int y = 0; y < MazeDefault.Height; y++)
                {
                    if (MazeDefault.Map[x, y] == Maze.GroundChar)
                    {
                        availableLocations.Add(new Location(x, y));
                    }
                }
            }
            return availableLocations;
        }
        static void PutPlayerOnMaze(Direction direction = Direction.Right)
        {
            PlayerDefault.Location = GetEntityLocationStart(MazeDefault);

            MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = PlayerDefault.CurrentChar;
        }
        static void PutTrollsOnMaze()
        {
            Trolls.Clear();
            for (int i = 0; i < Settings.TrollCount.Value; i++)
            {
                Trolls.Add(new Troll(GetEntityLocationStart(MazeDefault)));
                MazeDefault.Map[Trolls[i].Location.X, Trolls[i].Location.Y] = Troll.DefaultChar;
            }
        }
        #endregion

        #region States
        static void AddState()
        {
            Player player = new Player(PlayerDefault);
            char[,] map = new char[MazeDefault.Width, MazeDefault.Height];
            for (int y = 0; y < MazeDefault.Height; y++)
            {
                for (int x = 0; x < MazeDefault.Width; x++)
                {
                    map[x, y] = MazeDefault.Map[x, y];
                }

            }
            var trolls = new List<Troll>();
            foreach (var troll in Trolls)
            {
                Troll newTroll = new Troll(troll);
                trolls.Add(newTroll);
            }

            SaveStates.AddLast(new SaveState(player, map, trolls));

            if (SaveStates.Count > Settings.MaxUndo.Value)
            {
                SaveStates.RemoveFirst();
            }
        }
        static void LoadLastState()
        {
            if (SaveStates.Count > 0)
            {
                SaveState lastSaveState = SaveStates.Last();
                SaveStates.RemoveLast();
                PlayerDefault = lastSaveState.Player;
                MazeDefault.Map = lastSaveState.Map;
                Trolls = lastSaveState.Trolls;
                RefreshMaze();
            }
        }
        #endregion

        #region Player Move
        static void MakeMove()
        {
            var input = Console.ReadKey(true);
            char[,] mazeMapCopy = new char[MazeDefault.Map.GetLength(0), MazeDefault.Map.GetLength(1)];
            for (int x = 0; x < MazeDefault.Map.GetLength(0); x++)
            {
                for (int y = 0; y < MazeDefault.Map.GetLength(1); y++)
                {
                    mazeMapCopy[x, y] = MazeDefault.Map[x, y];
                }
            }
            MazeDefault.MapAtTurnStart = mazeMapCopy;
            //MazeDefault.MapAtTurnStart[0, 0] = 'i';
            //MessageBox.Show(MazeDefault.MapAtTurnStart[0, 0] + " " + MazeDefault.Map[0, 0]);
            switch (input.Key)
            {
                case ConsoleKey.W:
                    MovePlayer(Direction.Up);
                    break;
                case ConsoleKey.A:
                    MovePlayer(Direction.Left);
                    break;
                case ConsoleKey.S:
                    MovePlayer(Direction.Down);
                    break;
                case ConsoleKey.D:
                    MovePlayer(Direction.Right);
                    break;
                case ConsoleKey.DownArrow:
                    MovePlayer(Direction.Down);
                    break;
                case ConsoleKey.UpArrow:
                    MovePlayer(Direction.Up);
                    break;
                case ConsoleKey.RightArrow:
                    MovePlayer(Direction.Right);
                    break;
                case ConsoleKey.LeftArrow:
                    MovePlayer(Direction.Left);
                    break;
                case ConsoleKey.Z:
                    if (input.Modifiers == ConsoleModifiers.Control)
                    {
                        LoadLastState();
                    }
                    break;
                case ConsoleKey.Escape:
                    Console.SetCursorPosition(0, MazeDefault.Height);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Are you sure you want to quit? [y/n]");
                    var quitInput = Console.ReadKey();
                    if (quitInput.Key == ConsoleKey.Y)
                    {
                        StartGame();
                    }
                    else
                    {
                        Console.Clear();
                        RefreshMaze();
                        MakeMove();
                    }
                    break;
            }
        }
        private static bool DetectTrollCollision(Location locationToCompare)
        {
            if (Trolls.Any(x => x.Location.X == locationToCompare.X && x.Location.Y == locationToCompare.Y))
            {
                return true;
            }
            return false;
        }
        static void MovePlayer(Direction direction)
        {
            sw.Start();
            int dx = GetDx(direction);
            int dy = GetDy(direction);

            UpdatePlayerChar(direction);
            switch (GetPlayerMoveTile(dx, dy))
            {
                case TileType.Wall:
                    MovePlayerWall(dx, dy);
                    break;
                case TileType.Ground:
                    MovePlayerGround(dx, dy);
                    break;
                case TileType.Troll:
                    MovePlayerTroll(dx, dy);
                    break;
                case TileType.Finish:
                    Win();
                    break;
            }
        }
        static void UpdatePlayerChar(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    PlayerDefault.CurrentChar = Player.charUp;
                    break;
                case Direction.Down:
                    PlayerDefault.CurrentChar = Player.charDown;
                    break;
                case Direction.Left:
                    PlayerDefault.CurrentChar = Player.charLeft;
                    break;
                case Direction.Right:
                    PlayerDefault.CurrentChar = Player.charRight;
                    break;
            }
        }
        static TileType GetPlayerMoveTile(int dx, int dy)
        {
            char tileChar = MazeDefault.Map[PlayerDefault.Location.X + dx, PlayerDefault.Location.Y + dy];

            switch (tileChar)
            {
                case Maze.GroundChar:
                    return TileType.Ground;
                case Maze.WallChar:
                    return TileType.Wall;
                case Maze.TrollChar:
                    return TileType.Troll;
                case Maze.FinishChar:
                    return TileType.Finish;
                default:
                    return TileType.Ground;
            }
        }
        static void MovePlayerGround(int dx, int dy)
        {
            AddState();
            MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = Maze.GroundChar;
            MazeDefault.Map[PlayerDefault.Location.X + dx, PlayerDefault.Location.Y + dy] = PlayerDefault.CurrentChar;
            PlayerDefault.Location = new Location(PlayerDefault.Location.X + dx, PlayerDefault.Location.Y + dy);
            PlayerMoved();
        }
        static void MovePlayerWall(int dx, int dy)
        {
            if (!CheckTooManyWalls(dx, dy))
            {
                AddState();
                MazeDefault.Map[PlayerDefault.Location.X + 2 * dx, PlayerDefault.Location.Y + 2 * dy] = Maze.WallChar;
                MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = Maze.GroundChar;
                PlayerDefault.Location = new Location(PlayerDefault.Location.X + dx, PlayerDefault.Location.Y + dy);
                MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = PlayerDefault.CurrentChar;

                PlayerMoved();
            }
        }
        static void MovePlayerTroll(int dx, int dy)
        {
            MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = Maze.GroundChar;
            PlayerDefault.Location = new Location(PlayerDefault.Location.X + dx, PlayerDefault.Location.Y + dy);
            PlayerDefault.CurrentChar = Player.charDeath;
            Lose();
        }
        static void PlayerMoved()
        {
            Debug.WriteLine("player move: " + sw.Elapsed);
            MoveTrollsStart();
            Debug.WriteLine("player move + troll move: " + sw.Elapsed);
            sw.Stop();
            sw.Reset();
            RefreshMaze();
            if (DetectTrollCollision(PlayerDefault.Location))
            {
                Lose();
            }
        }
        static bool CheckTooManyWalls(int dx, int dy)
        {
            var characterTwoBlocks = MazeDefault.Map[PlayerDefault.Location.X + 2 * dx, PlayerDefault.Location.Y + 2 * dy];
            if (characterTwoBlocks != Maze.GroundChar)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Troll Move
        static void MoveTrollsStart()
        {
            SetTrollsNextDirection();
            MoveTrolls();
        }
        static void SetTrollsNextDirection()
        {
            var existingLocations = new List<Location>();
            foreach (var troll in Trolls)
            {
                troll.AvailableDirections = GetAvailableTrollDirections(troll);
                if (troll.AvailableDirections.Count == 0) continue;

                bool locationTaken;
                Location nextLocation;
                int i = 0;
                do
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    int directionIndex = rand.Next(troll.AvailableDirections.Count);

                    directionIndex = rand.Next(troll.AvailableDirections.Count);
                    
                    troll.NextDirection = troll.AvailableDirections[directionIndex];
                    if (CheckTrollMoveTile(TrollFindDirectionToPlayer(troll), troll) == Maze.GroundChar || CheckTrollMoveTile(troll.LastSeenPlayerDirection, troll) == Maze.GroundChar)
                    {
                        if (CheckTrollMoveTile(troll.LastSeenPlayerDirection, troll) == Maze.WallChar)
                        {
                            troll.LastSeenPlayerDirection = Direction.NoDirection;
                        }
                        else
                        {
                            troll.NextDirection = troll.LastSeenPlayerDirection;
                        }
                        
                        if (TrollFindDirectionToPlayer(troll) != Direction.NoDirection)
                        {
                            troll.NextDirection = TrollFindDirectionToPlayer(troll);
                        }
                        
                    }

                    int dx = GetDx(troll.NextDirection);
                    int dy = GetDy(troll.NextDirection);
                    nextLocation = new Location(troll.Location.X + dx, troll.Location.Y + dy);
                    //If nextLocation isn't the same as any other troll next locations
                    locationTaken = existingLocations.Any(location => location.X == nextLocation.X && location.Y == nextLocation.Y);
                    i++;

                    if (i > 100)
                    {
                        nextLocation = new Location(troll.Location.X, troll.Location.Y);
                        locationTaken = false;
                    }
                } while (locationTaken);

                existingLocations.Add(nextLocation);
            }
        }
        static Direction TrollFindDirectionToPlayer(Troll troll)
        {

            int trollViewingRange = 10;
            int trollViewingWidth = 3;
            //Check up
            for (int i = 1; i < trollViewingRange; i++)
            {
                int x = troll.Location.X;
                int y = troll.Location.Y - i;
                //If there is a player at (x,y)
                if (y < 0)
                    break;
                if (MazeDefault.Map[x, y] == Maze.WallChar) break;
                if (Player.AllCharacters.Any(pChar => pChar == MazeDefault.MapAtTurnStart[x, y]))
                {
                    troll.LastSeenPlayerDirection = Direction.Up;
                    return Direction.Up;
                }
            }
            //Check down
            for (int i = 1; i < trollViewingRange; i++)
            {
                int x = troll.Location.X;
                int y = troll.Location.Y + i;
                //If there is a player at (x,y)
                if (y > MazeDefault.Map.GetLength(1) - 1)
                    break;
                if (MazeDefault.Map[x, y] == Maze.WallChar) break;
                if (Player.AllCharacters.Any(pChar => pChar == MazeDefault.MapAtTurnStart[x, y]))
                {
                    troll.LastSeenPlayerDirection = Direction.Down;
                    return Direction.Down;
                }
            }
            //Check left
            for (int i = 1; i < trollViewingRange; i++)
            {
                int x = troll.Location.X - i;
                int y = troll.Location.Y;

                if (x < 0) continue;
                if (MazeDefault.Map[x, y] == Maze.WallChar) break;
                //If there is a player at (x,y)
                if (Player.AllCharacters.Any(pChar => pChar == MazeDefault.MapAtTurnStart[x, y]))
                {
                    troll.LastSeenPlayerDirection = Direction.Left;
                    return Direction.Left;
                }
            }

            //Check right
            for (int i = 1; i < trollViewingRange; i++)
            {
                int x = troll.Location.X + i;
                int y = troll.Location.Y;

                if (x > MazeDefault.Map.GetLength(0) - 1)
                    break;
                if (MazeDefault.Map[x, y] == Maze.WallChar) break;
                //If there is a player at (x,y)
                if (Player.AllCharacters.Any(pChar => pChar == MazeDefault.MapAtTurnStart[x, y]))
                {
                    troll.LastSeenPlayerDirection = Direction.Right;
                    return Direction.Right;
                }

            }

            return Direction.NoDirection;

        }
        static List<Direction> GetAvailableTrollDirections(Troll troll)
        {
            var directions = new List<Direction>();
            if (CheckTrollCouldMeetPlayer(troll, Direction.Up))
            {
                directions.Add(Direction.Up);
                return directions;
            }
            if (CheckTrollCouldMeetPlayer(troll, Direction.Right))
            {
                directions.Add(Direction.Right);
                return directions;
            }
            if (CheckTrollCouldMeetPlayer(troll, Direction.Down))
            {
                directions.Add(Direction.Down);
                return directions;
            }
            if (CheckTrollCouldMeetPlayer(troll, Direction.Left))
            {
                directions.Add(Direction.Left);
                return directions;
            }

            if (CheckTrollMoveTile(Direction.Up, troll) == Maze.GroundChar)
            {
                directions.Add(Direction.Up);
            }
            if (CheckTrollMoveTile(Direction.Right, troll) == Maze.GroundChar)
            {
                directions.Add(Direction.Right);
            }
            if (CheckTrollMoveTile(Direction.Down, troll) == Maze.GroundChar)
            {
                directions.Add(Direction.Down);
            }
            if (CheckTrollMoveTile(Direction.Left, troll) == Maze.GroundChar)
            {
                directions.Add(Direction.Left);
            }

            return directions;
        }
        static bool CheckTrollCouldMeetPlayer(Troll troll, Direction direction)
        {
            return Player.AllCharacters.Any(x => x == CheckTrollMoveTile(direction, troll));
        }
        static char CheckTrollMoveTile(Direction direction, Troll troll)
        {
            int dx = GetDx(direction);
            int dy = GetDy(direction);

            return MazeDefault.Map[troll.Location.X + dx, troll.Location.Y + dy];
        }
        static void MoveTrolls()

        {
            foreach (var troll in Trolls)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                //1/20 chance of troll not moving
                if (rnd.Next(1, 21) == 1)
                    continue;
                int dx = GetDx(troll.NextDirection);
                int dy = GetDy(troll.NextDirection);

                char nextTrollTile = CheckTrollMoveTile(troll.NextDirection, troll);
                if (nextTrollTile == Maze.GroundChar || Player.AllCharacters.Any(playerChar => playerChar == nextTrollTile))
                {
                    MazeDefault.Map[troll.Location.X, troll.Location.Y] = Maze.GroundChar;
                    MazeDefault.Map[troll.Location.X + dx, troll.Location.Y + dy] = Troll.DefaultChar;
                    troll.Location = new Location(troll.Location.X + dx, troll.Location.Y + dy);
                }
                
            }
        }
        #endregion

        static int GetDy(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return -1;
                case Direction.Down:
                    return 1;
                case Direction.Left:
                    return 0;
                case Direction.Right:
                    return 0;
                default:
                    return 0;
            }
        }
        static int GetDx(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return 0;
                case Direction.Down:
                    return 0;
                case Direction.Left:
                    return -1;
                case Direction.Right:
                    return 1;
                default:
                    return 0;
            }
        }
        static void Win()
        {

            Console.SetCursorPosition(0, MazeDefault.Height);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("YOU WIN!");

            while (true)
            {
                while (true)
                {
                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Escape)
                    {
                        StartGame();
                    }
                }
            }
        }
        static void Lose()
        {
            MazeDefault.Map[PlayerDefault.Location.X, PlayerDefault.Location.Y] = Player.charDeath;
            RefreshMaze();
            Console.SetCursorPosition(0, MazeDefault.Height);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("YOU Lose!");

            while (true)
            {
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Escape)
                {
                    StartGame();
                }
            }
        }


    }
}
