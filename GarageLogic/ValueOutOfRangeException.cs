namespace GarageLogic
{
    using System;

    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(
            Exception i_InnerException, 
            float i_MinValue,
            float i_MaxValue)
            : base(
            string.Format(
@"
Inputed value is out of range! Expected range from {0} up to {1} !!!
", 
i_MinValue, 
i_MaxValue), 
i_InnerException)
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

        public float MaxValue { get; }

        public float MinValue { get; }
    }
}
