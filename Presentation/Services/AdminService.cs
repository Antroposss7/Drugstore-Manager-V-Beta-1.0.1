using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Contexts;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    
    public class AdminService
    {
        public readonly AdminRepository _adminRepository;

        public AdminService()
        {
            DbInitializer.SeedAdmins();
            _adminRepository = new AdminRepository();
            Type type = typeof(ConsoleColor);
            
            Console.Clear();
        }

        public Admin Authorize()
        {
           
        LoginDescription:
            Console.WriteLine();
        ConsoleHelper.WriteWithColor("                 𝔻𝕣𝕦𝕘𝕤𝕥𝕠𝕣𝕖 𝕄𝕒𝕟𝕒𝕘𝕖𝕣 ©️  ", ConsoleColor.Magenta);
        Console.WriteLine();
        ConsoleHelper.WriteWithColor("Version: Beta 1.0.1  ", ConsoleColor.Magenta);

            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("──▄▀▀▀▄───────────────", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("──█───█───────────────", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("─███████─────────▄▀▀▄─", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("░██─▀─██░░█▀█▀▀▀▀█░░█░", ConsoleColor.Yellow);
            ConsoleHelper.WriteWithColor("░███▄███░░▀░▀░░░░░▀▀░░", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("░░░░░░░░░░░░░░░░░░░░░░", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("╭━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━╮",
                ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor(
                "╰━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━╯",
                ConsoleColor.Blue);

            Console.WriteLine();
            ConsoleHelper.WriteWithColor(
                " ╭━┳━━┳━━┳━━┳━━┳━━┳ █░░ █▀█ █▀▀   █ █▄░█   ▀█▀ █▀█   █▀ █▄█ █▀ ▀█▀ █▀▀ █▀▄▀█ ┳━━┳━━┳━━┳━━┳━━┳━┳━╮",
                ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor(
                " ╰━┻━━┻━━┻━━┻━━┻━━┻ █▄▄ █▄█ █▄█   █ █░▀█   ░█░ █▄█   ▄█ ░█░ ▄█ ░█░ ██▄ █░▀░█ ┻━━┻━━┻━━┻━━┻━━┻━┻━╯",
                ConsoleColor.Blue);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor(" 》Created by Antropos7 《    ", ConsoleColor.DarkMagenta);
            Console.WriteLine();
           
            

            ConsoleHelper.WriteWithColor("︾︾︾︾︾︾︾︾︾︾           ", ConsoleColor.Blue);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            
            ConsoleHelper.WriteWithCondition("\U0001f978 Username: ", ConsoleColor.DarkCyan);
            Console.ForegroundColor = ConsoleColor.Cyan;
            string username = Console.ReadLine();
           
            ConsoleHelper.WriteWithCondition("🔑 Password: ", ConsoleColor.DarkRed);


            var passwordInput = string.Empty;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && passwordInput.Length > 0)
                {
                    Console.Write("\b \b");
                    passwordInput = passwordInput[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("○");
                    passwordInput += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
           

            

            var admin = _adminRepository.GetByUsernameAndPassword(username, passwordInput);
            if (admin is null)
            {

                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor("Username or password is incorrect! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Beep(850,200);
                Console.Clear();

                goto LoginDescription;
            }

            Console.Clear();
            return admin;
        }
    }
}

















