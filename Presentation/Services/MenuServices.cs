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
        private readonly static OwnerService1 _ownerService;
        private readonly static AdminService1 _adminService;
        private readonly static DrugstoreService _drugstoreService;
        private static Admin admin;

        static MenuServices()
        {
            DbInitializer.SeedAdmins();
            _drugstoreService = new DrugstoreService(admin);
            _adminService = new AdminService1();
            _ownerService = new OwnerService1(admin);


            Array myArray = Enum.GetValues(typeof(MainMenuOptions));
            Console.OutputEncoding = Encoding.UTF8;



        }

        public void MainMenu()
        {



            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(MainMenuOptions));
                int ownerMenuSelect = 0;
                var num = 1; // поменять на смайлики! 


                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;

                    ConsoleHelper.WriteWithColor("Hello and welcome! Please choose type of registration:",
                        ConsoleColor.DarkMagenta);

                    for (int i = 0; i < menuOptions.Length; i++)
                    {

                        ConsoleHelper.WriteWithColor(
                            (i == ownerMenuSelect ? $"|[{num}]" : "") + menuOptions[i] +
                            (i == ownerMenuSelect ? "|" : ""), ConsoleColor.Blue);

                    }

                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && ownerMenuSelect != menuOptions.Length - 1)
                    {
                        Console.Beep();
                        ownerMenuSelect++;
                        num++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && ownerMenuSelect >= 1)
                    {
                        Console.Beep();
                        ownerMenuSelect--;
                        num--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        switch (ownerMenuSelect)
                        {
                            case 0:
                                Console.Clear();
                                OwnerMenu();
                                break;
                            case 1:
                                Console.Clear();
                                DrugStoreMenu();
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                Console.Clear();
                                Console.CursorVisible = true;
                                _adminService.Authorize();
                                break;





                        }

                    }



                }

            }
        }

        public void OwnerMenu()
        {

            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(OwnerOptions));
                int menuSelect = 0;
                int menuNum = 1;
                while (true)
                {

                    Console.Clear();
                    Console.CursorVisible = false;
                    ConsoleHelper.WriteWithColor("Owner Options", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("-------------", ConsoleColor.DarkMagenta);


                    for (int i = 0; i < menuOptions.Length; i++)

                    {

                        ConsoleHelper.WriteWithColor(
                            (i == menuSelect ? $"=============================➡️[{menuNum}]" : "") + menuOptions[i] +
                            (i == menuSelect ? "◀️=============================" : ""), ConsoleColor.Blue);

                    }

                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1)
                    {
                        Console.Beep();
                        menuSelect++;
                        menuNum++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                    {
                        Console.Beep();
                        menuSelect--;
                        menuNum--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        switch (menuSelect)
                        {
                            case 0:
                                Console.Clear();
                                Console.Beep();
                                _ownerService.Create();

                                break;
                            case 1:
                                Console.Clear();
                                Console.Beep();
                                _ownerService.Update();
                                break;

                            case 2:
                                Console.Clear();
                                Console.Beep();
                                _ownerService.Delete();
                                break;
                            case 3:
                                Console.Clear();
                                Console.Beep();
                                _ownerService.GetAll();
                                break;
                            case 4:
                                MainMenu();
                                break;
                        }
                    }



                }

            }
        }

        public void DrugStoreMenu()

        {
            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(DrugstoreOptions));
                int menuSelect = 0;
                int menuNum = 1;
                while (true)
                {

                    Console.Clear();
                    Console.CursorVisible = false;
                    ConsoleHelper.WriteWithColor("Drugstore Options", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("-----------------", ConsoleColor.DarkMagenta);

                    for (int i = 0; i < menuOptions.Length; i++)

                    {

                        ConsoleHelper.WriteWithColor(
                            (i == menuSelect ? $"=========================>[{menuNum}]" : "") + menuOptions[i] +
                            (i == menuSelect ? "<=========================" : ""), ConsoleColor.Blue);

                    }

                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1)
                    {
                        Console.Beep();
                        menuSelect++;
                        menuNum++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                    {
                        Console.Beep();
                        menuSelect--;
                        menuNum--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        switch (menuSelect)
                        {
                            case 0:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Create();
                                //add exc
                                break;
                            case 1:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Update();
                                //need check and add exc
                                break;

                            case 2:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Delete();
                                //need check
                                break;
                            case 3:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.GetAll();
                                break;
                            case 4:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.GetAllDrugStoresByOwner();
                                //need check and add ext
                                break;
                            case 5:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Sale();
                                //go back here after Sale 
                                break;
                            case 6:
                                MainMenu();
                                break;
                            case 7:
                                Console.Beep();
                                Console.Clear();
                                Console.CursorVisible = true;
                                _adminService.Authorize();
                                break;
                        }
                    }
                }
            }
        }

        public void Drug()
        {
            while (true)
            {

                string[] menuOptions = Enum.GetNames(typeof(DrugOptions));
                int menuSelect = 0;
                int menuNum = 1;
                while (true)
                {

                    Console.Clear();
                    Console.CursorVisible = false;
                    ConsoleHelper.WriteWithColor("Drugstore Options", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("-----------------", ConsoleColor.DarkMagenta);

                    for (int i = 0; i < menuOptions.Length; i++)

                    {

                        ConsoleHelper.WriteWithColor(
                            (i == menuSelect ? $"=========================>[{menuNum}]" : "") + menuOptions[i] +
                            (i == menuSelect ? "<=========================" : ""), ConsoleColor.Blue);

                    }

                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1)
                    {
                        Console.Beep();
                        menuSelect++;
                        menuNum++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                    {
                        Console.Beep();
                        menuSelect--;
                        menuNum--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        switch (menuSelect)
                        {
                            case 0:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Create();
                                //add exc
                                break;
                            case 1:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Update();
                                //need check and add exc
                                break;

                            case 2:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Delete();
                                //need check
                                break;
                            case 3:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.GetAll();
                                break;
                            case 4:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.GetAllDrugStoresByOwner();
                                //need check and add ext
                                break;
                            case 5:
                                Console.Beep();
                                Console.Clear();
                                _drugstoreService.Sale();
                                //go back here after Sale 
                                break;
                            case 6:
                                MainMenu();
                                break;
                            case 7:
                                Console.Beep();
                                Console.Clear();
                                Console.CursorVisible = true;
                                _adminService.Authorize();
                                break;
                        }

                    }
                }
            }
        }
    }
}
