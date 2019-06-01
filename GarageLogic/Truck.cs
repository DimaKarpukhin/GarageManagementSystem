namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_WheelsNum = 12;
        private const float k_MaxWheelAirPressure = 32;
        private float m_MaxCarryingWeight; 
        private bool m_IsCarryingDangerousMaterials; 

        public Truck() : base(k_WheelsNum, k_MaxWheelAirPressure)
        {
        }

        public override string GetSpecificDetails()
        {
            string details = string.Format(
@"Max carrying weight: {0}
Carrying dangerous materials: {1}",
m_MaxCarryingWeight.ToString(), 
isCarryingDangerousMaterials());

            return details;
        }

        public override string[] GetSpecificPropertiesAsStrings()
        {
            string[] SpecificProperties = new string[2];
            SpecificProperties[0] = string.Format(
@"Enter following specific properties, please:
============================================
Maximum carrying weight:"); 
            SpecificProperties[1] = "Is it carrying dangerous materials ? (type true/false):";

            return SpecificProperties;
        }

        public override void SetSpecificProperties(string[] i_SpecificProperties)
        {
            m_MaxCarryingWeight = float.Parse(i_SpecificProperties[0]);
            m_IsCarryingDangerousMaterials = bool.Parse(i_SpecificProperties[1]);
        }

        private string isCarryingDangerousMaterials()
        {
            if (m_IsCarryingDangerousMaterials == true)
            {
                return "yes";
            }
            else
            {
                return "false";
            }
        }
    }
}
