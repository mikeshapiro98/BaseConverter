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
            try
            {
                if (num1TextBox.Text.Trim(' ').Length == 0 || num2TextBox.Text.Trim(' ').Length == 0)
                {
                    throw new NothingShallComeFromNothingException();
                }
                else
                {
                    // clean up and set up
                    calculationLabel.Text = "";

                    // initialize masterNumeralSystem list
                    List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

                    // create num1NumeralSystem, num2NumeralSystem, and targetNumeralSystem lists from user selections
                    int num1Base = Convert.ToInt32(num1DropDownList.SelectedValue);
                    List<char> num1NumeralSystem = masterNumeralSystem.Take(num1Base).ToList();
                    string num1NumeralSystemName = num1DropDownList.SelectedItem.Text;

                    int num2Base = Convert.ToInt32(num2DropDownList.SelectedValue);
                    List<char> num2NumeralSystem = masterNumeralSystem.Take(num2Base).ToList();
                    string num2NumeralSystemName = num2DropDownList.SelectedItem.Text;

                    int targetBase = Convert.ToInt32(targetDropDownList.SelectedValue);
                    List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

                    // read user input
                    string num1 = num1TextBox.Text;
                    if (num1[0] == '-')
                    {
                        num1 = num1.TrimStart('-');
                    }
                    num1 = num1.TrimStart(' ', '0');
                    num1 = num1.Length > 0 ? num1 : "0";
                    char[] num1Array = num1.ToCharArray();

                    string num2 = num2TextBox.Text;
                    if (num2[0] == '-')
                    {
                        num2 = num2.TrimStart('-');
                    }
                    num2 = num2.TrimStart(' ', '0');
                    num2 = num2.Length > 0 ? num2 : "0";
                    char[] num2Array = num2.ToCharArray();

                    var places = Convert.ToInt32(placesTextBox.Text);

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

                    //// validate user num1
                    //if (HelperMethods.ValidateInput(num1NumeralSystem, num1Array, calculationLabel.Text, num1NumeralSystemName, num1Base, targetBase, num1, placesTextBox.Text, num1)
                    //    && HelperMethods.ValidateInput(num2NumeralSystem, num2Array, calculationLabel.Text, num2NumeralSystemName, num2Base, targetBase, num2, placesTextBox.Text, num2))
                    //{
                    //    // convert user num1 to decimal
                    //    double decimalNum1 = HelperMethods.ConvertInputToDecimal(num1Array, num1Base, masterNumeralSystem, calculationLabel.Text, places);
                    //    double decimalNum2 = HelperMethods.ConvertInputToDecimal(num2Array, num2Base, masterNumeralSystem, calculationLabel.Text, places);

                    //    //do math
                    //    double decimalTarget = HelperMethods.Calculate(decimalNum1, decimalNum2, operation);
                    //    string targetString = decimalTarget.ToString(Formatter.Notation);
                    //    // convert result from decimal to target base
                    //    string targetOutput = HelperMethods.ConvertDecimalToTarget(targetString, decimalTarget, masterNumeralSystem, targetNumeralSystem, targetBase, places);

                    //    // display result
                    //    calculationLabel.Text = HelperMethods.FormatCalculationForDisplay(num1, num1Base, operation, num2, num2Base, targetOutput, targetBase);
                    //}
                }
            }
            // exception handling
            catch (NothingShallComeFromNothingException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>Would you please fill out all required fields?</span>";
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
                calculationLabel.Text = "<span style='color:#B33A3A;'>There is no 0 in the Base 1 (Unary) system.</span>";
            }
            catch (DivideByZeroException)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>You cannot divide by 0.</span>";
            }
            catch (Exception ex)
            {
                calculationLabel.Text = "<span style='color:#B33A3A;'>The following error has occurred: " + ex.Message + "</span>";
            }
        }
    }
}