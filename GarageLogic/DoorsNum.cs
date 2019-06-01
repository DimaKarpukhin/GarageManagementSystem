namespace GarageLogic
{
    using System;

    public class DoorsNum
    {
        public eDoorsNum MyDoorsNum { get; set; }

        public static DoorsNum Parse(string i_DoorsNumAsStr)
        {
            eDoorsNum doorsNumToSet = eDoorsNum.Two;

            if (i_DoorsNumAsStr == "3" || i_DoorsNumAsStr == "three" || i_DoorsNumAsStr == "Three" || i_DoorsNumAsStr == "THREE")
            {
                doorsNumToSet = eDoorsNum.Three;
            }
            else if (i_DoorsNumAsStr == "4" || i_DoorsNumAsStr == "four" || i_DoorsNumAsStr == "Four" || i_DoorsNumAsStr == "FOUR")
            {
                doorsNumToSet = eDoorsNum.Four;
            }
            else if (i_DoorsNumAsStr == "5" || i_DoorsNumAsStr == "five" || i_DoorsNumAsStr == "Five" || i_DoorsNumAsStr == "FIVE")
            {
                doorsNumToSet = eDoorsNum.Five;
            }
            else if (i_DoorsNumAsStr != "2" && i_DoorsNumAsStr != "two" && i_DoorsNumAsStr != "Two" && i_DoorsNumAsStr != "TWO")
            {
                throw new FormatException();
            }

            DoorsNum doorsNum = new DoorsNum();
            doorsNum.MyDoorsNum = doorsNumToSet;

            return doorsNum;
        }

        public override string ToString()
        {
            string result = null;

            switch (MyDoorsNum)
            {
                case eDoorsNum.Two:
                    result = "2";
                    break;
                case eDoorsNum.Three:
                    result = "3";
                    break;
                case eDoorsNum.Four:
                    result = "4";
                    break;
                case eDoorsNum.Five:
                    result = "5";
                    break;
            }

            return result;
        }      
    }
}
