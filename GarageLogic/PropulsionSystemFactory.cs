namespace GarageLogic
{
    using System;

    public static class PropulsionSystemFactory
    {
        public static string GetListOfAvailableSystems()
        {
            return string.Format(@"
Choose the type of Propulsion System, please:
1-> Fuel System 
2-> Electric System
");
        }

        public static PropulsionSystem CreatePropulsionSystem(string i_VehicleName, string i_PropulsionSystem)
        {
            try
            {
                PropulsionSystem newPropulsionSystem = null;

                if (i_PropulsionSystem == "1" || i_PropulsionSystem == "FuelSystem" || i_PropulsionSystem == "Fuel System")
                {
                    if (i_VehicleName == "Car")
                    {
                        newPropulsionSystem = new FuelSystem(FuelType.Parse("Octan98"), 42f);
                    }
                    else if (i_VehicleName == "Motorcycle")
                    {
                        newPropulsionSystem = new FuelSystem(FuelType.Parse("Octan95"), 5.5f);
                    }
                    else if (i_VehicleName == "Truck")
                    {
                        newPropulsionSystem = new FuelSystem(FuelType.Parse("Octan96"), 135f);
                    }
                }
                else if (i_PropulsionSystem == "2" || i_PropulsionSystem == "ElectricSystem" || i_PropulsionSystem == "Electric System")
                {
                    if (i_VehicleName == "Car")
                    {
                        newPropulsionSystem = new ElectricSystem(2.5f);
                    }
                    else if (i_VehicleName == "Motorcycle")
                    {
                        newPropulsionSystem = new ElectricSystem(2.7f);
                    }
                    else if (i_VehicleName == "Truck")
                    {
                        throw new FormatException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }

                return newPropulsionSystem;
            }
            catch (FormatException exception)
            {
                throw new ValueOutOfRangeException(exception, 1, 1);
            }
        }
    }
}
