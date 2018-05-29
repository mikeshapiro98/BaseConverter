using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter.Domain
{
    public class Converter
    {
        public static double ConvertInputToDecimal(Number number)
        {
            double inputAsDecimal;
            // if input contains no fractional part
            if (!number.inputArray.Contains('.'))
            {
                // assign decimal value of integer to result
                inputAsDecimal = CalculateDecimalValueOfInteger(number.inputArray, number.originBase);
            }
            // if input contains a fractional part
            else
            {
                // split array on the period
                char[] integerArray = number.inputArray.TakeWhile(x => x != '.').ToArray();
                char[] fractionArray = number.inputArray.SkipWhile(x => x != '.').ToArray();
                fractionArray = fractionArray.Skip(1).ToArray();

                // assign decimal value of integer part to result
                inputAsDecimal = CalculateDecimalValueOfInteger(integerArray, number.originBase);

                // assign decimal value of integer part + fractional part to result
                double fraction = CalculateDecimalValueOfFraction(fractionArray, number.originBase);
                inputAsDecimal += fraction;
            }
            // return decimal value of input
            return inputAsDecimal;
        }

        // calculate the decimal value of an integer
        private static double CalculateDecimalValueOfInteger(char[] inputArray, int originBase)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = 0;
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                double digitValue = CalculateDigitValue(digit);
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a fraction
        private static double CalculateDecimalValueOfFraction(char[] inputArray, int originBase)
        {
            // initialize decimal value and counter variables
            double decimalValue = 0;
            var placeCounter = -(inputArray.Length);
            // calculate the value of each digit at its place in the input
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                var digit = inputArray[i];
                // calculate digit value and convert it to a double in order to multiply it by place value
                double digitValue = CalculateDigitValue(digit);
                // increase decimal value by the value of the digit times the value of the place
                decimalValue += (digitValue * CalculatePlaceValue(originBase, placeCounter));
                placeCounter++;
            }
            return decimalValue;
        }

        // calculate the decimal value of a digit
        private static double CalculateDigitValue(char digit)
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
            // retrieve the decimal value of the digit from its index in the MasterNumeralSystem
            else
            {
                digitValue = Number.MasterNumeralSystem.IndexOf(digit);
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

        public static string ConvertDecimalToTarget(Number number)
        {
            string targetResult = "";
            long decimalInteger;
            if (number.inputAsDecimal == 0)
            {
                targetResult = "0";
            }
            // if input does not contain a fractional part
            else if (!number.inputAsDecimalArray.Contains('.'))
            {
                // convert inputAsDecimal to int, calculate its new value in targetNumeralSystem, assign that value to targetResult
                decimalInteger = Convert.ToInt64(number.inputAsDecimal);
                targetResult = CalculateBaseXIntegerValue(number.targetBase, decimalInteger);
            }
            // if input contains a fractional part
            else
            {
                // split decimalInputArray on the period into integerArray and fractionArray
                char[] newIntegerArray = number.inputAsDecimalArray.TakeWhile(x => x != '.').ToArray();
                char[] newFractionArray = number.inputAsDecimalArray.SkipWhile(x => x != '.').ToArray();

                // convert integerArray and fractionArray into strings
                string integerString = new string(newIntegerArray);
                string fractionString = new string(newFractionArray);

                // convert integerString and fractionString into a long and a double, respectively
                if (newIntegerArray.Length == 0)
                {
                    integerString = "0";
                }
                decimalInteger = Convert.ToInt64(integerString);
                double decimalFraction = Convert.ToDouble(fractionString);

                //  calculate decimalInteger value in the targetNumeralSystem, assign that value to integerResult
                string integerResult = CalculateBaseXIntegerValue(number.targetBase, decimalInteger);

                // calculate fractionInput value in the targetNumeralSystem, assign that value to fractionResult
                string fractionResult = CalculateBaseXFractionValue(number.targetBase, decimalFraction);

                // assign integerResult.fractionResult to targetResult
                targetResult = integerResult + '.' + fractionResult;
            }
            return targetResult;
        }

        private static string CalculateBaseXIntegerValue(int targetBase, long decimalInteger)
        {
            string integerResult = "";
            // edge case: if targetBase is unary
            if (targetBase == 1)
            {
                for (int i = 0; i < decimalInteger; i++)
                {
                    // append a number of tally marks equal to decimalInteger to resultLabel
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
                        long dividend = decimalInteger / Convert.ToInt64(placeValue);

                        // determine digit equal to dividend in targetNumeralSystem
                        char digit = DetermineDigit(dividend);

                        // append digit to integerResult
                        integerResult += digit.ToString();

                        // subtract placeValue from intInput
                        decimalInteger -= (Convert.ToInt64(placeValue) * dividend);
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

        private static string CalculateBaseXFractionValue(int targetBase, double fractionInput)
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

                // assign integer part of appendageCarry to long appendage
                long appendage = Convert.ToInt64(Math.Floor(appendageCarry));

                // determine digit equal to appendage in targetNumeralSystem
                char digit = DetermineDigit(appendage);

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
        private static char DetermineDigit(long dividend)
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
                digitCharacter = Number.MasterNumeralSystem.ElementAt(Convert.ToInt32(dividend));
            }
            return digitCharacter;
        }
    }
}
