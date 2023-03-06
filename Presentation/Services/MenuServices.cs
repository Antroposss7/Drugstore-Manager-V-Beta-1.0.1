using Core.Constants;
using Core.Entities;
using Data.Contexts;
using Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;

namespace Presentation.Services
{
    public class MenuServices
    {
        private readonly static OwnerService _ownerService;
        private readonly static AdminService _adminService;
        private readonly static DrugstoreService _drugstoreService;
        private readonly static DrugService _drugService;
        private readonly static DruggistService _druggistService;
        private static Admin admin;

        static MenuServices()
        {
            DbInitializer.SeedAdmins();
            _drugstoreService = new DrugstoreService(admin);
            _adminService = new AdminService();
            _ownerService = new OwnerService(admin);
            _drugService = new DrugService();
            _druggistService = new DruggistService();

            Array myArray = Enum.GetValues(typeof(MainMenuOptions));
            Console.OutputEncoding = Encoding.UTF8;
        }
        public void MainMenu() ///////////////////// MAIN MENU
        {
            Console.Clear();
            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(MainMenuOptions));
                int ownerMenuSelect = 0;
                var num = 1; 
                menuOptions[0] = "👤|Owners    | ";
                menuOptions[1] = "🏥|Drugstores|  ";
                menuOptions[2] = "‍⚕️|Druggists |  ";
                menuOptions[3] = "💊|Drugs     | ";
                menuOptions[4] = "❎|Logout    |  ";
                menuOptions[5] = "❌|Exit      |  ";


                Console.Clear();
                var newMenu = new MenuRepository(menuOptions, 1, 1);

                newMenu.ModifyMenuCentered();
                newMenu.CenterMenuToConsole();
                newMenu.ResetCursorVisible();
                int selection = 0;

              
                while (selection != 5) 
                {
                    selection = newMenu.RunMenu();

                    Console.WriteLine(menuOptions[selection] + "                   ");
                    newMenu.ResetCursorVisible();
                    switch (selection)
                    {
                        case 0:
                            {
                                OwnerMenu();
                                break;
                            }
                        case 1:
                            {
                                DrugStoreMenu();
                                break;
                            }
                        case 2:
                            {

                                DruggistMenu();
                                break;
                            }

                        case 3:
                            {
                                DrugMenu();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                _adminService.Authorize();
                                break;
                            }
                        case 5:
                            {
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
            }
        }


        public void OwnerMenu() ////////////////////////////// OWNER MENU
        {

            Console.Clear();
            string[] menuOptions = Enum.GetNames(typeof(OwnerOptions));
            menuOptions[0] = "|Create Owner|";
            menuOptions[1] = "|Update Owner|";
            menuOptions[2] = "‍|Delete Owner| ";
            menuOptions[3] = "|Get All Owners|";
            menuOptions[4] = "|Main Menu|";
            
            int ownerMenuSelect = 0;
            var num = 1; 

            Console.Clear();
            var newMenu = new MenuRepository(menuOptions, 1, 1);

            newMenu.ModifyMenuCentered();
            newMenu.CenterMenuToConsole();
            newMenu.ResetCursorVisible();
            int selection = 0;

            
            while (selection != 3)
            {
                selection = newMenu.RunMenu();

                Console.WriteLine(menuOptions[selection] + "                   ");
                newMenu.ResetCursorVisible();
                switch (selection)
                {
                    case 0:
                        {
                            Console.Clear();
                            _ownerService.Create();
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            _ownerService.Update();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            _ownerService.Delete();
                            break;
                        }

                    case 3:
                        {
                            Console.Clear();
                            _ownerService.GetAll();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            MainMenu();
                            break;
                        }
                }
            }
        }

        public void DrugStoreMenu() /////////////////////////////// DRUGSTORE MENU (OLD)

        {
            while (true)
            {
                Console.Clear();
                string[] menuOptions = Enum.GetNames(typeof(DrugstoreOptions));
                menuOptions[0] = "|Create Drugstore|";
                menuOptions[1] = "|Update Drugstore|";
                menuOptions[2] = "‍|Delete Drugstore| ";
                menuOptions[3] = "|All Drugstores  |";
                menuOptions[4] = "|Drugstores by Owner|";
                menuOptions[5] = "|Sale| ";
                menuOptions[6] = "|Main Menu|";


                int ownerMenuSelect = 0;
                var num = 1; 
                Console.Clear();
                var newMenu = new MenuRepository(menuOptions, 1, 1);

                newMenu.ModifyMenuCentered();
                newMenu.CenterMenuToConsole();
                newMenu.ResetCursorVisible();
                int selection = 0;

                //
                while (selection != 3)
                {
                    selection = newMenu.RunMenu();

                    Console.WriteLine(menuOptions[selection] + "                   ");
                    newMenu.ResetCursorVisible();
                    switch (selection)
                    {
                        case 0:
                            {
                                Console.Clear();
                                _drugstoreService.Create();
                                break;
                            }
                        case 1:
                            {
                                Console.Clear();
                                _drugstoreService.Update();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                _drugstoreService.Delete();
                                break;
                            }

                        case 3:
                            {
                                Console.Clear();
                                _drugstoreService.GetAll();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                _drugstoreService.GetAllDrugStoresByOwner();
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                _drugstoreService.Sale();
                                break;
                            }
                        case 6:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                    }
                }
            }
        }




        public void DrugMenu()
        {///////////////////////////////////////// DRUG MENU (OLD)

            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(DrugOptions));
                menuOptions[0] = "|Create Drug| ";
                menuOptions[1] = "|Update Drug| ";
                menuOptions[2] = "‍|Delete Drug|  ";
                menuOptions[3] = "| All Drugs | ";
                menuOptions[4] = "|Drugs by Drugstore|";
                menuOptions[5] = "|Filter Drugs|  ";
                menuOptions[6] = "|Main Menu|";
                int ownerMenuSelect = 0;
                var num = 1; 
                Console.Clear();
                var newMenu = new MenuRepository(menuOptions, 1, 1);

                newMenu.ModifyMenuCentered();
                newMenu.CenterMenuToConsole();
                newMenu.ResetCursorVisible();
                int selection = 0;

                
                while (selection != 3)      
                {
                    selection = newMenu.RunMenu();

                    Console.WriteLine(menuOptions[selection] + "                   ");
                    newMenu.ResetCursorVisible();
                    switch (selection)
                    {
                        case 0:
                            {
                                Console.Clear();
                                _drugService.Create();
                                break;
                            }
                        case 1:
                            {
                                Console.Clear();
                                _drugService.Update();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                _drugService.Delete();
                                break;
                            }

                        case 3:
                            {
                                Console.Clear();
                                _drugService.GetAll();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                _drugService.GetAllDrugsByDrugstore();
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                _drugService.FilterDrugs();
                                break;
                            }
                        case 6:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                    }
                }
            }
        }
        public void DruggistMenu() //////////////////////////////  DRUGGIST MENU (OLD)
        {
            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(DruggistOptions));
                menuOptions[0] = "|Create Druggist|";
                menuOptions[1] = "|Update Druggist|";
                menuOptions[2] = "‍|Delete Druggist| ";
                menuOptions[3] = "| All Druggists |";
                menuOptions[4] = "|Druggists by Drug Store|";
                menuOptions[5] = "|Main Menu|";

                int ownerMenuSelect = 0;
                var num = 1; 
                Console.Clear();
                var newMenu = new MenuRepository(menuOptions, 1, 1);

                newMenu.ModifyMenuCentered();
                newMenu.CenterMenuToConsole();
                newMenu.ResetCursorVisible();
                int selection = 0;

                // this is a good place to use a switch statement for options
                while (selection != 3)      // make this your exit case
                {
                    selection = newMenu.RunMenu();

                    Console.WriteLine(menuOptions[selection] + "                   ");
                    newMenu.ResetCursorVisible();
                    switch (selection)
                    {
                        case 0:
                            {
                                Console.Clear();
                                _druggistService.Create();
                                break;
                            }
                        case 1:
                            {
                                Console.Clear();
                                _druggistService.Update();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                _druggistService.Delete();
                                break;
                            }

                        case 3:
                            {
                                Console.Clear();
                                _druggistService.GetAll();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                _druggistService.GetAllDruggistsByDrugstore();
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                        

                    }
                }
            }
        }
    }
}


























