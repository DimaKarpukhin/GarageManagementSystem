namespace GarageLogic
{
    using System;

    public class GarageVehicleStatus
    {

        public eGarageVehicleStatus VehicleStatus { get; set; }

        public static GarageVehicleStatus Parse(string i_GarageVehicleStatusAsStr)
        {
            eGarageVehicleStatus vehicleStatusToSet = eGarageVehicleStatus.UnderRepair;

            if (i_GarageVehicleStatusAsStr == "2" || i_GarageVehicleStatusAsStr.Contains("Fi") || i_GarageVehicleStatusAsStr.Contains("fi") || i_GarageVehicleStatusAsStr.Contains("FI"))
            {
                vehicleStatusToSet = eGarageVehicleStatus.Fixed;
            }
            else if (i_GarageVehicleStatusAsStr == "3" || i_GarageVehicleStatusAsStr.Contains("Pa") || i_GarageVehicleStatusAsStr.Contains("pa") || i_GarageVehicleStatusAsStr.Contains("PA"))
            {
                vehicleStatusToSet = eGarageVehicleStatus.Paid;
            }
            else if (i_GarageVehicleStatusAsStr == "4" || i_GarageVehicleStatusAsStr.Contains("Al") || i_GarageVehicleStatusAsStr.Contains("AL") || i_GarageVehicleStatusAsStr.Contains("al"))
            {
                vehicleStatusToSet = eGarageVehicleStatus.AllStatuses;
            }
            else if (i_GarageVehicleStatusAsStr != "1" && !i_GarageVehicleStatusAsStr.Contains("Un") && !i_GarageVehicleStatusAsStr.Contains("UN") && !i_GarageVehicleStatusAsStr.Contains("un"))
            {
                throw new FormatException();
            }

            GarageVehicleStatus vehicleStatus = new GarageVehicleStatus();
            vehicleStatus.VehicleStatus = vehicleStatusToSet;

            return vehicleStatus;
        }
        
        public override string ToString()
        {
            string result = null;

            switch (VehicleStatus)
            {
                case eGarageVehicleStatus.UnderRepair:
                    result = "Under Repair";
                    break;
                case eGarageVehicleStatus.Fixed:
                    result = "Fixed";
                    break;
                case eGarageVehicleStatus.Paid:
                    result = "Paid";
                    break;
                case eGarageVehicleStatus.AllStatuses:
                    result = "All Statuses";
                    break;
            }

            return result;
        }
    }
}
