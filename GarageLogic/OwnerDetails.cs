namespace GarageLogic
{
    public class OwnerDetails
    {
        public OwnerDetails(string i_OwnerName, string i_OwnerPhoneNum)
        {
            OwnerName = i_OwnerName;
            OwnerPhoneNum = i_OwnerPhoneNum;
        }

        public string OwnerName { get; set; }

        public string OwnerPhoneNum { get; set; }
    }
}
