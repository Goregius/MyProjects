using System.Drawing;
using Full_1.Properties;

namespace Full_1
{
    internal class Cell : BaseComponent
    {
        public Cell(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            ImageUp = Resources.Power_Supply;
            ImageUp.RotateFlip(RotateFlipType.Rotate90FlipY);
            ImageRight = Resources.Power_Supply;
            ImageDown = Resources.Power_Supply;
            ImageDown.RotateFlip(RotateFlipType.Rotate90FlipNone);
            ImageLeft = Resources.Power_Supply;
            ImageLeft.RotateFlip(RotateFlipType.RotateNoneFlipX);

            ComponentType = ComponentType.Cell;
            Direction = direction;
        }
    }
}