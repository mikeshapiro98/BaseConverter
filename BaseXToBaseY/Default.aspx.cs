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
                // clean up and set up
                resultLabel.Text = "";

                // initialize masterNumeralSystem list
                List<char> masterNumeralSystem = new List<char>() { '1', '0', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '/', ':', ';', '(', ')', '$', '&', '@', '"', ',', '?', '!', '\'', '[', ']', '{', '}', '#', '%', '^', '*', '+', '=', '_', '\\', '|', '~', '<', '>', '€', '£', '¥', '•', '₽', '¢', '₩', '§', '¿', '¡', 'ß' };

                // create  originNumeralSystem and targetNumeralSystem lists from user selection
                int originBase = Convert.ToInt32(originDropDownList.SelectedValue);
                List<char> originNumeralSystem = masterNumeralSystem.Take(originBase).ToList();
                string originNumeralSystemName = originDropDownList.SelectedItem.Text;

                int targetBase = Convert.ToInt32(targetDropDownList.SelectedValue);
                List<char> targetNumeralSystem = masterNumeralSystem.Take(targetBase).ToList();

                // read user input
                string input = inputTextBox.Text;
                if (input[0] == '-')
                {
                    input = input.TrimStart('-');
                }
                input = input.TrimStart(' ', '0');
                input = input.Length > 0 ? input : "0"; 
                char[] inputArray = input.ToCharArray();

                // validate user input
                if (HelperMethods.ValidateInput(originNumeralSystem, inputArray, resultLabel.Text, originNumeralSystemName, originBase, targetBase, input, placesTextBox.Text))
                {
                    var places = Convert.ToInt32(placesTextBox.Text);
                    // convert user input to decimal
                    double decimalInput = HelperMethods.ConvertInputToDecimal(inputArray, originBase, masterNumeralSystem, resultLabel.Text, places);
                    resultLabel.Text = decimalInput.ToString();
                    // convert user input from decimal to target base
                    string targetOutput = HelperMethods.ConvertDecimalToTarget(resultLabel.Text, decimalInput, masterNumeralSystem, targetNumeralSystem, targetBase, places);
                    // display result
                    resultLabel.Text = HelperMethods.FormatResultForDisplay(input, inputArray, targetOutput, originBase, targetBase, inputTextBox.Text, placesTextBox.Text);
                }
            }
            // exception handling
            catch (TargetNumeralSystemLacksCharacterException ex)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please only enter characters that exist in the " + ex.NumeralSystemName + " number system?</span>";
            }
            catch (TooManyMinusesException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple minus signs?</span>";
            }
            catch (TooManyPeriodsException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter multiple periods?</span>";
            }
            catch (MisplacedMinusException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Would you please not enter a minus sign anywhere but the front of your number?</span>";
            }
            catch (UnaryFractionException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Fractional values cannot exist in the Base 1 (Unary) numeral system.</span>";
            }
            catch (InvalidPlacesException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>Please enter a positive integer in the places field.</span>";
            }
            catch (NoDogsOnTheMoonException)
            {
                resultLabel.Text = "<span style='color:#B33A3A;'>There is no 0 in the Base 1 (Unary) system.</span>";
            }
            catch (FormatZeroException ex)
            {
                resultLabel.Text = "0<sub>" + ex.OriginBase + "</sub> = 0<sub>" + ex.TargetBase + "</sub>";
            }
        }
    }
}