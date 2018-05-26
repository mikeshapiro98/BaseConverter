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
        public static double Calculate(Number number1, Number number2, char operation)
        {
            if (number1.inputNegative)
            {
                number1.inputAsDecimal = number1.inputAsDecimal * -1;
            }
            if (number2.inputNegative)
            {
                number2.inputAsDecimal = number2.inputAsDecimal * -1;
            }

            double decimalTarget;
            switch (operation)
            {
                case '+':
                    decimalTarget = number1.inputAsDecimal + number2.inputAsDecimal;
                    break;
                case '-':
                    decimalTarget = number1.inputAsDecimal - number2.inputAsDecimal;
                    break;
                case '*':
                    decimalTarget = number1.inputAsDecimal * number2.inputAsDecimal;
                    break;
                default:
                    decimalTarget = number1.inputAsDecimal / number2.inputAsDecimal;
                    break;
            }
            return decimalTarget;
        }
    }
}
