using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace Full_1
{
    public enum ComponentDirection
    {
        Right = 0,
        Left = 1,
        Up = 2,
        Down = 3
    }

    [JsonObject(ItemRequired = Required.Always)]
    public class BaseComponent
    {
        public BaseComponent(ComponentDirection direction, double resistance, double voltage, double current)
        {
            if (IDs == null)
                IDs = new List<int>();
            
            //An ID is created so I can find a created picturebox by comparing the ID of a Component object and a PictureBox on screen.
            ID = CreateId();

            Resistance = resistance;
            Current = voltage;
            Voltage = current;
        }

        public ComponentType ComponentType { get; internal set; }

        private ComponentDirection _direction;

        public ComponentDirection Direction
        {
            get { return _direction; }
            set
            {
                //Sets the CurrentImage when _direction is changed.
                switch (value)
                {
                    case ComponentDirection.Right:
                        CurrentImage = ImageRight;
                        break;
                    case ComponentDirection.Left:
                        CurrentImage = ImageLeft;
                        break;
                    case ComponentDirection.Up:
                        CurrentImage = ImageUp;
                        break;
                    case ComponentDirection.Down:
                        CurrentImage = ImageDown;
                        break;
                }
                _direction = value;
            }
        }
        
        public static List<int> IDs { get; private set; }
        public int ID { get; set; }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                //Makes sure that the location won't be negative
                if (value.X >= 0 & value.Y >= 0)
                    _location = value;
            }
        }

        //These attributes makes this property not required to be serialized into json string, so no error when serializing.
        [JsonIgnore]
        [JsonProperty(Required = Required.Default)]
        public Image CurrentImage { get; set; }
        internal Image ImageUp { get; set; }
        internal Image ImageRight { get; set; }
        internal Image ImageDown { get; set; }
        internal Image ImageLeft { get; set; }

        public double Resistance { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }

        /// <summary>
        /// Randomly creates ID without a chance of a duplicate.
        /// </summary>
        /// <returns></returns>
        private int CreateId()
        {
            var rnd = new Random();
            int rndNumber = -1;
            do
            {
                int num = rnd.Next();

                //If IDs contains the randomly generated number (if a duplication would occur).
                if (!IDs.Contains(num))
                {
                    rndNumber = rnd.Next();
                }
                
            //rndNumber cannot be -1 as integers returned from rnd.Next() is always a natural number.
            } while (rndNumber == -1);

            //Add the generated number to the list of used IDs
            IDs.Add(rndNumber);
            return rndNumber;
        }
    }
}