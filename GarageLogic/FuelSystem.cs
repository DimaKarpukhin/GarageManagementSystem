namespace GarageLogic
{
    using System;

    public class FuelSystem : PropulsionSystem
    {
        private FuelType m_FuelType;               // must be initialized with a parameter from ctor
        private float m_MaxFuelAmountInLiters;     // must be initialized with a parameter from ctor
        private float m_CurrentFuelAmountInLiters; // must be initialized with a parameter from ctor

        public FuelSystem(FuelType i_FuelType, float i_MaxFuelAmountInLiters)
        {
            m_FuelType = i_FuelType;
            m_MaxFuelAmountInLiters = i_MaxFuelAmountInLiters;
        }

        public class FuellFillingDetails
        {
            private FuelType m_FuelType;
            private float m_AmountInLitersToAdd;

            public FuellFillingDetails(FuelType i_FuelType, float i_AmountInLitersToAdd)
            {
                m_FuelType = i_FuelType;
                m_AmountInLitersToAdd = i_AmountInLitersToAdd;
            }

            public FuelType FuelType
            {
                get { return m_FuelType; }
                set { m_FuelType = value; }
            }

            public float AmountInLitersToAdd
            {
                get { return m_AmountInLitersToAdd; }
                set { m_AmountInLitersToAdd = value; }
            }
        }

        public override float FillEnergy(object i_FillingDetails)
        {
            if (i_FillingDetails.GetType().Name == "FuellFillingDetails")
            {
                fillFuel((FuellFillingDetails)i_FillingDetails);
            }
            else
            {
                throw new ArgumentException();
            }

            float percentageOfEnergyAfterFilling = (m_CurrentFuelAmountInLiters / m_MaxFuelAmountInLiters) * 100;

            return percentageOfEnergyAfterFilling;
        }

        public override string GetEnergyDetails()
        {
            string details = string.Format(@"

Current fuel amount in liters: {0}
Fuel type: {1}
", 
m_CurrentFuelAmountInLiters.ToString(),
m_FuelType.ToString());

            return details;
        }

        private void fillFuel(FuellFillingDetails i_FillingDetails)
        {
            try
            {
                if (!i_FillingDetails.FuelType.Equals(m_FuelType.MyFuelType))
                {
                    throw new ArgumentException();
                }
                else
                {
                    if (m_CurrentFuelAmountInLiters + i_FillingDetails.AmountInLitersToAdd > m_MaxFuelAmountInLiters)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        m_CurrentFuelAmountInLiters += i_FillingDetails.AmountInLitersToAdd;
                    }
                }
            }
            catch (FormatException exception)
            {
                throw new ValueOutOfRangeException(exception, 0, m_MaxFuelAmountInLiters);
            }
        }
    }
}