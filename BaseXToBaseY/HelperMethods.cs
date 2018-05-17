using System;
using System.Collections.Generic;
using System.Linq;
using BaseXToBaseY.Exceptions;

namespace BaseXToBaseY
{
    public static class HelperMethods
    {

        // prevent faulty user input
        public static bool ValidateInput(List<char> originNumeralSystem, char[] inputArray, string result, string originNumeralSystemName, int originBase, int targetBase, string input, string placesText)
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
                    throw new TargetNumeralSystemLacksCharacterException(originNumeralSystemName);
                }
            }
            // throw exceptions if there are multiple minuses or periods
            if (minusCounter > 1)
            {
                throw new TooManyMinusesException();
            }
            if (periodCounter > 1)
            {
                throw new TooManyPeriodsException();
            }
            // throw exceptions if there's an inappropriate minus
            if (minusCounter == 1 && inputArray[0] != '-')
            {
                throw new MisplacedMinusException();
            }
            // throw exception for 0 in unary
            if (inputArray.Length == 1 && inputArray[0] == '0' && targetBase == 1)
            {
                throw new NoDogsOnTheMoonException();
            }
            // if input == 0
            if (input.Equals(0))
            {
                throw new FormatZeroException(originBase, targetBase);
            }
            // if places is not an int
            bool precise = Int32.TryParse(placesText, out int places);
            if (!precise)
            {
                throw new InvalidPlacesException();
            }
            return true;
        }

        public static double ConvertInputToDecimal(char[] inputArray, int originBase, List<char> masterNumeralSystem, string result, int places)
        {
            result = "";
            // split array on the period
            if (inputArray.Contains('.'))
            {
                char[] integerArray = inputArray.TakeWhile(x => x != '.').ToArray();
                char[] fractionArray = inputArray.SkipWhile(x => x != '.').ToArray();
                fractionArray = fractionArray.Skip(1).ToArray();
                // append decimal value of integer and decimal value of fraction to resultLabel
                result += CalculateDecimalValueOfInteger(integerArray, originBase, masterNumeralSystem, places).ToString();
                // remove leading 0. from fraction
                string fraction = CalculateDecimalValueOfFraction(fractionArray, originBase, masterNumeralSystem, places);
                if (fraction.Length > 0)
                {
                    fraction = fraction.TrimEnd('0');
                    result += fraction;
                }
            }
            else
            {
                result += CalculateDecimalValueOfInteger(inputArray, originBase, masterNumeralSystem, places).ToString();
            }
            // return decimal value of input
            double decimalInput = Convert.ToDouble(result);
            return decimalInput;
        }

        // calculate the decimal value of an integer
        private static double CalculateDecimalValueOfInteger(char[] inputArray, int originBase, List<char> masterNumeralSystem, int places)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = 0;
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                int digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                decimalValue += (digitValue * Convert.ToDouble(CalculatePlaceValue(originBase, placeCounter, places)));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a fraction
        private static string CalculateDecimalValueOfFraction(char[] inputArray, int originBase, List<char> masterNumeralSystem, int places)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = -(inputArray.Length);
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                double digitValue = Convert.ToDouble(CalculateDigitValue(digit, masterNumeralSystem));
                decimalValue += (digitValue * Convert.ToDouble(CalculatePlaceValue(originBase, placeCounter, places)));
                placeCounter++;
            }
            string notation = ".";
            for (int i = 0; i < places; i++)
            {
                notation += "#";
            }
            string decimalInput = decimalValue.ToString(notation);
            return decimalInput;
        }

        // calculate the decimal value of a digit
        private static int CalculateDigitValue(char digit, List<char> masterNumeralSystem)
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
        private static string CalculatePlaceValue(int givenBase, int placeCounter, int places)
        {
            // convert originBase and placeCounter to doubles to use C# Math.Pow function
            double doubleBase = Convert.ToDouble(givenBase);
            double doublePlace = Convert.ToDouble(placeCounter);
            // calculate placeValue and convert it to a long for return
            double doublePlaceValue = Math.Pow(doubleBase, doublePlace);
            string notation = ".";
            for (int i = 0; i < places; i++)
            {
                notation += "#";
            }
            string stringPlaceValue = doublePlaceValue.ToString(notation);
            return stringPlaceValue;
        }

        public static string ConvertDecimalToTarget(string result, double decimalInput, List<char> masterNumeralSystem, List<char> targetNumeralSystem, int targetBase, int places)
        {
            // split decimal value of input into a character array
            char[] decimalInputArray = result.ToCharArray();
            
            string targetResult = "";

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
                string integerResult = CalculateBaseXIntegerValue(targetBase, longDecimalInput, targetNumeralSystem, targetResult, places);
                // calculate fractionInput's value in the targetNumeralSystem, append a period and that value to resultLabel
                string fractionResult = CalculateBaseXFractionValue(targetBase, fractionDecimalInput, targetNumeralSystem, targetResult, places);
                targetResult = integerResult + '.' + fractionResult;
            }
            else
            {
                // convert decimalInput to long, calculate its new value in the targetNumeralSystem, append that value to resultLabel
                long longDecimalInput = Convert.ToInt64(decimalInput);
                targetResult += CalculateBaseXIntegerValue(targetBase, longDecimalInput, targetNumeralSystem, targetResult, places);
            }
            return targetResult;
        }

        private static string CalculateBaseXIntegerValue(int targetBase, long longInput, List<char> targetNumeralSystem, string targetResult, int places)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                for (int i = 0; i < longInput; i++)
                {
                    // append a number of tally marks equal to longInput to resultLabel
                    targetResult += '1';
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
                    placeValue = Convert.ToDouble(CalculatePlaceValue(targetBase, placeCounter, places));
                }
                // decrement placeCounter and recalculate placeValue
                placeCounter--;
                placeValue = Convert.ToDouble(CalculatePlaceValue(targetBase, placeCounter, places));
                // convert longInput to targetBase and append converted number, character by character, to resultLabel
                while (placeCounter >= 0)
                {
                    if (longInput >= placeValue)
                    {
                        // calculate dividend
                        long dividend = longInput / Convert.ToInt64(placeValue);
                        // determine digit equal to dividend in targetNumeralSystem
                        char digit = DetermineDigit(dividend, targetNumeralSystem);
                        // append digit to resultLabel
                        targetResult += digit.ToString();
                        // subtract placeValue from intInput
                        longInput -= (Convert.ToInt64(placeValue) * dividend);
                    }
                    else
                    {
                        targetResult += '0';
                    }
                    // decrement placeCounter and recalculate placeValue
                    placeCounter--;
                    placeValue = Convert.ToDouble(CalculatePlaceValue(targetBase, placeCounter, places));
                }
            }
            return targetResult;
        }

        private static string CalculateBaseXFractionValue(int targetBase, double fractionInput, List<char> targetNumeralSystem, string targetResult, int places)
        {
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                throw new UnaryFractionException();
            }
            else
            {
                // throw exception if places is not a positive int
                if (places < 0)
                {
                    throw new InvalidPlacesException();
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
                        targetResult += digit;
                        // set fractionInput equal to appendageCarry minus appendage
                        fractionInput = appendageCarry - appendage;
                        // increment loopCounter
                        loopCounter++;
                    }
                }
            }
            return targetResult;
        }

        // calculate the digit character of a given decimal value
        private static char DetermineDigit(long dividend, List<char> targetNumeralSystem)
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

        public static string FormatResultForDisplay(string input, char[] inputArray, string result, int originBase, int targetBase, string originalInput, int places)
        {
            // final validation
            double convertedNumber;
            char[] check1Array = inputArray.SkipWhile(x => x != '.').ToArray();
            check1Array = check1Array.Skip(1).ToArray();
            char[] check2Array = check1Array.SkipWhile(x => x == '0').ToArray();
            if (input == "0")
            {
                convertedNumber = 0;
            }
            else if (originalInput.Contains('.') && check2Array.Length == 0)
            {
                input = "0";
                convertedNumber = 0;
            }
            else
            {
                convertedNumber = Convert.ToDouble(result);
            }
            //handle negatives
            if (originalInput[0] == '-' && input != "0")
            {
                input = input.Insert(0, "-");
                convertedNumber = convertedNumber * -1;
            }

            if (input.Contains('.'))
            {
                input = input.TrimEnd('0');
            }

            string notation = ".";
            for (int i = 0; i < places; i++)
            {
                notation += "#";
            }
            string convertString = convertedNumber.ToString(notation);

            // format results for display
            result = String.Format("{0}<sub>{1}</sub> = {2}<sub>{3}</sub>",
                input,
                originBase,
                convertString,
                targetBase);

            return result;
        }
    }
}