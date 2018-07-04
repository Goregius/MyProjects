using System.Drawing;
using Full_1.Properties;

namespace Full_1
{
    internal class WireThreeWay : BaseComponent
    {
        public WireThreeWay(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            //Direction is where the most anti clockwise wire is facing.
            //E.g. For up right, the most anti clockwise wire is facing to the right.
            ImageUp = Resources._3_Way_Up;
            ImageUp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ImageRight = Resources._3_Way_Up;
            ImageDown = Resources._3_Way_Down;
            ImageDown.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ImageLeft = Resources._3_Way_Down;
            

            ComponentType = ComponentType.WireThreeWay;
            Direction = direction;
        }
    }
}