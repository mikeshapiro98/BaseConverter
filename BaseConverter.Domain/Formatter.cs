using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter.Domain
{
    public class Formatter
    {
        public const string Notation = ".####################################################################################################################################################################################################################################################################################################################################";

        public static string FormatConversionForDisplay(Number number)
        {
            if (number.input[0] == '.')
            {
                number.input.Insert(0, "0");
            }

            if (number.targetResult[0] == '.')
            {
                number.targetResult.Insert(0, "0");
            }

            //handle negatives
            if (number.inputNegative)
            {
                number.input = number.input.Insert(0, "-");
                number.targetResult = number.targetResult.Insert(0, "-");
            }

            // format results for display
            string formattedResult = String.Format("{0}<sub>{1}</sub> = {2}<sub>{3}</sub>",
                number.input,
                number.originBase,
                number.targetResult,
                number.targetBase);

            return formattedResult;
        }

        // display calculation
        public static string FormatCalculationForDisplay(Number number1, Number number2, Number targetNumber, char operation)
        {
            if (number1.input[0] == '.')
            {
                number1.input.Insert(0, "0");
            }
            if (number2.input[0] == '.')
            {
                number2.input.Insert(0, "0");
            }
            if (targetNumber.targetResult[0] == '.')
            {
                targetNumber.targetResult.Insert(0, "0");
            }

            //handle negatives
            if (number1.inputNegative)
            {
                number1.input = number1.input.Insert(0, "-");
            }
            if (number2.inputNegative)
            {
                number2.input = number2.input.Insert(0, "-");
            }
            if (targetNumber.inputNegative)
            {
                targetNumber.targetResult = targetNumber.targetResult.Insert(0, "-");
            }

            // format results for display
            string result = String.Format("{0}<sub>{1}</sub> {2} {3}<sub>{4}</sub> = {5}<sub>{6}</sub>",
                number1.input,
                number1.originBase,
                operation,
                number2.input,
                number2.originBase,
                targetNumber.targetResult,
                targetNumber.targetBase);
            return result;
        }
    }
}
