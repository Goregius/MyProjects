using System.Drawing;
using Full_1.Properties;

namespace Full_1
{
    internal class Voltmeter : BaseComponent
    {
        public Voltmeter(ComponentDirection direction, double resistance, double voltage, double current)
            : base(direction, resistance, voltage, current)
        {
            ImageUp = Resources.VoltmeterV;
            ImageRight = Resources.Voltmeter;
            ImageDown = Resources.VoltmeterV;
            ImageLeft = Resources.Voltmeter; 

            ComponentType = ComponentType.Voltmeter;
            Direction = direction;
        }
    }
}