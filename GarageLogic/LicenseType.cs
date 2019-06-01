namespace GarageLogic
{
    using System;

    public class LicenseType
    {

        public eLicenseType MyLicenseType { get; set; }

        public static LicenseType Parse(string i_LicenseTypeAsStr)
        {
            eLicenseType licenseTypeToSet = eLicenseType.A;

            if (i_LicenseTypeAsStr == "1" || i_LicenseTypeAsStr == "A" || i_LicenseTypeAsStr == "a")
            {
                licenseTypeToSet = eLicenseType.A;
            }
            else if (i_LicenseTypeAsStr == "2" || i_LicenseTypeAsStr == "AB" || i_LicenseTypeAsStr == "Ab" || i_LicenseTypeAsStr == "aB" || i_LicenseTypeAsStr == "ab")
            {
                licenseTypeToSet = eLicenseType.AB;
            }
            else if (i_LicenseTypeAsStr == "3" || i_LicenseTypeAsStr == "A2" || i_LicenseTypeAsStr == "a2")
            {
                licenseTypeToSet = eLicenseType.A2;
            }
            else if (i_LicenseTypeAsStr == "4" || i_LicenseTypeAsStr == "B1" || i_LicenseTypeAsStr == "b1")
            {
                licenseTypeToSet = eLicenseType.B1;
            }
            else if (i_LicenseTypeAsStr != "1" && i_LicenseTypeAsStr != "A" && i_LicenseTypeAsStr != "a")
            {
                throw new FormatException();
            }

            LicenseType licenseType = new LicenseType();
            licenseType.MyLicenseType = licenseTypeToSet;

            return licenseType;
        }

        public override string ToString()
        {
            string result = null;

            switch (MyLicenseType)
            {
                case eLicenseType.A:
                    result = "A";
                    break;
                case eLicenseType.A2:
                    result = "A2";
                    break;
                case eLicenseType.AB:
                    result = "AB";
                    break;
                case eLicenseType.B1:
                    result = "B1";
                    break;
            }

            return result;
        }
    }
}
