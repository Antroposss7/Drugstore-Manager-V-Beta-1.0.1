using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.Helpers;
using Presentation.Services;
using Data;
using Core;
using Core.Constants;
using Core.Entities;
using Data.Contexts;

namespace Presentation
{
    public static class Program
    {
        //private readonly static OwnerService1 _ownerService;
        private readonly static AdminService _adminService;
        private static Admin admin;
        private readonly static MenuServices _menuServices;


        static Program()
        {
            DbInitializer.SeedAdmins();
            _adminService = new AdminService();
            //_ownerService = new OwnerService1(admin);
            _menuServices = new MenuServices();

            Array myArray = Enum.GetValues(typeof(MainMenuOptions));
            Console.OutputEncoding = Encoding.UTF8;


        }

        static void Main(string[] args)
        {

            int eselection = 0;
            Console.Beep();
            Console.Beep(100, 50);
            admin = _adminService.Authorize();
            if (admin is not null)
            {
            _menuServices.MainMenu();
                Console.Beep();
            }


        }


    }



}







