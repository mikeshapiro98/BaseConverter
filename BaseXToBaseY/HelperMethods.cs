using System;
using System.Collections.Generic;
using System.Linq;
using BaseXToBaseY.Exceptions;

namespace BaseXToBaseY
{
    public static class HelperMethods
    {

        // prevent faulty user input
        public static bool ValidateInput(char[] inputArray, List<char> originNumeralSystem, string originNumeralSystemName, string input, int targetBase, int originBase)
        {
            // initialize minus and period counters
            var periodCounter = 0;
            // track quantity of minuses and periods in input array
            foreach (var digit in inputArray)
            {
                if (digit == '.')
                {
                    periodCounter++;
                }
                // ensure every char in input array exists in origin numeral system
                else if (!originNumeralSystem.Contains(digit))
                {
                    throw new OriginNumeralSystemLacksCharacterException(originNumeralSystemName);
                }
            }
            // throw exceptions if input contains multiple periods
            if (periodCounter > 1)
            {
                throw new TooManyPeriodsException();
            }
            // unary lacks 0 and fractional numbers
            if ((input.Contains('0') && targetBase == 1) 
                || (input.Contains('.') && targetBase == 1))
            {
                throw new NoDogsOnTheMoonException();
            }

            return true;
        }

        public static double ConvertInputToDecimal(char[] inputArray, int originBase, List<char> masterNumeralSystem)
        {
            double inputAsDecimal;
            // if input contains no fractional part
            if (!inputArray.Contains('.'))
            {
                // assign decimal value of integer to result
                inputAsDecimal = CalculateDecimalValueOfInteger(inputArray, originBase, masterNumeralSystem);
            }
            // if input contains a fractional part
            else
            {
                // split array on the period
                char[] integerArray = inputArray.TakeWhile(x => x != '.').ToArray();
                char[] fractionArray = inputArray.SkipWhile(x => x != '.').ToArray();
                fractionArray = fractionArray.Skip(1).ToArray();

                // assign decimal value of integer part to result
                inputAsDecimal = CalculateDecimalValueOfInteger(integerArray, originBase, masterNumeralSystem);

                // assign decimal value of integer part + fractional part to result
                double fraction = CalculateDecimalValueOfFraction(fractionArray, originBase, masterNumeralSystem);
                inputAsDecimal += fraction;
            }
            // return decimal value of input
            return inputAsDecimal;
        }

        // calculate the decimal value of an integer
        private static double CalculateDecimalValueOfInteger(char[] inputArray, int originBase, List<char> masterNumeralSystem)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = 0;
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                double digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a fraction
        private static double CalculateDecimalValueOfFraction(char[] inputArray, int originBase, List<char> masterNumeralSystem)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = -(inputArray.Length);
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                // calculate digit value and convert it to a double in order to multiply it by place value
                double digitValue = CalculateDigitValue(digit, masterNumeralSystem);
                // increase decimal value by the value of the digit times the value of the place
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a digit
        private static double CalculateDigitValue(char digit, List<char> masterNumeralSystem)
        {
            // initialize digit value
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
            // retrieve the decimal value of the digit from its index in the masterNumeralSystem list
            else
            {
                digitValue = masterNumeralSystem.IndexOf(digit);
            }
            return digitValue;
        }

        // calculate the decimal value of a given place by raising the base to the power of that place
        private static double CalculatePlaceValue(int givenBase, int placeCounter)
        {
            // convert originBase and placeCounter to doubles to use C# Math.Pow function
            double doubleBase = Convert.ToDouble(givenBase);
            double doublePlace = Convert.ToDouble(placeCounter);
            // calculate placeValue
            double placeValue = Math.Pow(doubleBase, doublePlace);
            return placeValue;
        }

        public static string ConvertDecimalToTarget(char[] inputAsDecimalArray, double inputAsDecimal, List<char> targetNumeralSystem, int targetBase)
        {
            string targetResult = "";
            int decimalInteger;
            if (inputAsDecimal == 0)
            {
                targetResult = "0";
            }
            // if input does not contain a fractional part
            else if (!inputAsDecimalArray.Contains('.'))
            {
                // convert inputAsDecimal to int, calculate its new value in targetNumeralSystem, assign that value to targetResult
                decimalInteger = Convert.ToInt32(inputAsDecimal);
                targetResult = CalculateBaseXIntegerValue(targetBase, decimalInteger, targetNumeralSystem);
            }
            // if input contains a fractional part
            else
            {
                // split decimalInputArray on the period into integerArray and fractionArray
                char[] newIntegerArray = inputAsDecimalArray.TakeWhile(x => x != '.').ToArray();
                char[] newFractionArray = inputAsDecimalArray.SkipWhile(x => x != '.').ToArray();

                // convert integerArray and fractionArray into strings
                string integerString = new string(newIntegerArray);
                string fractionString = new string(newFractionArray);

                // convert integerString and fractionString into a long and a double, respectively
                if (newIntegerArray.Length == 0)
                {
                    integerString = "0";
                }
                decimalInteger = Convert.ToInt32(integerString);
                double decimalFraction = Convert.ToDouble(fractionString);

                //  calculate decimalInteger value in the targetNumeralSystem, assign that value to integerResult
                string integerResult = CalculateBaseXIntegerValue(targetBase, decimalInteger, targetNumeralSystem);

                // calculate fractionInput value in the targetNumeralSystem, assign that value to fractionResult
                string fractionResult = CalculateBaseXFractionValue(targetBase, decimalFraction, targetNumeralSystem);

                // assign integerResult.fractionResult to targetResult
                targetResult = integerResult + '.' + fractionResult;
            }
            return targetResult;
        }

        private static string CalculateBaseXIntegerValue(int targetBase, int decimalInteger, List<char> targetNumeralSystem)
        {
            string integerResult = "";
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                for (int i = 0; i < decimalInteger; i++)
                {
                    // append a number of tally marks equal to longInput to resultLabel
                    integerResult += '1';
                }
            }
            else
            {
                // initialize placeCounter and placeValue variables
                int placeCounter = 0;
                double placeValue = 1;

                // increment placeCounter and recalculate placeValue until placeValue > intInput
                while (decimalInteger >= placeValue)
                {
                    placeCounter++;
                    placeValue = CalculatePlaceValue(targetBase, placeCounter);
                }

                // decrement placeCounter and recalculate placeValue
                placeCounter--;
                placeValue = CalculatePlaceValue(targetBase, placeCounter);

                // convert decimalInteger to targetBase and append converted number, character by character, to integerResult
                while (placeCounter >= 0)
                {
                    if (decimalInteger >= placeValue)
                    {
                        // calculate dividend
                        int dividend = decimalInteger / Convert.ToInt32(placeValue);

                        // determine digit equal to dividend in targetNumeralSystem
                        char digit = DetermineDigit(dividend, targetNumeralSystem);

                        // append digit to integerResult
                        integerResult += digit.ToString();

                        // subtract placeValue from intInput
                        decimalInteger -= (Convert.ToInt32(placeValue) * dividend);
                    }
                    else
                    {
                        integerResult += '0';
                    }
                    // decrement placeCounter and recalculate placeValue
                    placeCounter--;
                    placeValue = CalculatePlaceValue(targetBase, placeCounter);
                }
            }
            return integerResult;
        }

        private static string CalculateBaseXFractionValue(int targetBase, double fractionInput, List<char> targetNumeralSystem)
        {
            string fractionResult = "";
            
            // convert targetBase to a double to use in multiplication
            double doubleBase = Convert.ToDouble(targetBase);

            // initialize loopCounter
            int loopCounter = 0;

            // set while loop to terminate when fractionInput has been fully converted, or when max # of places is reached
            while (fractionInput > 0 && loopCounter <= 324)
            {
                // multiply fractionInput by targetBase, assign to double appendageCarry
                double appendageCarry = fractionInput * targetBase;

                // assign integer part of appendageCarry to int appendage
                int appendage = Convert.ToInt32(Math.Floor(appendageCarry));

                // determine digit equal to appendage in targetNumeralSystem
                char digit = DetermineDigit(appendage, targetNumeralSystem);

                // append digit to fractionResult
                fractionResult += digit;

                // set fractionInput equal to appendageCarry minus appendage
                fractionInput = appendageCarry - appendage;

                // increment loopCounter
                loopCounter++;
            }
            return fractionResult.TrimEnd('0');
        }

        // calculate the digit character of a given decimal value
        private static char DetermineDigit(int dividend, List<char> targetNumeralSystem)
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
                digitCharacter = targetNumeralSystem.ElementAt(dividend);
            }
            return digitCharacter;
        }

        public static string FormatConversionForDisplay(bool inputNegative, string input, string targetResult, int originBase, int targetBase)
        {
            if (input[0] == '.')
            {
                input.Insert(0, "0");
            }

            if (targetResult[0] == '.')
            {
                targetResult.Insert(0, "0");
            }

            //handle negatives
            if (inputNegative)
            {
                input = input.Insert(0, "-");
                targetResult = targetResult.Insert(0, "-");
            }

            // format results for display
            string formattedResult = String.Format("{0}<sub>{1}</sub> = {2}<sub>{3}</sub>",
                input,
                originBase,
                targetResult,
                targetBase);

            return formattedResult;
        }

        // add, subtract, multiply, or divide
        internal static double Calculate(double decimalNum1, bool num1Negative, double decimalNum2, bool num2Negative, char operation)
        {
            if (num1Negative)
            {
                decimalNum1 = decimalNum1 * -1;
            }
            if (num2Negative)
            {
                decimalNum2 = decimalNum2 * -1;
            }

            double decimalTarget;
            switch (operation)
            {
                case '+':
                    decimalTarget = decimalNum1 + decimalNum2;
                    break;
                case '-':
                    decimalTarget = decimalNum1 - decimalNum2;
                    break;
                case '*':
                    decimalTarget = decimalNum1 * decimalNum2;
                    break;
                default:
                    decimalTarget = decimalNum1 / decimalNum2;
                    break;
            }
            return decimalTarget;
        }

        // display calculation
        public static string FormatCalculationForDisplay(string num1, string num2, string targetResult, bool num1Negative, bool num2Negative, bool resultNegative, char operation, int num1Base, int num2Base, int targetBase)
        {
            if (num1[0] == '.')
            {
                num1.Insert(0, "0");
            }
            if (num2[0] == '.')
            {
                num2.Insert(0, "0");
            }
            if (targetResult[0] == '.')
            {
                targetResult.Insert(0, "0");
            }

            //handle negatives
            if (num1Negative)
            {
                num1 = num1.Insert(0, "-");
            }
            if (num2Negative)
            {
                num2 = num2.Insert(0, "-");
            }
            if (resultNegative)
            {
                targetResult = targetResult.Insert(0, "-");
            }

            // format results for display
            string result = String.Format("{0}<sub>{1}</sub> {2} {3}<sub>{4}</sub> = {5}<sub>{6}</sub>",
                num1,
                num1Base,
                operation,
                num2,
                num2Base,
                targetResult,
                targetBase);
            return result;
        }
    }
}