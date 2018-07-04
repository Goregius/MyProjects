using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Full_1
{
    public partial class ValueChanger : Form
    {
        public ValueChanger(int i, double r, double c, double v)
        {
            InitializeComponent();

            if (Opened)
                Close();

            FormClosed += CircuitForm.SetComponentValues;
            ComponentIndex = i;
            Resistance = r;
            Current = c;
            Voltage = v;

            SiPrefixes = new Dictionary<char, double>();
            //Sets the SiPrefixes.
            SiPrefixes.Add('Y', GetTenPower(24));//Yotta (10*24)
            SiPrefixes.Add('Z', GetTenPower(21));//Zetta (10*21)
            SiPrefixes.Add('E', GetTenPower(18));//Exa (10*18)
            SiPrefixes.Add('P', GetTenPower(15));//Peta (10*15)
            SiPrefixes.Add('T', GetTenPower(12));//Tera (10*12)
            SiPrefixes.Add('G', GetTenPower(9));//Giga (10*9)
            SiPrefixes.Add('M', GetTenPower(6));//Mega (10*6)
            SiPrefixes.Add('K', GetTenPower(3));//Kilo (10*3)
            SiPrefixes.Add('1', 0);// (10*0)
            SiPrefixes.Add('m', GetTenPower(-3));//Milli (10*-3)
            SiPrefixes.Add('µ', GetTenPower(-6));//Micro (10*-6)
            SiPrefixes.Add('n', GetTenPower(-9));//Nano (10*-9)
            SiPrefixes.Add('p', GetTenPower(-12));//Pico (10*-12)
            SiPrefixes.Add('f', GetTenPower(-15));//Femto (10*-15)
            SiPrefixes.Add('a', GetTenPower(-18));//Atto (10*-18)
            SiPrefixes.Add('z', GetTenPower(-21));//Zepto (10*-21)
            SiPrefixes.Add('y', GetTenPower(-24));//Yocto (10*-24)


            SiPrefixesInverse = new Dictionary<double, char>();
            //Inverses SiPrefixes
            foreach (var siPrefix in SiPrefixes)
                SiPrefixesInverse.Add(siPrefix.Value, siPrefix.Key);
            Opened = true;
        }

        public int ComponentIndex { get; set; }

        public double Resistance { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }

        public static Dictionary<char, double> SiPrefixes {get; set; }
        public static Dictionary<double, char> SiPrefixesInverse { get; set; }
        public static bool Opened { get; set; }
        
        //Returns 10^num
        private double GetTenPower(int num)
        {
            double dNum = Convert.ToDouble(num);
            return Math.Pow(10, dNum);
        }

        private void ValueChanger_Load(object sender, EventArgs e)
        {
            //Tests the loaded rcv values to be the values sent from the Circuit Form, which is converted to engineering notation.
            var resistance = GetNumEngForm(Resistance);
            var current = GetNumEngForm(Current);
            var voltage = GetNumEngForm(Voltage);

            //Sets the TextBoxes to the values of resistance, current and voltage.
            textBoxResistance.Text = resistance.Item1.ToString();
            textBoxCurrent.Text = current.Item1.ToString();
            textBoxVoltage.Text = voltage.Item1.ToString();

            //Adds all the Si Prefixes to each ComboBox with the suitable unit.
            foreach (var prefix in SiPrefixes)
            {
                //Since 10^0 has no prefix, only the unit symbol is shown.
                if (prefix.Key == '1')
                {
                    comboBoxR.Items.Add("Ω");
                    comboBoxV.Items.Add("V");
                    comboBoxC.Items.Add("I");
                }
                else
                {
                    comboBoxR.Items.Add(prefix.Key + "Ω");
                    comboBoxV.Items.Add(prefix.Key + "V");
                    comboBoxC.Items.Add(prefix.Key + "I");
                }
            }
            //Sets the ComboBoxes to the Si Prefixes of resistance, current and voltage.
            SetDropDownListText(comboBoxR, resistance.Item2 + "Ω");
            SetDropDownListText(comboBoxV, voltage.Item2 + "V");
            SetDropDownListText(comboBoxC, current.Item2 + "I");
        }

        public void SetDropDownListText(ComboBox comboBox, string text)
        {
            //Sets the selected item of comboBox to text, if any of the items of comboBox = text.
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                }
            }
        }

        //RoundToSignificantDigits is not my algorithm.
        //from URL: www.stackoverflow.com/questions/374316/round-a-double-to-x-significant-figures
        private double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            try
            {
                double resistanceOhms = GetBaseNumber("Ω", comboBoxR, textBoxResistance);
                double currentAmps = GetBaseNumber("I", comboBoxC, textBoxCurrent);
                double voltageVolts = GetBaseNumber("V", comboBoxV, textBoxVoltage);

                //Using V=IR.
                //RoundToSignificantDigits is used because of a loss of precision of the rvc values due to rounding.
                if (RoundToSignificantDigits((voltageVolts), 2) != RoundToSignificantDigits(resistanceOhms * currentAmps, 2))
                {
                    throw new Exception("voltage needs to equal currents * resistance.");
                }

                double largestNumber = (new List<double>() { resistanceOhms, currentAmps, voltageVolts }).Max();
                if (largestNumber > Math.Pow(10, 26))
                {
                    throw new Exception("A number you tried to store is too large.");
                }
                //I tried double smallestNumber = (new List<double>() { resistanceOhms, currentAmps, voltageVolts }).Min();
                //This didn't work, the smallestNumber would usually just incorrectly be assigned to 0.
                if (resistanceOhms < SiPrefixes.Values.Last()  && resistanceOhms > 0
                    || currentAmps < SiPrefixes.Values.Last() && currentAmps > 0
                    || voltageVolts < SiPrefixes.Values.Last() && voltageVolts > 0)
                {
                    throw new Exception("A number you tried to store is too small.");
                }

                //Rounds all the rvc values to 3 significant figures.
                resistanceOhms = RoundToSignificantDigits(resistanceOhms, 3);
                currentAmps = RoundToSignificantDigits(currentAmps, 3);
                voltageVolts = RoundToSignificantDigits(voltageVolts, 3);

                Resistance = resistanceOhms;
                Current = currentAmps;
                Voltage = voltageVolts;
                //Closes the form.
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private double GetBaseNumber(string symbol, ComboBox comboBox, TextBox textBox)
        {
            //Gets the SI prefix by searching through SiPrefixes using the text of comboBox, unless there is no prefix
            //which is when comboBox.SelectedItem.ToString() == symbol. ("V", "I" or "Ω" means no prefix) and it assigns it to 1 then.
            var prefixNum = (comboBox.SelectedItem.ToString() == symbol) ? 1 : Convert.ToDouble(SiPrefixes[comboBox.SelectedItem.ToString()[0]]);

            //The direct value is given by the text in textBox, parsed as a double.
            var numDirect = double.Parse(textBox.Text);
            Debug.WriteLine("Direct: " + numDirect);

            //The baseNum 
            var baseNum = numDirect * prefixNum;
            Debug.WriteLine("Ohms: " + baseNum + ", using prefix: " + prefixNum);

            return baseNum;
        }

        private bool TextBoxEmpty(TextBox textBox)
        {
            if (textBox.Text == "" || textBox.Text == string.Empty)
            {
                return true;
            }
            return false;
        }

        public static Tuple<double, string> GetNumEngForm(double baseValue)
        {
            bool positive = true;

            //This is used if the baseValue is negative, since that the log of a negative number doesn't exist, so the negative sign is added at the end.
            if (baseValue < 0)
            {
                baseValue = Math.Abs(baseValue);
                positive = false;
            }

            //If the value entered is 0, it returns 0  with no unit.
            if (baseValue == 0)
            {
                return new Tuple<double, string>(0, "");
            }

            //Example: when baseValue = 56234.754, power = 4 (log of 56.754 = 4.7500048...)
            //the power would be the power that the standard notation would use (56234.754 -> 5.6234754 * 10^4).
            var power = Math.Floor(Math.Log10(baseValue));

            //Example: This converts the standard notation to engineering notation.
            //The line with the ? operator is used as the way of converting is different 
            double powerRemainder = power%3;
            power -= (powerRemainder >= 0) ? powerRemainder : 3 + powerRemainder;

            //This calculates the left size of the full engineering form.
            double finalValue = baseValue*Math.Pow(10, -power);
            //This calculates the right size of the full engineering form.
            double powerAmount = Math.Pow(10, power);

            //This converts the number back to a negative if the original value was negative and made into positive.
            if (!positive)
            {
                finalValue = -finalValue;
            }

            //If the powerAmount = 1 (10^0), return with the final value but no unit
            //Searching the SiPrefixInverse dictionary is pointless and would increase the chance of an error.
            if (powerAmount == 1)
                return new Tuple<double, string>(finalValue, "");

            //the prefix character is searched for using the powerAmount into the SiPrefixesInverse dictionary.
            char prefix = SiPrefixesInverse[powerAmount];
            Debug.WriteLine("prefix: " + prefix);

            //This returns the final value with its prefix.
            return new Tuple<double, string>(finalValue, prefix.ToString());
        }

        private void ValueChanger_FormClosed(object sender, FormClosedEventArgs e)                                                                                                                                               
        {
            Opened = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            try
            {
                //V=IR, R=V/I, I=V/R.
                double resistanceOhms;
                double currentAmps;
                double voltageVolts;
                if (TextBoxEmpty(textBoxResistance))
                {
                    //R=V/I
                    voltageVolts = GetBaseNumber("V", comboBoxV, textBoxVoltage);
                    currentAmps = GetBaseNumber("I", comboBoxC, textBoxCurrent);

                    if (currentAmps == 0)
                        throw new DivideByZeroException();

                    resistanceOhms = RoundToSignificantDigits((voltageVolts / currentAmps), 3);

                    Tuple<double, string> resistance = GetNumEngForm(resistanceOhms);

                    textBoxResistance.Text = resistance.Item1.ToString();
                    SetDropDownListText(comboBoxR, resistance.Item2 + "Ω");

                }
                if (TextBoxEmpty(textBoxVoltage))
                {
                    //V=IR
                    resistanceOhms = GetBaseNumber("Ω", comboBoxR, textBoxResistance);
                    currentAmps = GetBaseNumber("I", comboBoxC, textBoxCurrent);

                    voltageVolts = RoundToSignificantDigits((currentAmps * resistanceOhms), 3);

                    Tuple<double, string> voltage = GetNumEngForm(voltageVolts);

                    textBoxVoltage.Text = voltage.Item1.ToString();
                    SetDropDownListText(comboBoxV, voltage.Item2 + "V");
                }
                if (TextBoxEmpty(textBoxCurrent))
                {
                    //I=V/R
                    voltageVolts = GetBaseNumber("V", comboBoxV, textBoxVoltage);
                    resistanceOhms = GetBaseNumber("Ω", comboBoxR, textBoxResistance);

                    if (resistanceOhms == 0)
                        throw new DivideByZeroException();

                    currentAmps = RoundToSignificantDigits((voltageVolts / resistanceOhms), 3);
                    Tuple<double, string> current = GetNumEngForm(currentAmps);

                    textBoxCurrent.Text = current.Item1.ToString();
                    SetDropDownListText(comboBoxC, current.Item2 + "I");
                }
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("The number generated is either too big or too small.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
