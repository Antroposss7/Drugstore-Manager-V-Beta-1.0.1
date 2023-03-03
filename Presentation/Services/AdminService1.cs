using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class AdminService1
    {
        public readonly AdminRepository _adminRepository;

        public AdminService1()
        {
            _adminRepository = new AdminRepository();
        }

        public Admin Authorize()
        {


        LoginDescription:
            Console.WriteLine("\n\n\n");
            ConsoleHelper.WriteWithColor("──▄▀▀▀▄───────────────", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("──█───█───────────────", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("─███████─────────▄▀▀▄─", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("░██─▀─██░░█▀█▀▀▀▀█░░█░", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("░███▄███░░▀░▀░░░░░▀▀░░", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("░░░░░░░░░░░░░░░░░░░░░░", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("╭━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━╮", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("╰━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━╯", ConsoleColor.Blue);

            Console.WriteLine();
            ConsoleHelper.WriteWithColor(" ╭━┳━━┳━━┳━━┳━━┳━━┳ █░░ █▀█ █▀▀   █ █▄░█   ▀█▀ █▀█   █▀ █▄█ █▀ ▀█▀ █▀▀ █▀▄▀█ ┳━━┳━━┳━━┳━━┳━━┳━┳━╮", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor(" ╰━┻━━┻━━┻━━┻━━┻━━┻ █▄▄ █▄█ █▄█   █ █░▀█   ░█░ █▄█   ▄█ ░█░ ▄█ ░█░ ██▄ █░▀░█ ┻━━┻━━┻━━┻━━┻━━┻━┻━╯", ConsoleColor.Blue);

            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Username:", ConsoleColor.Blue);
            string username = Console.ReadLine();
         

            ConsoleHelper.WriteWithCondition("Password:", ConsoleColor.Blue);
            string password = Console.ReadLine();

            
           





            var admin = _adminRepository.GetByUsernameAndPassword(username, password);
            if (admin is null)
            {


                ConsoleHelper.WriteWithCondition("Username or password is incorrect! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();

                Console.Clear();

                goto LoginDescription;
            }
            return admin;
        }

    }


}






