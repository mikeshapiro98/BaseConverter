using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace BaseXToBaseY
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void convertButton_Click(object sender, EventArgs e)
        {
            // PHASE 1. CONVERT INPUT FROM ORIGIN BASE TO DECIMAL

            // reset resultLabel
            resultLabel.Text = "";

            // initialize masterNumeralSystem list
            List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽','¢','₩','§','¿','¡','ß' };

            // create originNumeralSystem list from user selection
            int originBase = Convert.ToInt32(originDropDownList.SelectedValue);
            List<char> originNumeralSystem = masterNumeralSystem.Take(originBase).ToList();

            // read, split into character array, and validate user input
            string input = inputTextBox.Text;
            char[] inputArray = input.ToCharArray();
            if (ValidateInput(originNumeralSystem, inputArray))
            {
                // if user input is negative
                if (inputArray[0] == '-')
                {
                    // append minus sign to resultLabel and remove minus sign from inputArray
                    resultLabel.Text += "-";
                    inputArray = inputArray.Skip(1).ToArray();
                }
                // split array on the period
                if (inputArray.Contains('.'))
                {
                    char[] integerArray = inputArray.TakeWhile(x => x != '.').ToArray();
                    char[] fractionArray = inputArray.SkipWhile(x => x != '.').ToArray();
                    fractionArray = fractionArray.Skip(1).ToArray();
                    // append decimal value of integer and decimal value of fraction to resultLabel
                    resultLabel.Text += CalculateDecimalValueOfInteger(integerArray, originBase, masterNumeralSystem).ToString() + ".";
                    resultLabel.Text += CalculateDecimalValueOfFraction(fractionArray, originBase, masterNumeralSystem).ToString().TrimStart('0','.');
                }
                else
                {
                    resultLabel.Text += CalculateDecimalValueOfInteger(inputArray, originBase, masterNumeralSystem).ToString();
                }
                // STEP 2. CONVERT INPUT FROM DECIMAL TO TARGET BASE

                // assign decimal value of input to a variable
                double decimalInput = Convert.ToDouble(resultLabel.Text);

                // split decimal value of input into a character array
                char[] decimalInputArray = resultLabel.Text.ToCharArray();

                // clear resultLabel
                resultLabel.Text = "";

                // create targetNumeralSystem list from user selection
                int targetBase = Convert.ToInt32(targetDropDownList.SelectedValue);
                List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

                // if decimalInput is negative
                if (decimalInputArray[0] == '-')
                {
                    // append minus sign to resultLabel, remove minus sign from decimalInputArray, make decimalInput positive
                    resultLabel.Text += "-";
                    decimalInputArray = decimalInputArray.Skip(1).ToArray();
                    decimalInput = decimalInput * -1;
                }

                // split array on the period
                if (decimalInputArray.Contains('.'))
                {
                    // split decimalInputArray on the period into integerArray and fractionArray
                    char[] newIntegerArray = decimalInputArray.TakeWhile(x => x != '.').ToArray();
                    char[] newFractionArray = decimalInputArray.SkipWhile(x => x != '.').ToArray();

                    // convert integerArray and fractionArray into strings
                    string integerString = new string(newIntegerArray);
                    string fractionString = new string(newFractionArray);

                    // convert integerString and fractionString into a long and a double, respectively
                    long longDecimalInput = Convert.ToInt64(integerString);
                    double fractionDecimalInput = Convert.ToDouble(fractionString);

                    //  calculate longInput's value in the targetNumeralSystem, append that value to resultLabel
                    CalculateBaseXIntegerValue(targetBase, longDecimalInput, targetNumeralSystem);

                    // calculate fractionInput's value in the targetNumeralSystem, append a period and that value to resultLabel
                    resultLabel.Text += '.';
                    CalculateBaseXFractionValue(targetBase, fractionDecimalInput, targetNumeralSystem);
                }
                else
                {
                    // convert decimalInput to long, calculate its new value in the targetNumeralSystem, append that value to resultLabel
                    long longDecimalInput = Convert.ToInt64(decimalInput);
                    CalculateBaseXIntegerValue(targetBase, longDecimalInput, targetNumeralSystem);
                }
                
                // assign the input's value in the targetNumeralSystem to a variable
                var convertedNumber = resultLabel.Text;

                // format results for display
                resultLabel.Text = String.Format("{0}<sub>{1}</sub> = {2}<sub>{3}</sub>",
                    input,
                    originDropDownList.SelectedValue,
                    convertedNumber,
                    targetDropDownList.SelectedValue);
            }
        }

        // prevent faulty user input
        private bool ValidateInput(List<char> originNumeralSystem, char[] inputArray)
        {
            // initialize minus and period counters
            var minusCounter = 0;
            var periodCounter = 0;
            // track quantity of minuses and periods in inputArray
            foreach (var digit in inputArray)
            {
                if (digit == '-')
                {
                    minusCounter++;
                }
                else if (digit == '.')
                {
                    periodCounter++;
                }
                // ensure every char in inputArray exists in originNumeralSystem
                else if (!originNumeralSystem.Contains(digit))
                {
                    resultLabel.Text = String.Format("<span style='color:#B33A3A;'>Would you please only enter characters that exist in the {0} number system?</span>",
                        originDropDownList.SelectedItem.Text);
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

        // calculate the decimal value of an integer
        private double CalculateDecimalValueOfInteger(char[] inputArray, int originBase, List<char> masterNumeralSystem)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = 0;
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                int digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a fraction
        private double CalculateDecimalValueOfFraction(char[] inputArray, int originBase, List<char> masterNumeralSystem)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = -(inputArray.Length);
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                double digitValue = Convert.ToDouble(CalculateDigitValue(digit, masterNumeralSystem));
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a digit
        private int CalculateDigitValue(char digit, List<char> masterNumeralSystem)
        {
            // initialize digit variable
            int digitValue;
            // account for unary and binary edge cases
            if (digit == '0')
            {
                digitValue = 0;
            }
            else if (digit == '1')
            {
                digitValue = 1;
            }
            // otherwise, retrieve the decimal value of the digit from its index in the masterNumeralSystem list
            else
            {
                digitValue = masterNumeralSystem.IndexOf(digit);
            }
            return digitValue;
        }

        // calculate the decimal value of a given place by raising the originBase to the power of that place
        private double CalculatePlaceValue(int givenBase, int placeCounter)
        {
            // convert originBase and placeCounter to doubles to use C# Math.Pow function
            double doubleBase = Convert.ToDouble(givenBase);
            double doublePlace = Convert.ToDouble(placeCounter);
            // calculate placeValue and convert it to a long for return
            double doublePlaceValue = Math.Pow(doubleBase, doublePlace);
            return doublePlaceValue;
        }

        private void CalculateBaseXIntegerValue(int targetBase, long longInput, List<char> targetNumeralSystem)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                for (int i = 0; i < longInput; i++)
                {
                    // append a number of tally marks equal to longInput to resultLabel
                    resultLabel.Text += '1';
                }
            }
            else
            {
                // initialize placeCounter and placeValue variables
                int placeCounter = 0;
                double placeValue = 1;
                // increment placeCounter and recalculate placeValue until placeValue > intInput
                while (longInput >= placeValue)
                {
                    placeCounter++;
                    placeValue = CalculatePlaceValue(targetBase, placeCounter);
                }
                // decrement placeCounter and recalculate placeValue
                placeCounter--;
                placeValue = CalculatePlaceValue(targetBase, placeCounter);
                // convert longInput to targetBase and append converted number, character by character, to resultLabel
                while (placeCounter >= 0)
                {
                    if (longInput >= placeValue)
                    {
                        // calculate dividend
                        long dividend = longInput/Convert.ToInt64(placeValue);
                        // determine digit equal to dividend in targetNumeralSystem
                        char digit = DetermineDigit(dividend, targetNumeralSystem);
                        // append digit to resultLabel
                        resultLabel.Text += digit.ToString();
                        // subtract placeValue from intInput
                        longInput -= (Convert.ToInt64(placeValue) * dividend);
                    }
                    else
                    {
                        resultLabel.Text += '0';
                    }
                    // decrement placeCounter and recalculate placeValue
                    placeCounter--;
                    placeValue = CalculatePlaceValue(targetBase, placeCounter);
                }
            }
        }

        private void CalculateBaseXFractionValue(int targetBase, double fractionInput, List<char> targetNumeralSystem)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Fractional values cannot exist in the Base 1 (Unary) numeral system.</span>";
                return;
            }
            else
            {
                // determine the # of places up to which to be precise
                bool precise = Int64.TryParse(placesTextBox.Text, out long places);
                // throw exception if places is not a positive long
                if (!precise || places < 0)
                {
                    resultLabel.Text = "<span style='color:#B33A3A;'>Please enter a positive integer in the places field.</span>";
                    return;
                }
                else
                {
                    // convert targetBase to a double to use in multiplication
                    double doubleBase = Convert.ToDouble(targetBase);
                    // initialize loopCounter
                    int loopCounter = 0;
                    // set while loop to terminate when fractionInput has been fully converted, or when # of places is reached
                    while (fractionInput > 0 && loopCounter < places)
                    {
                        // multiply fractionInput by targetBase, assign to double appendageCarry
                        double appendageCarry = fractionInput * targetBase;
                        // assign integer part of appendageCarry to int appendage
                        int appendage = Convert.ToInt16(Math.Floor(appendageCarry));
                        // determine digit equal to appendage in targetNumeralSystem
                        char digit = DetermineDigit(appendage, targetNumeralSystem);
                        // add digit to resultLabel
                        resultLabel.Text += digit;
                        // set fractionInput equal to appendageCarry minus appendage
                        fractionInput = appendageCarry - appendage;
                        // increment loopCounter
                        loopCounter++;
                    }
                }
            }
        }

        // calculate the digit character of a given decimal value
        private char DetermineDigit(long dividend, List<char> targetNumeralSystem)
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
    }
}