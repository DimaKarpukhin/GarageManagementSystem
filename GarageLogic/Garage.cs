namespace GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class Garage
    {
        private readonly Dictionary<string, Vehicle> m_InFixVehicles = new Dictionary<string, Vehicle>();
        private readonly Dictionary<string, Vehicle> m_FixedVehicles = new Dictionary<string, Vehicle>();
        private readonly Dictionary<string, Vehicle> m_PayedVehicles = new Dictionary<string, Vehicle>();
        private readonly Dictionary<string, OwnerDetails> m_VehiclesOwnersDetails = new Dictionary<string, OwnerDetails>();

        public void AddVehicle(Vehicle i_Vehicle)
        {
            m_InFixVehicles[i_Vehicle.LicenseNum] = i_Vehicle;
        }

        public void SetOwnerDetailsOfVehicle(string i_VehicleLicenseNum, params string[] i_OwnerDetails)
        {
            m_VehiclesOwnersDetails[i_VehicleLicenseNum] = new OwnerDetails(i_OwnerDetails[0], i_OwnerDetails[1]);
        }
    
        public bool isVehicleExists(string i_LicenseNum)
        {
            return detectCollectionInWhichTheVehicleStored(i_LicenseNum) != null;
        }

        public string[] GetLicenseNumsOfAllVehicles()
        {
            string[] resArr = new string[m_InFixVehicles.Count + m_FixedVehicles.Count + m_PayedVehicles.Count];
            int i = 0;

            //Array.Copy(GetLicenseNumsOfAllVehiclesWithSpecStatus(
            //    GarageVehicleStatus.Parse("under repair")), 0, resArr, 0, m_InFixVehicles.Count);
            //Array.Copy(GetLicenseNumsOfAllVehiclesWithSpecStatus(
            //    GarageVehicleStatus.Parse("fixed")), 0, resArr, m_InFixVehicles.Count, m_FixedVehicles.Count);
            //Array.Copy(GetLicenseNumsOfAllVehiclesWithSpecStatus(
            //    GarageVehicleStatus.Parse("paid")), 0, resArr, m_FixedVehicles.Count, m_PayedVehicles.Count);

            Array.Copy(extractKeyValues(m_InFixVehicles), 0, resArr, 0, m_InFixVehicles.Count);
            Array.Copy(extractKeyValues(m_FixedVehicles), 0, resArr, m_InFixVehicles.Count, m_FixedVehicles.Count);
            Array.Copy(extractKeyValues(m_PayedVehicles), 0, resArr, m_FixedVehicles.Count, m_PayedVehicles.Count);


            //foreach (KeyValuePair<string, Vehicle> kvp in m_InFixVehicles)
            //{
            //    resArr[i] = kvp.Key;
            //    i++;
            //}

            //foreach (KeyValuePair<string, Vehicle> kvp in m_FixedVehicles)
            //{
            //    resArr[i] = kvp.Key;
            //    i++;
            //}

            //foreach (KeyValuePair<string, Vehicle> kvp in m_PayedVehicles)
            //{
            //    resArr[i] = kvp.Key;
            //    i++;
            //}

            return resArr;
        }

        public string[] GetLicenseNumsOfAllVehiclesWithSpecStatus(GarageVehicleStatus i_Status)
        {
            Dictionary<string, Vehicle> collectionToScan = detectCollectionByStatus(i_Status);
            
            return extractKeyValues(collectionToScan);
        }

        public void ChangeVehicleStatus(string i_LicenseNum, GarageVehicleStatus i_NewStatus)
        {
            Dictionary<string, Vehicle> collectionInWhichTheVehicleStored = detectCollectionInWhichTheVehicleStored(i_LicenseNum);
            Dictionary<string, Vehicle> destCollection = detectCollectionByStatus(i_NewStatus);

            if (collectionInWhichTheVehicleStored != destCollection)
            {
                destCollection[i_LicenseNum] = collectionInWhichTheVehicleStored[i_LicenseNum];
                collectionInWhichTheVehicleStored.Remove(i_LicenseNum);
            }
        }

        public void FillToMaxAirInWheelsOfVehicle(string i_LicenseNum)
        {
            Dictionary<string, Vehicle> collectionInWhichTheVehicleStored = detectCollectionInWhichTheVehicleStored(i_LicenseNum);
            collectionInWhichTheVehicleStored[i_LicenseNum].FillToMaxAirInWheels();
        }

        public void FillFuelInVehicleThatBasedOnFuelSystem(string i_LicenseNum, FuelType i_FuelType, float i_FillingAmount)
        {
            Dictionary<string, Vehicle> collectionWhereVehicleStored = detectCollectionInWhichTheVehicleStored(i_LicenseNum);
            if (collectionWhereVehicleStored == null)
            {
                throw new ArgumentException();
            }
            else
            {
                collectionWhereVehicleStored[i_LicenseNum].FillEnergy(new FuelSystem.FuellFillingDetails(i_FuelType, i_FillingAmount));
            }
        }

        public void ChargeVehicleThatBasedOnElectricSystem(string i_LicenseNum, float i_NumOfMinsToCharge)
        {
            Dictionary<string, Vehicle> collectionWhereVehicleStored = detectCollectionInWhichTheVehicleStored(i_LicenseNum);
            if (collectionWhereVehicleStored == null)
            {
                throw new ArgumentException();
            }
            else
            {
                collectionWhereVehicleStored[i_LicenseNum].FillEnergy(i_NumOfMinsToCharge / 60f);
            }
        }

        public string GetAllDetailsOfVehicle(string i_LicenseNum)
        {
            string details = null;
            Dictionary<string, Vehicle> collectionWhereVehicleStored = detectCollectionInWhichTheVehicleStored(i_LicenseNum);
            if (collectionWhereVehicleStored == null)
            {
                throw new ArgumentException();
            }
            else
            {
                Vehicle theVehicle = collectionWhereVehicleStored[i_LicenseNum];
                details = theVehicle.GetSpecificDetails();
                details += theVehicle.GetDetailsOfCommonThings();
                details = string.Format(
@"There are the details of current vehicle:
==========================================
{0}
{1}
{2}
Owner name: {3}
Owner phone: {4}", 
theVehicle.GetSpecificDetails(), 
theVehicle.GetDetailsOfCommonThings(),
getStatusAccordingToCollection(collectionWhereVehicleStored),
m_VehiclesOwnersDetails[i_LicenseNum].OwnerName,
m_VehiclesOwnersDetails[i_LicenseNum].OwnerPhoneNum);
            }

            return details;
        }

        private string[] extractKeyValues(Dictionary<string, Vehicle> i_VehicleCollection)
        {
            string[] resArr = new string[i_VehicleCollection.Count];
            int i = 0;

            foreach (KeyValuePair<string, Vehicle> kvp in i_VehicleCollection)
            {
                resArr[i] = kvp.Key;
                i++;
            }

            return resArr;
        }

        private Dictionary<string, Vehicle> detectCollectionByStatus(GarageVehicleStatus i_Status)
        {
            Dictionary<string, Vehicle> result = null;

            if (i_Status.VehicleStatus == eGarageVehicleStatus.UnderRepair)
            {
                result = m_InFixVehicles;
            }
            else if (i_Status.VehicleStatus == eGarageVehicleStatus.Fixed)
            {
                result = m_FixedVehicles;
            }
            else if (i_Status.VehicleStatus == eGarageVehicleStatus.Paid)
            {
                result = m_PayedVehicles;
            }

            return result;
        }

        private Dictionary<string, Vehicle> detectCollectionInWhichTheVehicleStored(string i_LicenseNum)
        {
            Dictionary<string, Vehicle> result;

            if (m_InFixVehicles.ContainsKey(i_LicenseNum))
            {
                result = m_InFixVehicles;
            }
            else if (m_FixedVehicles.ContainsKey(i_LicenseNum))
            {
                result = m_FixedVehicles;
            }
            else if (m_PayedVehicles.ContainsKey(i_LicenseNum))
            {
                result = m_PayedVehicles;
            }
            else
            {
                result = null;
            }

            return result;
        }

        private string getStatusAccordingToCollection(Dictionary<string, Vehicle> i_Collection)
        {
            string status = null;

            if (i_Collection == m_InFixVehicles)
            {
                status = "Vehicle status: 'under repair'";
            }
            else if (i_Collection == m_FixedVehicles)
            {
                status = "Vehicle status: 'fixed'";
            }
            else if (i_Collection == m_PayedVehicles)
            {
                status = "Vehicle status: 'paid'";
            }

            return status;
        }
    }
}