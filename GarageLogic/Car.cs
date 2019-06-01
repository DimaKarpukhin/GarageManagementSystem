namespace GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_WheelsNum = 4;
        private const float k_MaxWheelAirPressure = 30;
        private Color m_Color; 
        private DoorsNum m_DoorsNum; 
        
        public Car() : base(k_WheelsNum, k_MaxWheelAirPressure)
        {
        }
        
        public override string GetSpecificDetails()
        {
            string details = string.Format(
@"Color: {0}
Amount of doors: {1}",
m_Color.ToString(),
m_DoorsNum.ToString());

            return details;
        }

        public override string[] GetSpecificPropertiesAsStrings()
        {
            string[] SpecificProperties = new string[2];
            SpecificProperties[0] = string.Format(
@"Enter following specific properties, please:
============================================
A color:
1-> Blue
2-> White
3-> Yellow
4-> Black");
            SpecificProperties[1] = "Amount of doors (from 2 to 5 is available):";

            return SpecificProperties;
        }

        public override void SetSpecificProperties(string[] i_SpecificProperties)
        {
            m_Color = Color.Parse(i_SpecificProperties[0]);
            m_DoorsNum = DoorsNum.Parse(i_SpecificProperties[1]);
        }
    }
}