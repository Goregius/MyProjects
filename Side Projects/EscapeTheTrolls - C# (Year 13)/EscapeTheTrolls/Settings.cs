using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeTheTrolls
{
    
    class Settings
    {
        public class PlayerViewingRadius
        {
            public static int Value { get; set; } = 0;
            public static string Name { get; set; } = "PlayerViewingRadius";
            public static string DisplayName { get; set; } = "Player viewing radius";
        }
        public class HorizontalViewMultiplier
        {
            public static float Value { get; set; } = 1;
            public static string Name { get; set; } = "HorizontalViewMultiplier";
            public static string DisplayName { get; set; } = "Horizontal view multiplier";
        }
        public class VerticalViewMultiplier
        {
            public static float Value { get; set; } = 1;
            public static string Name { get; set; } = "VerticalViewMultiplier";
            public static string DisplayName { get; set; } = "Vertical view multiplier";
        }
        public class TrollCount
        {
            public static int Value { get; set; } = 0;
            public static string Name { get; set; } = "TrollCount";
            public static string DisplayName { get; set; } = "Troll count";
        }
        public class Colour
        {
            public static bool Value { get; set; } = true;
            public static string Name { get; set; } = "Colour";
            public static string DisplayName { get; set; } = "Colour";
        }
        public class MaxUndo
        {
            public static int Value { get; set; } = 3;
            public static string Name { get; set; } = "MaxUndo";
            public static string DisplayName { get; set; } = "Maximum undos";
        }
        public static void ReadSettingsFromFile()
        {
            Console.ForegroundColor = ConsoleColor.White;
            List<string> settingsList = new List<string>();
            using (StreamReader reader = new StreamReader(@"Settings\Settings.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    settingsList.Add(line); // Add to list.
                }
            }

            ApplyListToSettings(settingsList);
        }
        private static void ApplyListToSettings(List<string> settingsList)
        {
            Tuple<int, bool> playerViewingRadius = SetSettingFromListInt(settingsList, PlayerViewingRadius.Name);
            Tuple<int, bool> trollCount = SetSettingFromListInt(settingsList, TrollCount.Name);
            Tuple<float, bool> horizontalViewMultiplier = SetSettingFromListfloat(settingsList, HorizontalViewMultiplier.Name);
            Tuple<float, bool> verticalViewMultiplier = SetSettingFromListfloat(settingsList, VerticalViewMultiplier.Name);
            Tuple<bool, bool> colour = SetSettingFromListBool(settingsList, Colour.Name);
            Tuple<int, bool> maxUndo = SetSettingFromListInt(settingsList, MaxUndo.Name);

            if (playerViewingRadius.Item2) PlayerViewingRadius.Value = playerViewingRadius.Item1;
            else Console.WriteLine("Error while reading " + PlayerViewingRadius.Name + " from the settings file");

            if (trollCount.Item2) TrollCount.Value  = trollCount.Item1;
            else Console.WriteLine("Error while reading " + TrollCount.Name + " from the settings file");

            if (horizontalViewMultiplier.Item2) HorizontalViewMultiplier.Value = horizontalViewMultiplier.Item1;
            else Console.WriteLine("Error while reading " + HorizontalViewMultiplier.Name + " from the settings file");

            if (verticalViewMultiplier.Item2) VerticalViewMultiplier.Value = verticalViewMultiplier.Item1;
            else Console.WriteLine("Error while reading " + VerticalViewMultiplier.Name + " from the settings file");

            if (verticalViewMultiplier.Item2) VerticalViewMultiplier.Value = verticalViewMultiplier.Item1;
            else Console.WriteLine("Error while reading " + VerticalViewMultiplier.Name + " from the settings file");

            if (colour.Item2) Colour.Value = colour.Item1;
            else Console.WriteLine("Error while reading " + Colour.Name + " from the settings file");

            if (maxUndo.Item2) MaxUndo.Value = maxUndo.Item1;
            else Console.WriteLine("Error while reading " + MaxUndo.Name + " from the settings file");

            if (!playerViewingRadius.Item2 || !trollCount.Item2 || !horizontalViewMultiplier.Item2 || !verticalViewMultiplier.Item2 || !colour.Item2 || !maxUndo.Item2)
            {
                Console.ReadLine();
            }
        }
        private static Tuple<int, bool> SetSettingFromListInt(List<string> settingsList, string settingName)
        {
            string line;
            int settingValue;

            try
            {
                line = settingsList.Where(x => x.StartsWith(settingName)).First();
            }
            catch (Exception)
            {
                return new Tuple<int, bool>(-1, false);
            }
            bool success = int.TryParse(line.Split(' ').Last(), out settingValue);

            return new Tuple<int, bool>(settingValue, success);
        }
        private static Tuple<float, bool> SetSettingFromListfloat(List<string> settingsList, string settingName)
        {
            string line;
            float settingValue;

            try
            {
                line = settingsList.Where(x => x.StartsWith(settingName)).First();
            }
            catch (Exception)
            {
                return new Tuple<float, bool>(-1, false);
            }
            
            bool success = float.TryParse(line.Split(' ').Last(), out settingValue);

            return new Tuple<float, bool>(settingValue, success);
        }
        private static Tuple<bool, bool> SetSettingFromListBool(List<string> settingsList, string settingName)
        {
            string line;
            bool settingValue;

            try
            {
                line = settingsList.Where(x => x.StartsWith(settingName)).First();
            }
            catch (Exception)
            {
                return new Tuple<bool, bool>(false, false);
            }

            bool success = bool.TryParse(line.Split(' ').Last(), out settingValue);

            return new Tuple<bool, bool>(settingValue, success);
        }
        public static void ApplySettingsToFile()
        {
            Console.ForegroundColor = ConsoleColor.White;
            List<string> settingsList = new List<string>();
            using (StreamWriter writer = new StreamWriter(@"Settings\Settings.txt"))
            {
                writer.WriteLine(PlayerViewingRadius.Name + " = " + PlayerViewingRadius.Value);
                writer.WriteLine(TrollCount.Name + " = " + TrollCount.Value);
                writer.WriteLine(HorizontalViewMultiplier.Name + " = " + HorizontalViewMultiplier.Value);
                writer.WriteLine(VerticalViewMultiplier.Name + " = " + VerticalViewMultiplier.Value);
                writer.WriteLine(Colour.Name + " = " + Colour.Value);
                writer.WriteLine(MaxUndo.Name + " = " + MaxUndo.Value);
            }
        }
        
    }
}
