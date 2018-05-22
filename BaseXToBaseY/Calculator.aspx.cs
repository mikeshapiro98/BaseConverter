using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BaseXToBaseY.Exceptions;

namespace BaseXToBaseY
{
	public partial class Calculator : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void equalsButton_Click(object sender, EventArgs e)
        {
            try { 
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

                    // initialize master numeral system list, create num1, num2, and target numeral system lists from user selections
                    List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

                    int num1Base = Convert.ToInt32(num1DropDownList.SelectedValue);
                    List<char> num1NumeralSystem = masterNumeralSystem.Take(num1Base).ToList();
                    string num1NumeralSystemName = num1DropDownList.SelectedItem.Text;

                    int num2Base = Convert.ToInt32(num2DropDownList.SelectedValue);
                    List<char> num2NumeralSystem = masterNumeralSystem.Take(num2Base).ToList();
                    string num2NumeralSystemName = num2DropDownList.SelectedItem.Text;

                    int targetBase = Convert.ToInt32(targetDropDownList.SelectedValue);
                    List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

                    // prepare user inputs for use
                    string num1 = num1TextBox.Text;
                    string num2 = num2TextBox.Text;

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

                    bool num1Negative = false;
                    bool num2Negative = false;
                    if (num1[0] == '-')
                    {
                        num1 = num1.TrimStart('-');
                        num1Negative = true;
                    }
                    if (num2[0] == '-')
                    {
                        num2 = num2.TrimStart('-');
                        num2Negative = true;
                    }

                    num1 = num1.TrimStart(' ', '0');
                    num2 = num2.TrimStart(' ', '0');

                    if (num1.Contains('.'))
                    {
                        num1 = num1.TrimEnd('0');
                    }
                    if (num2.Contains('.'))
                    {
                        num2 = num2.TrimEnd('0');
                    }

                    if (num1 == "." || num1.Length == 0)
                    {
                        num1 = "0";
                    }
                    if (num2 == "." || num2.Length == 0)
                    {
                        num2 = "0";
                    }

                    char[] num1Array = num1.ToCharArray();
                    char[] num2Array = num2.ToCharArray();

                    // validate user input
                    if (HelperMethods.ValidateInput(num1Array, num1NumeralSystem, num1NumeralSystemName, num1, targetBase, num1Base)
                        && HelperMethods.ValidateInput(num2Array, num2NumeralSystem, num2NumeralSystemName, num2, targetBase, num2Base))
                    {
                        // convert input to decimal
                        double num1AsDecimal = HelperMethods.ConvertInputToDecimal(num1Array, num1Base, masterNumeralSystem);
                        double num2AsDecimal = HelperMethods.ConvertInputToDecimal(num2Array, num2Base, masterNumeralSystem);

                        // prepare inputAsDecimal for use, preventing scientific notation
                        string num1AsDecimalString = num1AsDecimal.ToString(Formatter.Notation);
                        char[] num1AsDecimalArray = num1AsDecimalString.ToCharArray();
                        string num2AsDecimalString = num2AsDecimal.ToString(Formatter.Notation);
                        char[] num2AsDecimalArray = num2AsDecimalString.ToCharArray();

                        // do math in decimal
                        double calculationResult = HelperMethods.Calculate(num1AsDecimal, num1Negative, num2AsDecimal, num2Negative, operation);

                        // handle negative
                        bool resultNegative = false;
                        if (calculationResult < 0)
                        {
                            resultNegative = true;
                            calculationResult = calculationResult * -1;
                        }

                        // prep calculationResult for use
                        string calculationResultAsString = calculationResult.ToString(Formatter.Notation);
                        char[] calculationResultAsDecimalArray = calculationResultAsString.ToCharArray();

                        // convert math result to target base
                        string targetResult = HelperMethods.ConvertDecimalToTarget(calculationResultAsDecimalArray, calculationResult, targetNumeralSystem, targetBase);

                        // display results
                        calculationLabel.Text = HelperMethods.FormatCalculationForDisplay(num1, num2, targetResult, num1Negative, num2Negative, resultNegative, operation, num1Base, num2Base, targetBase);
                    }
                }
            }
            // exception handling
            catch (NothingShallComeFromNothingException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Would you please fill out all required fields?</span>";
            }
            catch (IsNaNException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Would you please enter a valid number?</span>";
            }
            catch (OriginNumeralSystemLacksCharacterException ex)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Would you please only enter characters that exist in the " + ex.NumeralSystemName + " number system?</span>";
            }
            catch (TooManyPeriodsException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple periods?</span>";
            }
            catch (NoDogsOnTheMoonException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>There is no 0 and no fractional values in the Base 1 (Unary) system.</span>";
            }
            catch (Exception ex)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>The following error has occurred: " + ex.Message + "</span>";
            }
        }
    }
}