using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Full_1
{
    public enum ComponentType
    {
        Resistor = 0,
        Voltmeter = 1,
        Ammeter = 2,
        Cell = 3,
        WireStraight = 4,
        WireTwoAlternate = 5,
        WireThreeWay = 6,
        WireFourWay = 7
    }

    public enum Mode
    {
        Select,
        Resistor,
        Voltmeter,
        Ammeter,
        Cell,
        WireStraight,
        WireTwoAlternate,
        WireThreeWay,
        WireFourWay
    }

    public partial class CircuitForm : Form
    {
        private Mode CircuitMode { get; set; }
        private static List<BaseComponent> Components { get; set; }
        private bool Grid { get; set; }

        #region Utility

        //Gets the index of the Component in Components that matches the ID of the parameter PictureBox  p.
        public int GetComponentIndex(PictureBox p)
        {
            //Loops through every component in Components, if a component matches with the p, the index of that component is returned.
            for (int i = 0; i < Components.Count; i++)
                if (Components[i].ID.ToString() == p.Tag.ToString())
                    return i;

            throw new Exception("Component not found.");
        }

        public double RoundDownToNearest(double passedNumber, double roundTo)
        {
            return Math.Floor(passedNumber/roundTo)*roundTo;
        }

        public int RoundDownToNearest(int passedNumber, int roundTo)
        {
            double num = passedNumber;
            double roundToFinal = roundTo;
            return Convert.ToInt32(Math.Floor(num/roundToFinal)*roundToFinal);
        }

        public bool CheckOutsideBounds(PictureBox p)
        {
            //returns true if the PictureBox p is outside the bounds of the panelCircuitBoard.
            return p.Location.X < 0 ||
                   p.Location.X >= panelCircuitBoard.Width ||
                   p.Location.Y < 0 ||
                   p.Location.Y >= panelCircuitBoard.Height;
        }

        public bool CheckPictureBoxCollision(PictureBox pictureBoxToCompare, PictureBox pictureBoxToCompareAgainst, int gap)
        {
            //Returns false if the PictureBoxes don't collide.
            if (pictureBoxToCompare.Location.X < pictureBoxToCompareAgainst.Location.X ||
                pictureBoxToCompare.Location.X > pictureBoxToCompareAgainst.Location.X + gap - 1 ||
                pictureBoxToCompare.Location.Y < pictureBoxToCompareAgainst.Location.Y ||
                pictureBoxToCompare.Location.Y > pictureBoxToCompareAgainst.Location.Y + gap - 1) return false;

            //returns true if the PictureBoxes aren't the same, false if they are since that if they're the same, 
            //they would always collide with itself and so wouldn't be an error
            return pictureBoxToCompare.Tag != pictureBoxToCompareAgainst.Tag;
        }

        private Image GetRotatedImage(int componentIndex)
        {
            var direction = Components[componentIndex].Direction;

            //Rotates the direction 90 degrees clockwise, this also changes the Components[componentIndex].CurrentImage because of its set method.
            switch (direction)
            {
                case ComponentDirection.Right:
                    Components[componentIndex].Direction = ComponentDirection.Down;
                    break;
                case ComponentDirection.Left:
                    Components[componentIndex].Direction = ComponentDirection.Up;
                    break;
                case ComponentDirection.Up:
                    Components[componentIndex].Direction = ComponentDirection.Right;
                    break;
                case ComponentDirection.Down:
                    Components[componentIndex].Direction = ComponentDirection.Left;
                    break;
            }
            return Components[componentIndex].CurrentImage;
        }

        #endregion

        #region CircuitForm Methods

        public CircuitForm()
        {
            InitializeComponent();

            ValueChanger v = new ValueChanger(1,2,3,4);
            v.Show();
            KeyPreview = true;
            CircuitMode = Mode.Select;
            Components = new List<BaseComponent>();
            Grid = true;
        }

        //For testing.
        private void CircuitForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void CircuitForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Changes the CircuitMode given specific keys
            switch (e.KeyChar)
            {
                case 's':
                    CircuitMode = Mode.Select;
                    break;
                case 'r':
                    CircuitMode = Mode.Resistor;
                    break;
                case 'v':
                    CircuitMode = Mode.Voltmeter;
                    break;
                case 'a':
                    CircuitMode = Mode.Ammeter;
                    break;
                case 'c':
                    CircuitMode = Mode.Cell;
                    break;
                case '1':
                    CircuitMode = Mode.WireStraight;
                    break;
                case '2':
                    CircuitMode = Mode.WireTwoAlternate;
                    break;
                case '3':
                    CircuitMode = Mode.WireThreeWay;
                    break;
                case '4':
                    CircuitMode = Mode.WireFourWay;
                    break;
            }
        }

        private void CircuitForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    TranslateComponents(ArrowDirection.Up);
                    break;
                case Keys.Right:
                    TranslateComponents(ArrowDirection.Right);
                    break;
                case Keys.Down:
                    TranslateComponents(ArrowDirection.Down);
                    break;
                case Keys.Left:
                    TranslateComponents(ArrowDirection.Left);
                    break;
            }
        }

        private void CircuitForm_Resize(object sender, EventArgs e)
        {
            //When resizing, it ensures the panel is the maximum si
            var widthPanel = Size.Width - Size.Width%50 - 50;
            var heightPanel = Size.Height - Size.Height%50 - 100;

            panelCircuitBoard.Size = new Size(widthPanel, heightPanel);
            panelCircuitBoard.Refresh();
        }

        #endregion

        #region Circuit Panel Methods

        private void panelCircuitBoard_Paint(object sender, PaintEventArgs e)
        {
            if (!Grid)
                return;
            var gap = 50;

            //Draws grid with spacings of {gap} pixels.
            using (var g = e.Graphics)
            {
                using (var p = new Pen(Brushes.Gray))
                {
                    for (var i = 0; i <= panelCircuitBoard.Width/50; i++)
                    {
                        g.DrawLine(p, gap*i, 0, gap*i, panelCircuitBoard.Size.Width);
                        g.DrawLine(p, 0, gap*i, panelCircuitBoard.Size.Width, gap*i);
                    }
                    for (var i = 0; i <= panelCircuitBoard.Height/50; i++)
                    {
                        g.DrawLine(p, gap*i, 0, gap*i, panelCircuitBoard.Size.Height);
                        g.DrawLine(p, 0, gap*i, panelCircuitBoard.Size.Height, gap*i);
                    }

                    g.DrawLine(p, panelCircuitBoard.Size.Width - 1, 0, panelCircuitBoard.Size.Width - 1,
                        panelCircuitBoard.Size.Height);
                    g.DrawLine(p, 0, panelCircuitBoard.Size.Height - 1, panelCircuitBoard.Size.Width,
                        panelCircuitBoard.Size.Height - 1);
                }
            }
        }

        private void panelCircuitBoard_MouseClick(object sender, MouseEventArgs e)
        {
            var direction = ComponentDirection.Right;

            //Doesn't allow right clicks
            if (e.Button == MouseButtons.Right || ModifierKeys == Keys.Shift) return;

            //If it's a ctrl + click, make the component vertical
            if (ModifierKeys == Keys.Control)
            {
                direction = ComponentDirection.Up;
            }

            switch (CircuitMode)
            {
                case Mode.Select:
                    break;
                case Mode.Resistor:
                    AddComponent(ComponentType.Resistor, e.X, e.Y, direction);
                    break;
                case Mode.Voltmeter:
                    AddComponent(ComponentType.Voltmeter, e.X, e.Y, direction);
                    break;
                case Mode.Ammeter:
                    AddComponent(ComponentType.Ammeter, e.X, e.Y, direction);
                    break;
                case Mode.Cell:
                    AddComponent(ComponentType.Cell, e.X, e.Y, direction);
                    break;
                case Mode.WireStraight:
                    AddComponent(ComponentType.WireStraight, e.X, e.Y, direction);
                    break;
                case Mode.WireTwoAlternate:
                    AddComponent(ComponentType.WireTwoAlternate, e.X, e.Y, direction);
                    break;
                case Mode.WireThreeWay:
                    AddComponent(ComponentType.WireThreeWay, e.X, e.Y, direction);
                    break;
                case Mode.WireFourWay:
                    AddComponent(ComponentType.WireFourWay, e.X, e.Y, direction);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Component Methods

        private void AddComponent(ComponentType componentType, int posX, int posY,
            ComponentDirection direction = ComponentDirection.Right, double resistance = 0, double voltage = 0,
            double current = 0)
        {
            //Ensures that the PictureBox that is to be created will be on a square.
            posX = RoundDownToNearest(posX, 50);
            posY = RoundDownToNearest(posY, 50);

            BaseComponent component;

            try
            {
                //Sets the component as a specific component, depending on the ComponentType the method receives.
                switch (componentType)
                {
                    case ComponentType.Resistor:
                        component = new Resistor(direction, resistance, current, voltage);
                        break;
                    case ComponentType.Voltmeter:
                        component = new Voltmeter(direction, resistance, current, voltage);
                        break;
                    case ComponentType.Ammeter:
                        component = new Ammeter(direction, resistance, current, voltage);
                        break;
                    case ComponentType.Cell:
                        component = new Cell(direction, resistance, current, voltage);
                        break;
                    case ComponentType.WireStraight:
                        component = new Wire(direction, resistance, current, voltage);
                        break;
                    case ComponentType.WireTwoAlternate:
                        component = new WireTwoWayAlt(direction, resistance, current, voltage);
                        break;
                    case ComponentType.WireThreeWay:
                        component = new WireThreeWay(direction, resistance, current, voltage);
                        break;
                    case ComponentType.WireFourWay:
                        component = new WireFourWay(direction, resistance, current, voltage);
                        break;
                    default:
                        throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //Sets the location of component.
            component.Location = Location;

            var pictureBox = new PictureBox
            {
                Tag = component.ID,
                //Sets the location of the PictureBox.
                Location = new Point(posX, posY),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            //Events.
            pictureBox.MouseMove += p_MouseMove;
            pictureBox.MouseClick += P_MouseClick;
            pictureBox.MouseWheel += P_MouseWheel;
            pictureBox.MouseHover += P_MouseHover;
            pictureBox.Image = component.CurrentImage;

            Components.Add(component);
            //Adds the component to the main panel.
            panelCircuitBoard.Controls.Add(pictureBox);
        }

        private void AddComponent(BaseComponent component, Point location)
        {
            try
            {
                //Sets the component as a specific component, depending on the ComponentType of component.
                switch (component.ComponentType)
                {
                    case ComponentType.Resistor:
                        component = new Resistor(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.Voltmeter:
                        component = new Voltmeter(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.Ammeter:
                        component = new Ammeter(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.Cell:
                        component = new Cell(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.WireStraight:
                        component = new Wire(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.WireTwoAlternate:
                        component = new WireTwoWayAlt(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.WireThreeWay:
                        component = new WireThreeWay(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    case ComponentType.WireFourWay:
                        component = new WireFourWay(component.Direction, component.Resistance, component.Voltage,
                            component.Current);
                        break;
                    default:
                        throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            component.Location = location;

            var pictureBox = new PictureBox
            {
                Location = location,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Tag = component.ID
            };


            //Events.
            pictureBox.MouseMove += p_MouseMove;
            pictureBox.MouseClick += P_MouseClick;
            pictureBox.MouseWheel += P_MouseWheel;
            pictureBox.MouseHover += P_MouseHover;
            pictureBox.Image = component.CurrentImage;

            
            Components.Add(component);
            //Adds the component to the main panel
            panelCircuitBoard.Controls.Add(pictureBox);
        }

        private void RotateComponent(object sender)
        {
            
            try
            {
                var pictureBox = ((PictureBox)sender);
                var i = GetComponentIndex(pictureBox);
                //Sets the new rotated image of the PictureBox.
                //GetRotatedImage also changes the direction of the component that is paired with pictureBoxSender.
                pictureBox.Image = GetRotatedImage(i);
                pictureBox.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveComponents()
        {
            //Converts (Serializes) resistorPosition to a string
            var componentsString = JsonConvert.SerializeObject(Components);
            //Outputs componentsString to a file.
            SaveJsonString(componentsString);
        }

        private void SaveJsonString(string jsonString)
        {
            // Initializes a new instance of SaveFileDialog. Sets the filter to json only.
            var savefile = new SaveFileDialog {Filter = @"Json file (*.json)|*.json"};

            //If when the saveFile closes, save isn't clicked, the method stops.
            if (savefile.ShowDialog() != DialogResult.OK) return;

            //Writes the json file to the file name given from the Save File Dialog
            using (var sw = new StreamWriter(savefile.FileName))
                sw.WriteLine(jsonString);
        }

        private void LoadComponents()
        {
            // Sets the filter to json only.
            var openFileDialog = new OpenFileDialog {Filter = @"Json file (*.json)|*.json"};

            //If when the openFileDialog closes, open isn't clicked, the method stops.
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                //Assigns text to the file given from the openFileDialog.
                var text = File.ReadAllText(openFileDialog.SafeFileName);

                //I would have used a single line for assigning baseComponents, however when testing this, all the enum values would set itself to "0".
                //I found this out when JsonConvert.DeserializeObject was given an argument in its genereric argument section.
                //So JsonConvert.DeserializeObject<List<BaseComponent>>(text) wouldn't work properly.
                //But I found if the text was directly deserialized to an object, the enum would remain intact.
                //So I converted text to the object first, obtained the enum values by a string search through GetEnumNumbers.
                //Then put these values into the object, which was converted to a list of BaseComponent first.
                var deserializedObject = (JArray) JsonConvert.DeserializeObject(text);
                var componentTypes = GetEnumNumbers(deserializedObject, "ComponentType");
                var directions = GetEnumNumbers(deserializedObject, "Direction");
                var baseComponents = deserializedObject.ToObject<List<BaseComponent>>();

                MessageBox.Show(deserializedObject.ToString());
                for (var i = 0; i < baseComponents.Count; i++)
                {
                    baseComponents[i].ComponentType = (ComponentType) componentTypes[i];
                    baseComponents[i].Direction = (ComponentDirection) directions[i];
                }

                DeleteComponents();

                foreach (var t in baseComponents)
                {
                    AddComponent(t, t.Location);
                    //Fixes the wrong components from being made.
                    Thread.Sleep(20);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Debug.WriteLine("After Load " + Components.Count());
        }

        private static List<int> GetEnumNumbers(JArray jArray, string text)
        {
            //An example of jArray.ToString() to show what this algorithm is processing.
            //[
            //  {
            //    "ComponentType": 0,
            //    "Direction": 0,
            //    "ID": 1715365667,
            //    "Location": "156, 156",
            //    "Resistance": 0.0,
            //    "Current": 0.0,
            //    "Voltage": 0.0
            //  },
            //  {
            //    "ComponentType": 3,
            //    "Direction": 3,
            //    "ID": 2089864200,
            //    "Location": "156, 156",
            //    "Resistance": 0.0,
            //    "Current": 0.0,
            //    "Voltage": 0.0
            //  }
            //]
            var indexes = new List<int>();
            var index = 0;
            //Saves the index of each point of {text} to indexes.
            while (index != -1)
            {
                //If there are no items in indexes -> find the index of text from the 0th position in jArray.
                //If there are -> find the index of text from the position in jArray after the position of text in the last loop.
                index = indexes.Count == 0
                    ? jArray.ToString().IndexOf(text, 0, StringComparison.Ordinal)
                    : jArray.ToString().IndexOf(text, indexes.Last() + 1, StringComparison.Ordinal);

                //If the last position of text wasn't found, add text to indexes.
                if (index != -1)
                    indexes.Add(index);
            }

            var enumNumbers = new List<int>();

            //This outputs all the integer values of each enum value by looking up their position and saving them to enumNumbers.
            foreach (var typeIndex in indexes)
            {
                var fullNumber = "";
                //The only point of using a for loop here and fullNumber is in case the enum value uses 2 digits,
                //in case there are more component types added in the future.
                for (var j = 0; j < 2; j++)
                {
                    //E.g. for {ComponentType": 3,}, the 3 would be in the position of the location of ComponentType + length of ComponentType (13) + {": } (3) + 0.
                    //So typeIndex + text.Length + 3 + j would go into Substring.
                    var character = jArray.ToString().Substring(typeIndex + text.Length + 3 + j, 1);
                    int unusedInt;
                    //If character isnt't numeric
                    if (!int.TryParse(character, out unusedInt))
                        break;
                    fullNumber += character;
                }
                enumNumbers.Add(int.Parse(fullNumber));
            }

            return enumNumbers;
        }

        private void DeleteComponents()
        {
            //.ToArray() is used as the resizing feature of a list messes up with the foreach iteration
            //so without it, some PictureBoxes would still be visible.
            foreach (var pictureBox in panelCircuitBoard.Controls.OfType<PictureBox>().ToArray())
            {
                panelCircuitBoard.Controls.Remove(pictureBox);
            }
            Components.Clear();
        }

        private void DeleteComponent(PictureBox pictureBox)
        {
            //panelPictureBox is the PictureBox that is the same as pictureBoxSender, the foreach loop shoulnd't iterate.
            //This uses the LINQ statement 'Where', the alternative would be by using an if statement {if (panelPictureBox == pictureBoxSender)} within the foreach loop.
            foreach (var panelPictureBox in panelCircuitBoard.Controls.OfType<PictureBox>()
                        .Where(panelPictureBox => panelPictureBox == pictureBox))
            {
                //Gets the index of the component that pairs with pictureBoxSender
                var componentIndex = GetComponentIndex(panelPictureBox);
                //Deletes the Component and PictureBox that matches to this index.
                Components.RemoveAt(componentIndex);
                panelCircuitBoard.Controls.Remove(panelPictureBox);
            }
        }

        private void TranslateComponents(ArrowDirection direction)
        {
            int dy = 0;
            int dx = 0;
            //Sets the translation values.
            switch (direction)
            {
                case ArrowDirection.Up:
                    dy = -50;
                    dx = 0;
                    break;
                case ArrowDirection.Down:
                    dy = 50;
                    dx = 0;
                    break;
                case ArrowDirection.Left:
                    dy = 0;
                    dx = -50;
                    break;
                case ArrowDirection.Right:
                    dy = 0;
                    dx = 50;
                    break;
            }

            foreach (var pictureBox in panelCircuitBoard.Controls.OfType<PictureBox>())
            {
                //If the pictureBoxSender is outside, then skip this iteration.
                if (!CheckOutsideBounds(pictureBox)) continue;

                DialogResult result =  MessageBox.Show("There are components outside of the grid." + Environment.NewLine + Environment.NewLine + "Would you like these components deleted?", 
                    "Attention!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                    DeleteComponentsOutsidePanel();

                return;
            }


            foreach (var pictureBox in panelCircuitBoard.Controls.OfType<PictureBox>())
            {
                int fX = pictureBox.Location.X + dx;
                int fY = pictureBox.Location.Y + dy;
                //Checks if after translation, any PictureBox would be outside the panel. If so it doesn't allow a translation.
                if (fX < 0 || fY < 0 || fX > panelCircuitBoard.Width - 50 || fY > panelCircuitBoard.Height - 50)
                    return;
            }

            //Assigns the translation to all PictureBoxes in panelCircuitBoard.
            foreach (var pictureBox in panelCircuitBoard.Controls.OfType<PictureBox>())
            {
                pictureBox.Location = new Point(pictureBox.Location.X + dx, pictureBox.Location.Y + dy);
            }
            //Assigns the translation to all Components.
            foreach (var component in Components)
            {
                component.Location = new Point(component.Location.X + dx, component.Location.Y + dy);
            }
        }

        private void DeleteComponentsOutsidePanel()
        {
            //Goes through each PictureBox in panelPictureBox where the PictureBox is Outside the bounds of the panelPictureBox.
            //CheckOutsideBounds doesn't use panelPictureBox as an argument as the compiler infer this
            foreach (var panelPictureBox in panelCircuitBoard.Controls.OfType<PictureBox>().ToArray().Where(CheckOutsideBounds))
            {
                var componentIndex = GetComponentIndex(panelPictureBox);
                Components.RemoveAt(componentIndex);
                panelCircuitBoard.Controls.Remove(panelPictureBox);
            }
        }

        public static void SetComponentValues(object sender, FormClosedEventArgs e)
        {
            try
            {
                var valueChanger = (ValueChanger) sender;
                int i = valueChanger.ComponentIndex;
                //Sets the values of the component that valueChanger is set to change.
                Components[i].Resistance = valueChanger.Resistance;
                Components[i].Current = valueChanger.Current;
                Components[i].Voltage = valueChanger.Voltage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "PictureBox Methods"

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            const int gap = 50;
            var pictureBoxSender = ((PictureBox) sender);

            var originalPbLocation = pictureBoxSender.Location;
            //If LMB isn't down exit method
            if (e.Button != MouseButtons.Left) return;

            var coordinates = panelCircuitBoard.PointToClient(Cursor.Position);
            var roundedX = RoundDownToNearest(coordinates.X, gap);
            var roundedY = RoundDownToNearest(coordinates.Y, gap);

            pictureBoxSender.Location = new Point(roundedX, roundedY);

            //Keeps the pictureboxes within the bounds of panel.
            if (CheckOutsideBounds(pictureBoxSender))
            {
                pictureBoxSender.Location = originalPbLocation;
            }

            //Doesn't allow the PictureBox that is being moved to be in the same position as another.
            foreach (var pictureBox in panelCircuitBoard.Controls.OfType<PictureBox>())
            {
                if (CheckPictureBoxCollision(pictureBoxSender, pictureBox, gap))
                {
                    pictureBoxSender.Location = originalPbLocation;
                }
            }
            //Sets the location of the component pair of pictureBoxSender to be the same pictureBoxSender's location.
            RefreshComponentLocation(pictureBoxSender);
        }

        private void RefreshComponentLocation(PictureBox pictureBox)
        {
            foreach (var component in Components)
            {
                //If pictureBox and component matches, set the component's location to the location of pictureBox.
                if (pictureBox.Tag.ToString() == component.ID.ToString())
                {
                    component.Location = pictureBox.Location;
                }
            }
        }

        private void P_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ValueChanger.Opened == false)
                {
                    var i = GetComponentIndex((PictureBox) sender);
                    var r = Components[i].Resistance;
                    var c = Components[i].Current;
                    var v = Components[i].Voltage;
                    //Sends the index, and rvs values of the selected Component to the ValueChanger form.
                    var valueChanger = new ValueChanger(i, r, c, v)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    valueChanger.ShowDialog();
                }
            }

            //ctrl + LMB
            if (ModifierKeys == Keys.Control)
            {
                RotateComponent(sender);
            }

            if (ModifierKeys == Keys.Shift)
            {
                DeleteComponent((PictureBox) sender);
            }
        }

        private void P_MouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            RotateComponent(sender);
        }

        private void P_MouseHover(object sender, EventArgs e)
        {
            //Creates a tool tip which will hover over the selected PictureBox
            var toolTip1 = new ToolTip();
            var i = GetComponentIndex((PictureBox) sender);
            //Comverts the rvc values of the Component to engineering notation.
            var resistance = ValueChanger.GetNumEngForm(Components[i].Resistance);
            var voltage = ValueChanger.GetNumEngForm(Components[i].Voltage);
            var current = ValueChanger.GetNumEngForm(Components[i].Current);

            //Item1 is the value, Item2 is the SI Prefix.
            var valueMessage = "Resistance: " + resistance.Item1 + " " + resistance.Item2 + "Ω" + Environment.NewLine +
                               "Voltage: " + voltage.Item1 + " " + voltage.Item2 + "V" + Environment.NewLine +
                               "Current: " + current.Item1 + " " + current.Item2 + "I" + Environment.NewLine;
            //Shows the tool tip.
            toolTip1.SetToolTip((PictureBox) sender, valueMessage);
        }

        #endregion

        #region "Strip Menu Methods"

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid = !Grid;
            panelCircuitBoard.Refresh();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadComponents();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveComponents();
        }

        private void LoadItemClick(object sender, EventArgs e)
        {
            //Load circuit
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteComponents();
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.Select;
        }

        private void CellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.Cell;
        }

        private void resistorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.Resistor;
        }

        private void voltmeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.Voltmeter;
        }

        private void ammeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.Ammeter;
        }

        private void wire1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.WireStraight;
        }

        private void wire2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.WireTwoAlternate;
        }

        private void wire3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.WireThreeWay;
        }

        private void fourWayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircuitMode = Mode.WireFourWay;
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Circuit panel " + DateTime.Now.TimeOfDay;
        }
    }
}