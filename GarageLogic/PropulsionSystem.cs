namespace GarageLogic
{
    public abstract class PropulsionSystem
    {
        public abstract float FillEnergy(object i_FillingDetails);

        public abstract string GetEnergyDetails();
    }
}
