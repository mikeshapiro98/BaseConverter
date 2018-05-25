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
