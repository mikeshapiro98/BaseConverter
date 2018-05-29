using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BaseXToBaseY.Exceptions;
using BaseConverter.Domain;

namespace BaseXToBaseY
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void convertButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputTextBox.Text.Trim(' ').Length == 0)
                {
                    throw new NothingShallComeFromNothingException();
                }
                else if (inputTextBox.Text.Trim(' ') == ".")
                {
                    throw new IsNaNException();
                }
                else
                {
                    // clear resultLabel
                    resultLabel.Text = "";

                    // instantiate a new Number object
                    Number number = new Number();

                    // create origin and target numeral system lists from user selections
                    number.originBase = Convert.ToInt16(originDropDownList.SelectedValue);
                    number.originNumeralSystem = Number.MasterNumeralSystem.Take(number.originBase).ToList();
                    number.originNumeralSystemName = originDropDownList.SelectedItem.Text;

                    number.targetBase = Convert.ToInt16(targetDropDownList.SelectedValue);
                    number.targetNumeralSystem = Number.MasterNumeralSystem.Take(number.targetBase).ToList();

                    // prepare user input for use
                    number.input = inputTextBox.Text;

                    number.inputNegative = false;
                    if (number.input[0] == '-')
                    {
                        number.input = number.input.TrimStart('-');
                        number.inputNegative = true;
                    }

                    number.input = number.input.TrimStart(' ','0');

                    if (number.input.Contains('.'))
                    {
                        number.input = number.input.TrimEnd('0');
                    }

                    if (number.input == "." || number.input.Length == 0)
                    {
                        number.input = "0";
                    }

                    number.inputArray = number.input.ToCharArray();

                    // validate user input
                    if (Validator.ValidateInput(number))
                    {
                        // avoid rounding embarrassment
                        if (number.originBase == number.targetBase)
                        {
                            number.targetResult = number.input;
                        }
                        else
                        {
                            // convert input to decimal
                            number.inputAsDecimal = Converter.ConvertInputToDecimal(number, Number.MasterNumeralSystem);

                            // prepare inputAsDecimal for use, preventing scientific notation
                            number.inputAsDecimalString = number.inputAsDecimal.ToString(Formatter.Notation);
                            number.inputAsDecimalArray = number.inputAsDecimalString.ToCharArray();

                            // convert decimal to target base
                            number.targetResult = Converter.ConvertDecimalToTarget(number);
                        }
                        // display results
                        resultLabel.Text = Formatter.FormatConversionForDisplay(number);
                    }
                }
            }
            // exception handling
            catch (NothingShallComeFromNothingException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Please fill out all required fields</span>";
            }
            catch (IsNaNException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Please enter a valid number</span>";
            }
            catch (OriginNumeralSystemLacksCharacterException ex)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Please only enter characters that exist in the " + ex.NumeralSystemName + " numeral system</span>";
            }
            catch (TooManyPeriodsException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Please do not enter multiple periods</span>";
            }
            catch (NoDogsOnTheMoonException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>The Base 1 (Unary) numeral system lacks fractions and the digit 0</span>";
            }
            catch (OverflowException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Input exceeds the maximum value of the C# long data type and cannot be processed</span>";
            }
            catch (Exception ex)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>The following error has occurred: " + ex.Message + "</span>";
            }
        }
    }
}