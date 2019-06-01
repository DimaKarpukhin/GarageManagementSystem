namespace GarageLogic
{
    using System;

    public class FuelType
    {

        public eFuelType MyFuelType { get; set; }

        public static FuelType Parse(string i_FuelTypeAsStr)
        {
            eFuelType fuelTypeToSet = eFuelType.Octan98;

            if (i_FuelTypeAsStr == "2" || i_FuelTypeAsStr == "Octan96" || i_FuelTypeAsStr == "Octan 96" || i_FuelTypeAsStr == "octan96" || i_FuelTypeAsStr == "octan 96" || i_FuelTypeAsStr == "OCTAN96" || i_FuelTypeAsStr == "OCTAN 96")
            {
                fuelTypeToSet = eFuelType.Octan96;
            }
            else if (i_FuelTypeAsStr == "3" || i_FuelTypeAsStr == "Octan95" || i_FuelTypeAsStr == "Octan 95" || i_FuelTypeAsStr == "octan95" || i_FuelTypeAsStr == "octan 95" || i_FuelTypeAsStr == "OCTAN95" || i_FuelTypeAsStr == "OCTAN 95")
            {
                fuelTypeToSet = eFuelType.Octan95;
            }
            else if (i_FuelTypeAsStr == "4" || i_FuelTypeAsStr == "Soler" || i_FuelTypeAsStr == "soler" || i_FuelTypeAsStr == "SOLER")
            {
                fuelTypeToSet = eFuelType.Soler;
            }
            else if (i_FuelTypeAsStr != "1" && i_FuelTypeAsStr != "Octan98" && i_FuelTypeAsStr != "Octan 98" && i_FuelTypeAsStr != "octan98" && i_FuelTypeAsStr != "octan 98" && i_FuelTypeAsStr != "OCTAN98" && i_FuelTypeAsStr != "OCTAN 98")
            {
                throw new FormatException();
            }

            FuelType fuelType = new FuelType();
            fuelType.MyFuelType = fuelTypeToSet;

            return fuelType;
        }

        public override string ToString()
        {
            string result = null;

            switch (MyFuelType)
            {
                case eFuelType.Octan95:
                    result = "Octan95";
                    break;
                case eFuelType.Octan96:
                    result = "Octan96";
                    break;
                case eFuelType.Octan98:
                    result = "Octan98";
                    break;
                case eFuelType.Soler:
                    result = "Soler";
                    break;
            }

            return result;
        }       
    }
}
