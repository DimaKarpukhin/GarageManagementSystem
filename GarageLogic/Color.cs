namespace GarageLogic
{
    using System;

    public class Color
    {
         private eColor m_Color;

        public eColor MyColor
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public static Color Parse(string i_ColorAsStr)
        {
            eColor colorToSet = eColor.Black;

            if (i_ColorAsStr == "1" || i_ColorAsStr == "Blue" || i_ColorAsStr == "blue" || i_ColorAsStr == "BLUE")
            {
                colorToSet = eColor.Blue;
            }
            else if (i_ColorAsStr == "2" || i_ColorAsStr == "White" || i_ColorAsStr == "white" || i_ColorAsStr == "WHITE")
            {
                colorToSet = eColor.White;
            }
            else if (i_ColorAsStr == "3" || i_ColorAsStr == "Yellow" || i_ColorAsStr == "yellow" || i_ColorAsStr == "YELLOW")
            {
                colorToSet = eColor.Yellow;
            }
            else if (i_ColorAsStr != "4" && i_ColorAsStr != "Black" && i_ColorAsStr != "black" && i_ColorAsStr != "BLACK")
            {
                throw new FormatException();
            }

            Color color = new Color();
            color.m_Color = colorToSet;

            return color;
        }

        public override string ToString()
        {
            string result = null;

            switch (m_Color)
            {
                case eColor.Black:
                    result = "Black";
                    break;
                case eColor.Blue:
                    result = "Blue";
                    break;
                case eColor.White:
                    result = "White";
                    break;
                case eColor.Yellow:
                    result = "Yellow";
                    break;        
            }

            return result;
        }        
    }
}
