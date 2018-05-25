using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseXToBaseY.Exceptions;

namespace BaseConverter.Domain
{
    public class Validator
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
    }
}
