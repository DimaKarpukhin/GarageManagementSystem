namespace GarageLogic
{
    public static class VehicleFactory
    {
        public static string GetListOfAvailableVehiclesToBuild()
        {
            return string.Format(@"Choose the type of the Vehicle to build, please:
1-> Car 
2-> Motorcycle
3-> Truck");
        }
    
        public static Vehicle BuildNewVehicle(string i_VehicleType)
        {
            Vehicle result = null;

            if (i_VehicleType == "1" || i_VehicleType == "Car" || i_VehicleType == "car" || i_VehicleType == "CAR")
            {
                result = new Car();
            }
            else if (i_VehicleType == "2" || i_VehicleType == "Motorsycle" || i_VehicleType == "Motorsycle" || i_VehicleType == "MOTORCYCLE")
            {
                result = new Motorcycle();
            }
            else if (i_VehicleType == "3" || i_VehicleType == "Truck" || i_VehicleType == "truck" || i_VehicleType == "TRUCK")
            {
                result = new Truck();
            }

            return result;
        }
    }
}
