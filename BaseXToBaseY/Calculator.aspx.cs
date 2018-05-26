using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BaseXToBaseY.Exceptions;
using BaseConverter.Domain;

namespace BaseXToBaseY
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void equalsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (num1TextBox.Text.Trim(' ').Length == 0 || num2TextBox.Text.Trim(' ').Length == 0)
                {
                    throw new NothingShallComeFromNothingException();
                }
                else if (num1TextBox.Text.Trim(' ') == "." || num2TextBox.Text.Trim(' ') == ".")
                {
                    throw new IsNaNException();
                }
                else
                {
                    // clear calculationLabel
                    calculationLabel.Text = "";

                    // instantiate three Number objects
                    Number number1 = new Number();
                    Number number2 = new Number();
                    Number targetNumber = new Number();

                    // initialize master numeral system list, create num1, num2, and target numeral system lists from user selections
                    List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

                    number1.originBase = Convert.ToInt16(num1DropDownList.SelectedValue);
                    number1.originNumeralSystem = masterNumeralSystem.Take(number1.originBase).ToList();
                    number1.originNumeralSystemName = num1DropDownList.SelectedItem.Text;

                    number2.originBase = Convert.ToInt16(num2DropDownList.SelectedValue);
                    number2.originNumeralSystem = masterNumeralSystem.Take(number2.originBase).ToList();
                    number2.originNumeralSystemName = num2DropDownList.SelectedItem.Text;

                    targetNumber.originBase = 10;
                    targetNumber.originNumeralSystem = masterNumeralSystem.Take(10).ToList();
                    targetNumber.targetBase = Convert.ToInt16(targetDropDownList.SelectedValue);
                    targetNumber.targetNumeralSystem = masterNumeralSystem.Take(targetNumber.targetBase).ToList();

                    // prepare user inputs for use
                    number1.input = num1TextBox.Text;
                    number2.input = num2TextBox.Text;

                    char operation;
                    if (additionRadioButton.Checked)
                    {
                        operation = '+';
                    }
                    else if (subtractionRadioButton.Checked)
                    {
                        operation = '-';
                    }
                    else if (multiplicationRadioButton.Checked)
                    {
                        operation = '*';
                    }
                    else
                    {
                        operation = '/';
                    }

                    number1.inputNegative = false;
                    number2.inputNegative = false;
                    if (number1.input[0] == '-')
                    {
                        number1.input = number1.input.TrimStart('-');
                        number1.inputNegative = true;
                    }
                    if (number2.input[0] == '-')
                    {
                        number2.input = number2.input.TrimStart('-');
                        number2.inputNegative = true;
                    }

                    number1.input = number1.input.TrimStart(' ', '0');
                    number2.input = number2.input.TrimStart(' ', '0');

                    if (number1.input.Contains('.'))
                    {
                        number1.input = number1.input.TrimEnd('0');
                    }
                    if (number2.input.Contains('.'))
                    {
                        number2.input = number2.input.TrimEnd('0');
                    }

                    if (number1.input == "." || number1.input.Length == 0)
                    {
                        number1.input = "0";
                    }
                    if (number2.input == "." || number2.input.Length == 0)
                    {
                        number2.input = "0";
                    }

                    number1.inputArray = number1.input.ToCharArray();
                    number2.inputArray = number2.input.ToCharArray();

                    // validate user input
                    if (Validator.ValidateInput(number1)
                        && Validator.ValidateInput(number2))
                    {
                        // convert input to decimal
                        number1.inputAsDecimal = Converter.ConvertInputToDecimal(number1, masterNumeralSystem);
                        number2.inputAsDecimal = Converter.ConvertInputToDecimal(number2, masterNumeralSystem);

                        // prepare inputAsDecimal for use, preventing scientific notation
                        number1.inputAsDecimalString = number1.inputAsDecimal.ToString(Formatter.Notation);
                        number1.inputAsDecimalArray = number1.inputAsDecimalString.ToCharArray();

                        number2.inputAsDecimalString = number2.inputAsDecimal.ToString(Formatter.Notation);
                        number2.inputAsDecimalArray = number2.inputAsDecimalString.ToCharArray();

                        // do math in decimal
                        targetNumber.inputAsDecimal = DecimalCalculator.Calculate(number1, number2, operation);

                        // handle negative
                        targetNumber.inputNegative = false;
                        if (targetNumber.inputAsDecimal < 0)
                        {
                            targetNumber.inputNegative = true;
                            targetNumber.inputAsDecimal = targetNumber.inputAsDecimal * -1;
                        }

                        // prep calculationResult for use
                        targetNumber.inputAsDecimalString = targetNumber.inputAsDecimal.ToString(Formatter.Notation);
                        targetNumber.inputAsDecimalArray = targetNumber.inputAsDecimalString.ToCharArray();

                        // convert math result to target base
                        targetNumber.targetResult = Converter.ConvertDecimalToTarget(targetNumber);

                        // display results
                        calculationLabel.Text = Formatter.FormatCalculationForDisplay(number1, number2, targetNumber, operation);

                        // reset the board
                        num1TextBox.Text = "";
                        if (targetNumber.inputNegative)
                        {
                            num1TextBox.Text = "-";
                        }
                        num1TextBox.Text += targetNumber.targetResult;
                        num1DropDownList.SelectedValue = targetDropDownList.SelectedValue;
                        num2TextBox.Text = "";
                        num2DropDownList.SelectedValue = targetDropDownList.SelectedValue;
                    }
                }
            }
            // exception handling
            catch (NothingShallComeFromNothingException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Please fill out all required fields</span>";
            }
            catch (IsNaNException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Please enter a valid number</span>";
            }
            catch (OriginNumeralSystemLacksCharacterException ex)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Please only enter characters that exist in the " + ex.NumeralSystemName + " numeral system</span>";
            }
            catch (TooManyPeriodsException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Please do not enter multiple periods</span>";
            }
            catch (NoDogsOnTheMoonException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>The Base 1 (Unary) numeral system lacks fractions and the digit 0</span>";
            }
            catch (OverflowException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Input exceeds the maximum value of the C# long data type and cannot be processed</span>";
            }
            catch (Exception ex)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>The following error has occurred: " + ex.Message + "</span>";
            }
        }
    }
}