namespace GarageLogic
{
    using System;

    public class ElectricSystem : PropulsionSystem
    {
        private float m_MaxLifeTimeInHours; 
        private float m_LifeTimeLeftInHours; 

        public ElectricSystem(float i_MaxLifeTimeInHours)
        {
            m_MaxLifeTimeInHours = i_MaxLifeTimeInHours;
        }

        public override float FillEnergy(object i_FillingDetails)
        {
            if (i_FillingDetails.GetType().Name == "Single")
            {
                charge((float)i_FillingDetails);
            }
            else
            {
                throw new ArgumentException();
            }

            float percentageOfEnergyAfterFilling = (m_LifeTimeLeftInHours / m_MaxLifeTimeInHours) * 100;

            return percentageOfEnergyAfterFilling;
        }

        public override string GetEnergyDetails()
        {
            string details = string.Format(
@"

Life time left in hours : {0}
",
m_LifeTimeLeftInHours);

            return details;
        }

        private void charge(float i_HoursNumToChargeWith)
        {
            try
            {
                if (m_LifeTimeLeftInHours + i_HoursNumToChargeWith <= m_MaxLifeTimeInHours)
                {
                    m_LifeTimeLeftInHours += i_HoursNumToChargeWith;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                throw new ValueOutOfRangeException(exception, 0, m_MaxLifeTimeInHours * 60f);
            }
        }
    }
}