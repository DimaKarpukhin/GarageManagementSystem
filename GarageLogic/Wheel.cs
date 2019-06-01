namespace GarageLogic
{
    using System;

    public class Wheel
    {
        private float m_MaxAirPressure;

        public string ManufacturerName { get; set; }

        public float CurrentAirPressure { get; set; }

        public float MaxAirPressure
        {
            get { return MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }

        public void Inflate(float i_AirToAdd)
        {
            try
            {
                if ((CurrentAirPressure + i_AirToAdd) <= m_MaxAirPressure)
                {
                    CurrentAirPressure += i_AirToAdd;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                throw new ValueOutOfRangeException(exception, 0, m_MaxAirPressure);
            }
        }

        public void InflateToMaxAirPressure()
        {
            Inflate(m_MaxAirPressure - CurrentAirPressure);
        }
    }
}
