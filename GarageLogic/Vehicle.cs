namespace GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private float m_PercentageOfEnergy; 
        private Wheel[] m_WheelsCollection; 
        private PropulsionSystem m_PropulsionSystem;

        public Vehicle(int i_WheelsNum, float i_MaxWheelAirPressure)
        {
            m_WheelsCollection = new Wheel[i_WheelsNum];
            for (int i = 0; i < i_WheelsNum; i++)
            {
                m_WheelsCollection[i] = new Wheel();
                m_WheelsCollection[i].MaxAirPressure = i_MaxWheelAirPressure;
            }
        }

        public string LicenseNum { get; set; }

        public string ModelName
        {
            set { m_ModelName = value; }
        }

        public int WheelsNum
        {
            get { return m_WheelsCollection.Length; }
        }

        public void SetPropulsionSystem(string i_VehicleName, string i_PropulsionSystemType)
        {
            m_PropulsionSystem = PropulsionSystemFactory.CreatePropulsionSystem(i_VehicleName, i_PropulsionSystemType);
        }        

        public void FillEnergy(object i_FillingDetails)
        {
            float percentageOfEnergyAfterFilling = m_PropulsionSystem.FillEnergy(i_FillingDetails);
            m_PercentageOfEnergy = percentageOfEnergyAfterFilling;
        }

        public void FillToMaxAirInWheels()
        {
            foreach (Wheel wheel in m_WheelsCollection)
            {
                wheel.InflateToMaxAirPressure();
            }
        }

        public abstract string GetSpecificDetails();

        public string GetDetailsOfCommonThings()
        {
            string details = string.Format(
@"Model name: {0}
License number: {1}
Percentage of energy: {2:f1}%

Wheel's details",
m_ModelName,
LicenseNum,
m_PercentageOfEnergy);

            details += getWheelsDetails() + m_PropulsionSystem.GetEnergyDetails();
            return details;
        }

        public void SetManufacturerNameOfSpecificWheel(int i_WheelInd, string i_ManufacturerName)
        {
            m_WheelsCollection[i_WheelInd].ManufacturerName = i_ManufacturerName;
        }

        public abstract string[] GetSpecificPropertiesAsStrings();

        public abstract void SetSpecificProperties(string[] i_SpecificProperties);

        private string getWheelsDetails()
        {
            string details = null;
            foreach (Wheel wheel in m_WheelsCollection)
            {
                details += string.Format(
@"
Manufacturer name: {0}
Current air pressure: {1}", 
wheel.ManufacturerName, 
wheel.CurrentAirPressure);
            }

            return details;
        }
    }   
}