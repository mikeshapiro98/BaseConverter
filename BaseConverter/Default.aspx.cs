using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace BaseConverter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void convertButton_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "";
            // initialize masterNumeralSystem array
            List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•',' ' };

            // use user selection to create chosenNumeralSystem array
            int chosenBase = Convert.ToInt32(originBaseDropDownList.SelectedValue);
            List<char> chosenNumeralSystem = masterNumeralSystem.Take(chosenBase).ToList();

            // validate user input
            string input = inputTextBox.Text;
            char[] inputArray = input.ToCharArray();
            if(ValidateInput(chosenNumeralSystem, inputArray))
            {
                // if inputArray is negative, make final answer negative
                if (inputArray[0] == '-')
                {
                    resultLabel.Text = "-";
                    inputArray = inputArray.Skip(1).ToArray();
                }
                // if user input is an integer
                if (inputArray[inputArray.Length -1] == '.' || !inputArray.Contains('.'))
                {
                    resultLabel.Text += CalculateValueOfPositiveExponentArray(inputArray, chosenBase, masterNumeralSystem).ToString();
                }
                // if user input is a fractional number < 0
                else if (inputArray[0] == '.')
                {
                    inputArray = inputArray.Skip(1).ToArray();
                    resultLabel.Text += CalculateValueOfNegativeExponentArray(inputArray, chosenBase, masterNumeralSystem).ToString();
                }
                // if user input is a fractional number > 0
                else
                {
                    // split the array on the period
                    char[] integerArray = inputArray.TakeWhile(x => x != '.').ToArray();
                    resultLabel.Text += CalculateValueOfPositiveExponentArray(integerArray, chosenBase, masterNumeralSystem).ToString() + ".";
                    char[] fractionArray = inputArray.SkipWhile(x => x != '.').ToArray();
                    fractionArray = fractionArray.Skip(1).ToArray();
                    resultLabel.Text += CalculateValueOfNegativeExponentArray(fractionArray, chosenBase, masterNumeralSystem).ToString().TrimStart('0','.');
                }
            }
        }

        // calculate the decimal value of a user's fractional input
        private double CalculateValueOfNegativeExponentArray(char[] inputArray, int chosenBase, List<char> masterNumeralSystem)
        {
            double decimalValue = 0;
            var placeCounter = -(inputArray.Length);
            for (int i = inputArray.Length -1; i >= 0; i--)
            {
                var digit = inputArray[i];
                int digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                decimalValue += (digitValue * CalculatePlaceValue(chosenBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a user's integer input
        private double CalculateValueOfPositiveExponentArray(char[] inputArray, int chosenBase, List<char> masterNumeralSystem)
        {
            double decimalValue = 0;
            var placeCounter = 0;
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                int digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                decimalValue += (digitValue * CalculatePlaceValue(chosenBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a given place by raising the chosenBase to the power of that place
        private double CalculatePlaceValue(int chosenBase, int placeCounter)
        {
            double doubleBase = Convert.ToDouble(chosenBase);
            double doublePlace = Convert.ToDouble(placeCounter);
            double placeValue = Math.Pow(doubleBase, doublePlace);
            return placeValue;
        }

        // calculate the decimal value of a given digit
        private int CalculateDigitValue(char digit, List<char> masterNumeralSystem)
        {
            int digitValue;
            if (digit == '0')
            {
                digitValue = 0;
            }
            else if (digit == '1')
            {
                digitValue = 1;
            }
            else
            {
                digitValue = masterNumeralSystem.IndexOf(digit);
            }
            return digitValue;
        }

        // prevent bad user inputs
        private bool ValidateInput(List<char> chosenNumeralSystem, char[] inputArray)
        {
            var minusCounter = 0;
            var periodCounter = 0;
            foreach (var digit in inputArray)
            {
                // track quantity of minuses and periods in inputArray
                if (digit == '-')
                {
                    minusCounter += 1;
                }
                else if (digit == '.')
                {
                    periodCounter += 1;
                }
                // ensure all chars in inputArray exist in chosenNumeralSystem
                else if (!chosenNumeralSystem.Contains(digit))
                {
                    resultLabel.Text = String.Format("<span style='color:#B33A3A;'>Would you please only enter characters that exist in the {0} number system?</span>",
                        originBaseDropDownList.SelectedItem.Text);
                    return false;
                }
            }
            // throw exceptions if there are multiple minuses or periods
            if (minusCounter > 1)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple minus signs?</span>";
                return false;
            }
            if (periodCounter > 1)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple periods?</span>";
                return false;
            }
            // throw exceptions if there's an inappropriate minus
            if (minusCounter == 1 && inputArray[0] != '-')
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter a minus sign anywhere but in front of your number?</span>";
                return false;
            }
            if (inputArray.ToString() == "-0")
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please enter a valid number?</span>";
                return false;
            }
            return true;
        }
    }
}