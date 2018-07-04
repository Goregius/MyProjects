using Full_1.Properties;

namespace Full_1
{
    internal class WireFourWay : BaseComponent
    {
        public WireFourWay(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            //Direction is where the most anti clockwise wire is facing.
            //E.g. For up right, the most anti clockwise wire is facing to the right.
            ImageUp = Resources._4_way;
            ImageRight = Resources._4_way;
            ImageDown = Resources._4_way;
            ImageLeft = Resources._4_way;

            ComponentType = ComponentType.WireFourWay;
            Direction = direction;
        }
    }
}