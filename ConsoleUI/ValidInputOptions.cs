namespace ConsoleUI
{
    using System;

    public class ValidInputOptions
    {

        public eValidInputOptions Input { get; private set; }

        public static ValidInputOptions Parse(string i_InputAsStr)
        {
            eValidInputOptions inputToParse = eValidInputOptions.One;

            if (i_InputAsStr == "2")
            {
                inputToParse = eValidInputOptions.Two;
            }
            else if (i_InputAsStr == "3")
            {
                inputToParse = eValidInputOptions.Three;
            }
            else if (i_InputAsStr == "4")
            {
                inputToParse = eValidInputOptions.Four;
            }
            else if (i_InputAsStr == "5")
            {
                inputToParse = eValidInputOptions.Five;
            }
            else if (i_InputAsStr == "6")
            {
                inputToParse = eValidInputOptions.Six;
            }
            else if (i_InputAsStr == "7")
            {
                inputToParse = eValidInputOptions.Seven;
            }
            else if (i_InputAsStr == "Q" || i_InputAsStr == "q")
            {
                inputToParse = eValidInputOptions.Exit;
            }
            else if (i_InputAsStr != "1")
            {
                throw new FormatException();
            }

            ValidInputOptions input = new ValidInputOptions();
            input.Input = inputToParse;

            return input;
        }
    }
}
