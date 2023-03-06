using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Contexts.Repositories.Abstract;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class OwnerService
    {
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly MenuServices _menuServices;


        public OwnerService(Admin admin)
        {
            _drugstoreRepository = new DrugstoreRepository();
            _ownerRepository = new OwnerRepository();
            _menuServices = new MenuServices();

        }


        public void Create()
        {
        OwnerNameDesc:



        Console.WriteLine();
            ConsoleHelper.WriteWithCondition("New owner's Name: ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            if (!name.CheckString())
            {
                Console.Write("\n\n");
                ConsoleHelper.WriteWithColor("Owner name is not in correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerNameDesc;
            }

            Console.Clear();
        OwnerSurnameDesc:
        Console.WriteLine();
            ConsoleHelper.WriteWithCondition("New owner's Surname: ", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            if (!surname.CheckString())
            {
                Console.Write("\n\n");
                
                ConsoleHelper.WriteWithColor("New owner surname is not in correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerSurnameDesc;
            }

            var owner = new Owner
            {
                Name = name,
                Surname = surname,
            };
            _ownerRepository.Add(owner);
            Console.Clear();
            Console.Write("\n");
            ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.DarkCyan);
Console.Write("\n\n");
            ConsoleHelper.WriteWithColor($"New owner: {owner.Name} {owner.Surname} is successfully created!", ConsoleColor.Green);
Console.Write("\n\n");
ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.DarkCyan);

            ConsoleHelper.WriteWithColor("Press any key to back Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }
        public void GetAll()//+
        {

            
            var owners = _ownerRepository.GetAll();

            if (owners.Count == 0)
            {
                ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.DarkCyan);

                ConsoleHelper.WriteWithColor("There is no any Owner!", ConsoleColor.Red);
                Console.Write("");
                ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.DarkCyan);

                ConsoleHelper.WriteWithColor("Press any key to go to the Owner Menu...", ConsoleColor.Cyan);
                Console.ReadKey();
                _menuServices.OwnerMenu();
            }
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($" Owner ID: {owner.Id}, Owner Name: {owner.Name}, Owner Surname: {owner.Surname}.", ConsoleColor.DarkCyan);
            }
            ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to continue", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
            return;

        }

        public void Delete()//+
        {
        OwnerIdDescription:
            GetAll();
            Console.Write("");
            ConsoleHelper.WriteWithCondition("Enter Owner's Id: ", ConsoleColor.Cyan);
            int id;
            var isValid = int.TryParse(Console.ReadLine(), out id);
            //////////////////////// изменить экстеншн!!!!!!!!!!
            if (id.CheckInt())
            {
                ConsoleHelper.WriteWithColor($"Wrong Id Format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerIdDescription;

            }
            Console.Clear();
            var dbOwner = _ownerRepository.Get(id);
            if (dbOwner == null)
            {
                ConsoleHelper.WriteWithColor("No any Owner with this Id! Press any key to try again...");
                Console.ReadKey();
                Console.Clear();
                goto OwnerIdDescription;
            }
            _ownerRepository.Delete(dbOwner);
            ConsoleHelper.WriteWithColor($"Owner Id: {dbOwner.Id},Owner Name: {dbOwner.Name},Owner Surname {dbOwner.Surname} is Successfully Deleted!", ConsoleColor.DarkGreen);
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }

        public void Update()//+
        {
            GetAll();
            Console.Write("");
        EnterOwnerIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Owner's Id:", ConsoleColor.Cyan);
            int id; //+
            int.TryParse(Console.ReadLine(), out id);
            if (id.CheckInt())
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Wrong Owner Id format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterOwnerIdDesc;
            }
            var owner = _ownerRepository.Get(id);
            if (owner == null)
            {

                ConsoleHelper.WriteWithColor("No any Owner with this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterOwnerIdDesc;
            }

        OwnerNameDesc:
        Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Owner name:", ConsoleColor.Cyan);
            
            string name = Console.ReadLine();
            Console.Clear();
            if (!name.CheckString())
            {
                
                ConsoleHelper.WriteWithColor("Owner name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerNameDesc;

            }
            Console.Clear();
        OwnerSurnameDesc:
            ConsoleHelper.WriteWithCondition("Enter new Owner surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            if (!name.CheckString())
            {
                ConsoleHelper.WriteWithColor("Owner surname is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerSurnameDesc;
            }
            Console.Clear();
            ConsoleHelper.WriteWithColor($"Owner Id: {owner.Id}, Owner Name:{owner.Name}, Surname: {owner.Surname} is successfully updated!", ConsoleColor.Green);
            owner.Name = name;
            owner.Surname = surname;
            _ownerRepository.Update(owner);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to back to Owner Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();




            //dbGroupField.Groups.Add(group);
        }
    }









}


