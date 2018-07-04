using System.Drawing;
using Full_1.Properties;

namespace Full_1
{
    internal class Resistor : BaseComponent
    {
        public Resistor(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            ImageUp = Resources.Resistor;
            ImageUp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ImageRight = Resources.Resistor;
            ImageDown = Resources.Resistor;
            ImageDown.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ImageLeft = Resources.Resistor;

            ComponentType = ComponentType.Resistor;
            Direction = direction;
        }
    }
}