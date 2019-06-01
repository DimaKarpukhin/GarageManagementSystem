namespace ConsoleUI
{
    using System;

    public static class GarageManagerUI
    {
        private const string k_Interruption = "X";
        private static bool s_Exit;
        private static bool s_InterruptCurrTask;

        public static void Run()
        {
            try
            {
                run();
            }
            catch (FormatException exception)
            {
                WriteErrorMsg();
                UserInputOutput.WriteLine(exception.ToString());
            }
            catch (ArgumentException exception)
            {
                WriteErrorMsg();
                Console.WriteLine(exception.ToString());
            }
            catch (GarageLogic.ValueOutOfRangeException exception)
            {
                WriteErrorMsg();
                UserInputOutput.WriteLine(exception.ToString());
            }
            catch (Exception exception)
            {
                WriteErrorMsg();
                UserInputOutput.WriteLine(exception.ToString());
            }
        }

        public static void WriteErrorMsg()
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"
 _____  _____   _____   _____   _____    _
(   __)(  O  ) (  O  ) /  _  \ (  O  )  / \
|  >__ |     \ |     \(  (_)  )|     \  \_/
(_____)(__)\__)(__)\__)\_____/ (__)\__)  O
==========================================
");
        }

        private static void run()
        {
            string userInput = null;
            GarageLogic.Garage garage = new GarageLogic.Garage();

            while (!s_Exit)
            {
                showGarageMenu();
                userInput = Console.ReadLine();
                activeMethodAccordingToUserChoiseFromGarageMenu(ValidInputOptions.Parse(userInput), garage);
                exitIfUserDidNotChooseAnyOption();
            }

            writeHaveAGoodDayMsg();
        }

        private static void activeMethodAccordingToUserChoiseFromGarageMenu(ValidInputOptions i_UserChoice, GarageLogic.Garage i_Garage)
        {
            switch (i_UserChoice.Input)
            {
                case eValidInputOptions.One:
                    addNewVehicleToGarage(i_Garage);
                    break;
                case eValidInputOptions.Two:
                    showLicenseNumsOfVehiclesAccordingTheKey(i_Garage);
                    break;
                case eValidInputOptions.Three:
                    changeVehicleStatusAccordingThekey(i_Garage);
                    break;
                case eValidInputOptions.Four:
                    inflateWheelsToMaxPressure(i_Garage);
                    break;
                case eValidInputOptions.Five:
                    refeulFuelVehicle(i_Garage);
                    break;
                case eValidInputOptions.Six:
                    chargeElectricVehicle(i_Garage);
                    break;
                case eValidInputOptions.Seven:
                    showAllCurrentInformationOfVihecle(i_Garage);
                    break;
                case eValidInputOptions.Exit:
                    s_Exit = true;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static void addNewVehicleToGarage(GarageLogic.Garage i_Garage)
        {
            string[] specificPropertiesToGet = null;
            string[] specificPropertiesToSet = null;
            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItIsNotExistiInGarage(i_Garage);

            if (!s_InterruptCurrTask)
            {
                UserInputOutput.WriteLine(GarageLogic.VehicleFactory.GetListOfAvailableVehiclesToBuild());
                GarageLogic.Vehicle newVehicle = buildVehilce(UserInputOutput.ReadLine());
                if (!s_InterruptCurrTask)
                {
                    setStandardDetailsOfNewVehicle(newVehicle, licenseNumber);
                    UserInputOutput.WriteLine(GarageLogic.PropulsionSystemFactory.GetListOfAvailableSystems());
                    newVehicle.SetPropulsionSystem(newVehicle.GetType().Name, UserInputOutput.ReadLine());
                    specificPropertiesToGet = newVehicle.GetSpecificPropertiesAsStrings();
                    specificPropertiesToSet = new string[specificPropertiesToGet.Length];
                    UserInputOutput.ClearScreen();
                    for (int i = 0; i < specificPropertiesToGet.Length; i++)
                    {
                        UserInputOutput.WriteLine(specificPropertiesToGet[i]);
                        specificPropertiesToSet[i] = UserInputOutput.ReadLine();
                    }

                    newVehicle.SetSpecificProperties(specificPropertiesToSet);
                    string[] ownerDetailsToSet = getOwnerDetailsOfVehicleFromUser();
                    i_Garage.SetOwnerDetailsOfVehicle(licenseNumber, ownerDetailsToSet);
                    i_Garage.AddVehicle(newVehicle);
                }
            }
        }

        private static void showLicenseNumsOfVehiclesAccordingTheKey(GarageLogic.Garage i_Garage)
        {
            string[] licenseNumsContainer = null;

            showVehiclesStatusOptions();
            GarageLogic.GarageVehicleStatus key = GarageLogic.GarageVehicleStatus.Parse(UserInputOutput.ReadLine());
            UserInputOutput.ClearScreen();
            if (key.VehicleStatus == GarageLogic.eGarageVehicleStatus.AllStatuses)
            {
                licenseNumsContainer = i_Garage.GetLicenseNumsOfAllVehicles();
            }
            else
            {
                licenseNumsContainer = i_Garage.GetLicenseNumsOfAllVehiclesWithSpecStatus(key);
            }

            foreach (string value in licenseNumsContainer)
            {
                UserInputOutput.WriteLine(value);
            }
        }

        private static void changeVehicleStatusAccordingThekey(GarageLogic.Garage i_Garage)
        {
            showVehiclesStatusOptions();
            GarageLogic.GarageVehicleStatus key = GarageLogic.GarageVehicleStatus.Parse(UserInputOutput.ReadLine());
            while (key.VehicleStatus == GarageLogic.eGarageVehicleStatus.AllStatuses)
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(
@"
You can't change a vehicle's status to all statuses, choose only one statuse please!");
                showVehiclesStatusOptions();
                key = GarageLogic.GarageVehicleStatus.Parse(UserInputOutput.ReadLine());
            }

            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItExistsInGarage(i_Garage);
            if (!s_InterruptCurrTask)
            {
                i_Garage.ChangeVehicleStatus(licenseNumber, key);
            }
        }

        private static void inflateWheelsToMaxPressure(GarageLogic.Garage i_Garage)
        {
            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItExistsInGarage(i_Garage);

            if (!s_InterruptCurrTask)
            {
                i_Garage.FillToMaxAirInWheelsOfVehicle(licenseNumber);
            }
        }

        private static void refeulFuelVehicle(GarageLogic.Garage i_Garage)
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"Type an amount of fuel you want to refill in liters");
            float litersOfFuelToRefuel = float.Parse(UserInputOutput.ReadLine());
            UserInputOutput.WriteLine(@"
Choose a type of fuel:
=====================
1-> Octan98
2-> Octan96
3-> Octan95
4-> Soler");
            GarageLogic.FuelType fuelType = GarageLogic.FuelType.Parse(UserInputOutput.ReadLine());
            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItExistsInGarage(i_Garage);
            if (!s_InterruptCurrTask)
            {
                i_Garage.FillFuelInVehicleThatBasedOnFuelSystem(licenseNumber, fuelType, litersOfFuelToRefuel);
            }
        }

        private static void chargeElectricVehicle(GarageLogic.Garage i_Garage)
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"Type an amount of minutes to charge");
            float minutesToCharge = float.Parse(UserInputOutput.ReadLine());
            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItExistsInGarage(i_Garage);
            if (!s_InterruptCurrTask)
            {
                i_Garage.ChargeVehicleThatBasedOnElectricSystem(licenseNumber, minutesToCharge);
            }
        }

        private static void showAllCurrentInformationOfVihecle(GarageLogic.Garage i_Garage)
        {
            string licenseNumber = getUserInputOfVehicleLicenceNumberIfItExistsInGarage(i_Garage);

            if (!s_InterruptCurrTask)
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(i_Garage.GetAllDetailsOfVehicle(licenseNumber));
            }
        }

        private static void exitIfUserDidNotChooseAnyOption()
        {
            if (!s_Exit && !s_InterruptCurrTask)
            {
                UserInputOutput.WriteLine(@"
Press any key to go back to 'Garage Menu' or press Q to exit the program");
                string input = UserInputOutput.ReadLine();
                // $G$ CSS-999 (-2) You should have used constant
                if (input == "Q" || input == "q")
                {
                    s_Exit = true;
                }
            }
        }

        private static void showGarageMenu()
        {
            s_InterruptCurrTask = false;
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(
@"************
GARAGE MENU:
************
Choose one the following operations to do:
==========================================
1-> Add a new vehicle to the garage 
2-> Show the vehicles according their repair status 
3-> Change repair status of the vehicle
4-> Inflate the wheels to maximum pressure
5-> Refuel fuel vehicle
6-> Charge electric vehicle
7-> Show all current information about the vehicle

Press 'Q' for exit
");
        }

        private static string[] getOwnerDetailsOfVehicleFromUser()
        {
            const int k_AmountOfDetails = 2;
            string[] result = new string[k_AmountOfDetails];

            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(
@"Type vehicle's owner name first and owner phone number second 
(press enter after each input)");
            for (int i = 0; i < k_AmountOfDetails; i++)
            {
                result[i] = UserInputOutput.ReadLine();
            }

            return result;
        }

        private static GarageLogic.Vehicle buildVehilce(string i_TypeOfVehicle)
        {
            GarageLogic.Vehicle newVehicle = GarageLogic.VehicleFactory.BuildNewVehicle(i_TypeOfVehicle);

            while (newVehicle == null && !s_InterruptCurrTask)
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(
@"This type of the vehicle, you inserted doesn't created on our vehicle factory.
Insert another type of vehicl or press 'X' to interrupt the task.");
                s_InterruptCurrTask = isInterruptTheCurrTask(out i_TypeOfVehicle);
                newVehicle = GarageLogic.VehicleFactory.BuildNewVehicle(i_TypeOfVehicle);
            }

            return newVehicle;
        }

        private static void setStandardDetailsOfNewVehicle(GarageLogic.Vehicle i_Vehicle, string i_LicenseNum)
        {
            UserInputOutput.ClearScreen();
            i_Vehicle.LicenseNum = i_LicenseNum;
            UserInputOutput.WriteLine(@"Enter the model name of your vehicle, please");
            i_Vehicle.ModelName = UserInputOutput.ReadLine();
            for (int i = 0; i < i_Vehicle.WheelsNum; i++)
            {
                UserInputOutput.WriteLine(string.Format(@"Enter the manufacturer name of {0} wheel, please", i + 1));
                i_Vehicle.SetManufacturerNameOfSpecificWheel(i, UserInputOutput.ReadLine());
            }
        }

        private static string getUserInputOfVehicleLicenceNumberIfItIsNotExistiInGarage(GarageLogic.Garage i_Garage)
        {
            int dummyValue;
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"Type the 'licence number' of the vehicle");
            string licenseNumberInput = UserInputOutput.ReadLine();
            while (!int.TryParse(licenseNumberInput, out dummyValue))
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(@"Invalid input! Insert numeral value, please!");
                licenseNumberInput = UserInputOutput.ReadLine();
            }

            while (i_Garage.isVehicleExists(licenseNumberInput) && !s_InterruptCurrTask)
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(@"
The vehicle with license number you typed already exist in garage.
Type license number of another vehicle  or press 'X' to interrupt the task.");
                s_InterruptCurrTask = isInterruptTheCurrTask(out licenseNumberInput);
            }

            return licenseNumberInput;
        }

        private static string getUserInputOfVehicleLicenceNumberIfItExistsInGarage(GarageLogic.Garage i_Garage)
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"Type the 'licence number' of the vehicle");
            string licenseNumberInput = UserInputOutput.ReadLine();
            while (!i_Garage.isVehicleExists(licenseNumberInput) && !s_InterruptCurrTask)
            {
                UserInputOutput.ClearScreen();
                UserInputOutput.WriteLine(@"
The license number of vehicle you typed is not found.
Please check your input before you type again or
press 'X' to interrupt the task.
");
                s_InterruptCurrTask = isInterruptTheCurrTask(out licenseNumberInput);
            }

            return licenseNumberInput;
        }

        private static bool isInterruptTheCurrTask(out string io_Input)
        {
            io_Input = UserInputOutput.ReadLine();
            return io_Input == k_Interruption || io_Input == "x";
        }

        private static void showVehiclesStatusOptions()
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"
Choose a repair status of the following options, please:
============================================
1-> Under repair
2-> Fixed
3-> Paid
4-> All statuses");
        }

        private static void writeHaveAGoodDayMsg()
        {
            UserInputOutput.ClearScreen();
            UserInputOutput.WriteLine(@"
 _   _    __  __  __  _____      __      ____   _____    _____   ____      ____     __   _   _  _
| |_| |  /__\(  )(  )(   __)    /__\    / ___) /  _  \  /  _  \ |  _  \   |  _  \  /__\ ( \_/ )/ \
|  _  | /(__)\\ \/ / |  >__    /(__)\  ( (___n(  (_)  )(  (_)  )| |_)  )  | |_)  )/(__)\ \   / \_/
|_| |_|(__)(__)\__/  (_____)  (__)(__)  \____/ \_____/  \_____/ |_____/   |_____/(__)(__)(__/   O
===================================================================================================");
        }
    }
}
