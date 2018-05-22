using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BaseXToBaseY.Exceptions;

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

                    // initialize master numeral system list, create origin and target numeral system lists from user selections
                    List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

                    int originBase = Convert.ToInt32(originDropDownList.SelectedValue);
                    List<char> originNumeralSystem = masterNumeralSystem.Take(originBase).ToList();
                    string originNumeralSystemName = originDropDownList.SelectedItem.Text;

                    int targetBase = Convert.ToInt32(targetDropDownList.SelectedValue);
                    List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

                    // prepare user input for use
                    string input = inputTextBox.Text;

                    // strip minus sign from input
                    bool inputNegative = false;
                    if (input[0] == '-')
                    {
                        input = input.TrimStart('-');
                        inputNegative = true;
                    }

                    input = input.TrimStart(' ','0');

                    if (input.Contains('.'))
                    {
                        input = input.TrimEnd('0');
                    }

                    if (input == "." || input.Length == 0)
                    {
                        input = "0";
                    }
                    
                    char[] inputArray = input.ToCharArray();

                    // validate user input
                    if (HelperMethods.ValidateInput(inputArray, originNumeralSystem, originNumeralSystemName, input, targetBase, originBase))
                    {
                        // convert input to decimal
                        double inputAsDecimal = HelperMethods.ConvertInputToDecimal(inputArray, originBase, masterNumeralSystem);

                        // prepare inputAsDecimal for use, preventing scientific notation
                        string inputAsDecimalString = inputAsDecimal.ToString(Formatter.Notation);
                        char[] inputAsDecimalArray = inputAsDecimalString.ToCharArray();

                        // convert decimal to target base
                        string targetResult = HelperMethods.ConvertDecimalToTarget(inputAsDecimalArray, inputAsDecimal, targetNumeralSystem, targetBase);
                        
                        // display results
                        resultLabel.Text = HelperMethods.FormatConversionForDisplay(inputNegative, input, targetResult, originBase, targetBase);
                    }
                }
            }
            // exception handling
            catch (NothingShallComeFromNothingException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please fill out all required fields?</span>";
            }
            catch (IsNaNException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please enter a valid number?</span>";
            }
            catch (OriginNumeralSystemLacksCharacterException ex)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please only enter characters that exist in the " + ex.NumeralSystemName + " number system?</span>";
            }
            catch (TooManyPeriodsException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple periods?</span>";
            }
            catch (NoDogsOnTheMoonException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>There is no 0 and no fractional values in the Base 1 (Unary) system.</span>";
            }
            catch (Exception ex)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>The following error has occurred: " + ex.Message + "</span>";
            }
        }
    }
}