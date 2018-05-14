using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DecimalToX
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void convertButton_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "";
            // initialize masterNumeralSystem and decimalSystem array
            List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', ' ' };
            List<char> decimalSystem = masterNumeralSystem.Take(10).ToList();

            // use user selection to create targetNumeralSystem array
            int targetBase = Convert.ToInt32(targetBaseDropDownList.SelectedValue);
            List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

            // validate user input
            string input = inputTextBox.Text;
            char[] inputArray = input.ToCharArray();
            if (ValidateInput(decimalSystem, inputArray))
            {
                // if inputArray is negative, make final answer negative
                if (inputArray[0] == '-')
                {
                    resultLabel.Text += "-";
                    inputArray = inputArray.Skip(1).ToArray();
                }
                // if user input is an integer
                if (inputArray[inputArray.Length - 1] == '.' || !inputArray.Contains('.'))
                {
                    var intInput = Convert.ToInt64(input);
                    if (intInput < 0)
                    {
                        intInput = intInput * -1;
                    }
                    CalculateIntegerValue(targetBase, intInput, targetNumeralSystem);
                }
                // if user input is a fractional number < 0
                else if (inputArray[0] == '.')
                {
                    inputArray = inputArray.Skip(1).ToArray();
                    input = input.TrimStart('.');
                    var intInput = Convert.ToInt64(input);
                    if (intInput < 0)
                    {
                        intInput = intInput * -1;
                    }
                    CalculateFractionalValue(targetBase, intInput, targetNumeralSystem);
                }
                // if user input is a fractional number > 0
                else
                {
                    // split the array on the period
                    char[] integerArray = inputArray.TakeWhile(x => x != '.').ToArray();
                    string integerString = new string(integerArray);
                    long intInput = Convert.ToInt64(integerString);
                    if (intInput < 0)
                    {
                        intInput = intInput * -1;
                    }

                    char[] fractionArray = inputArray.SkipWhile(x => x != '.').ToArray();
                    fractionArray = fractionArray.Skip(1).ToArray();
                    string fractionString = new string(fractionArray);
                    long fractionInput = Convert.ToInt64(fractionString);
                    if (fractionInput < 0)
                    {
                        fractionInput = fractionInput * -1;
                    }

                    // convert integer input
                    CalculateIntegerValue(targetBase, intInput, targetNumeralSystem);

                    resultLabel.Text += '.';

                    // convert fractional number from base 10 to base X
                    CalculateFractionalValue(targetBase, fractionInput, targetNumeralSystem);
                } 
            }
        }

        private void CalculateFractionalValue(int targetBase, long fractionInput, List<char> targetNumeralSystem)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Fractional values cannot exist in the Base 1 (Unary) numeral system.</span>";
            }
            else
            {
                resultLabel.Text += "Support for fractional numbers coming soon!";
            }
            /*
            // decrement placeCounter until you reach a placeValue < intInput
            var placeCounter = -1;
            long placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
            while (fractionInput <= placeValue)
            {
                placeCounter--;
                placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
            }
            // increment placeCounter and recalculate placeValue
            placeCounter++;
            placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
            while (placeCounter <= -1)
            {
                if (intInput < placeValue)
                {
                    // calculate dividend, concatenate dividend to resultLabel
                    long dividend = intInput / placeValue;
                    // concatenate dividend in target numeralSystem to resultLabel
                    char digitCharacter = DetermineDigitCharacter(dividend, targetNumeralSystem);
                    resultLabel.Text += digitCharacter.ToString();
                    // subtract placeValue from intInput
                    intInput -= (placeValue * dividend);
                }
                else
                {
                    resultLabel.Text += '0';
                }
                // increment placeCounter and recalculate placeValue
                placeCounter++;
                placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
            }
            */
        }

        private void CalculateIntegerValue(int targetBase, long intInput, List<char> targetNumeralSystem)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                for (int i = 0; i < intInput; i++)
                {
                    resultLabel.Text += '1';
                }
            }
            else
            {
                // increment placeCounter until you reach a placeValue > intInput
                var placeCounter = 0;
                long placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
                while (intInput >= placeValue)
                {
                    placeCounter++;
                    placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
                }
                // decrement placeCounter and recalculate placeValue
                placeCounter--;
                placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
                while (placeCounter >= 0)
                {
                    if (intInput >= placeValue)
                    {
                        // calculate dividend, concatenate dividend to resultLabel
                        long dividend = intInput / placeValue;
                        // concatenate dividend in target numeralSystem to resultLabel
                        char digitCharacter = DetermineDigitCharacter(dividend, targetNumeralSystem);
                        resultLabel.Text += digitCharacter.ToString();
                        // subtract placeValue from intInput
                        intInput -= (placeValue * dividend);
                    }
                    else
                    {
                        resultLabel.Text += '0';
                    }
                    // decrement placeCounter and recalculate placeValue
                    placeCounter--;
                    placeValue = Convert.ToInt64(CalculatePlaceValue(targetBase, placeCounter));
                }
            }
        }

        private double CalculatePlaceValue(int targetBase, int placeCounter)
        {
            double doubleBase = Convert.ToDouble(targetBase);
            double doublePlace = Convert.ToDouble(placeCounter);
            double placeValue = Math.Pow(doubleBase, doublePlace);
            return placeValue;
        }

        // calculate the digit character of a given decimal value
        private char DetermineDigitCharacter(long dividend, List<char> targetNumeralSystem)
        {
            char digitCharacter;
            if (dividend == 1)
            {
                digitCharacter = '1';
            }
            else if (dividend == 0)
            {
                digitCharacter = '0';
            }
            else
            {
                digitCharacter = targetNumeralSystem.ElementAt(Convert.ToInt32(dividend));
            }
            return digitCharacter;
        }

        // prevent bad user inputs
        private bool ValidateInput(List<char> decimalSystem, char[] inputArray)
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
                else if (!decimalSystem.Contains(digit))
                {
                    resultLabel.Text = String.Format("<span style='color:#B33A3A;'>Would you please only enter characters that exist in the Base 10 (Decimal) number system?</span>");
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