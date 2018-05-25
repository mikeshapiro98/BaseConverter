using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConverter.Domain
{
    public class DecimalCalculator
    {
        // add, subtract, multiply, or divide
        public static double Calculate(double decimalNum1, bool num1Negative, double decimalNum2, bool num2Negative, char operation)
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
    }
}
