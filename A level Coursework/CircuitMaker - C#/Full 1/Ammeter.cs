using Full_1.Properties;

namespace Full_1
{
    internal class Ammeter : BaseComponent
    {
        public Ammeter(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            ImageUp = Resources.AmmeterV;
            ImageRight = Resources.Ammeter;
            ImageDown = Resources.AmmeterV;
            ImageLeft = Resources.Ammeter;

            ComponentType = ComponentType.Ammeter;
            Direction = direction;
        }
    }
}