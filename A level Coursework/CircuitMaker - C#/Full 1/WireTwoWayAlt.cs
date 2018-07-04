using Full_1.Properties;

namespace Full_1
{
    internal class WireTwoWayAlt : BaseComponent
    {
        public WireTwoWayAlt(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            //Direction is where the most clockwise wire is facing.
            //E.g. For up right, the most clockwise wire is facing to the right.
            
            ImageRight = Resources._2_Way_Up_Right;
            ImageDown = Resources._2_Way_Right_Down;
            ImageLeft = Resources._2_Way_Right_Up;
            ImageUp = Resources._2_Way_Down_Right;

            ComponentType = ComponentType.WireTwoAlternate;
            Direction = direction;
        }
    }
}