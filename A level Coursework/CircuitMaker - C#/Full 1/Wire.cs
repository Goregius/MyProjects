using Full_1.Properties;
namespace Full_1
{
    internal class Wire : BaseComponent
    {
        public Wire(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            ImageUp = Resources.Wire_Vertical;
            ImageRight = Resources.Wire_Horizontal;
            ImageDown = Resources.Wire_Vertical;
            ImageLeft = Resources.Wire_Horizontal;

            ComponentType = ComponentType.WireStraight;
            Direction = direction;
        }
    }
}