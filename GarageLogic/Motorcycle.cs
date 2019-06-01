namespace GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_WheelsNum = 2;
        private const float k_MaxWheelAirPressure = 33;
        private LicenseType m_LicenseType; 
        private int m_EngineVolume; 

        public Motorcycle() : base(k_WheelsNum, k_MaxWheelAirPressure)
        {
        }

        public override string GetSpecificDetails()
        {
            string details = string.Format(
@"License type: {0}
Engine volume: {1}",
m_LicenseType.ToString(), 
m_EngineVolume.ToString());

            return details;
        }

        public override string[] GetSpecificPropertiesAsStrings()
        {
            string[] SpecificProperties = new string[2];
            SpecificProperties[0] = string.Format(
@"Enter following specific properties, please:
============================================
A license type:
1-> A
2-> AB
3-> A2
4-> B1");
            SpecificProperties[1] = "Enter engine volume:";

            return SpecificProperties;
        }

        public override void SetSpecificProperties(string[] i_SpecificProperties)
        {
            m_LicenseType = LicenseType.Parse(i_SpecificProperties[0]);
            m_EngineVolume = int.Parse(i_SpecificProperties[1]);
        }
    }
}
