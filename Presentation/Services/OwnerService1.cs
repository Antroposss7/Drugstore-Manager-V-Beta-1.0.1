using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Data.Contexts.Repositories.Abstract;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class OwnerService1
    {
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugstoreRepository _drugstoreRepository;

        public OwnerService1(Admin admin)
        {
            _drugstoreRepository = new DrugstoreRepository();
            _ownerRepository = new OwnerRepository();
            Console.CursorVisible = true;
        }

        public void Create()
        {

        OwnerNameDesc:
        
            ConsoleHelper.WriteWithCondition("New owner's Name: ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithCondition("New owner's Surname: ", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            var owner = new Owner
            {
                Name = name,
                Surname = surname,
            };
            _ownerRepository.Add(owner);
            Console.Clear();
            ConsoleHelper.WriteWithColor($"New owner: {owner.Name} {owner.Surname} is successfully created!", ConsoleColor.Green);
            ConsoleHelper.WriteWithCondition("Press any key to back Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }

        public void GetAll()
        {
            var owners = _ownerRepository.GetAll();
            ConsoleHelper.WriteWithColor("---- All Owners ----", ConsoleColor.Cyan);
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($" Owner ID: {owner.Id}, Owner Name: {owner.Name}, Owner Surname: {owner.Surname}.", ConsoleColor.DarkCyan);
            }
            ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.Cyan);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to continue", ConsoleColor.Cyan);

            Console.ReadKey();
            //исправить баг путем добавления дополнительного условия, которое запрашивает определенную цифру для возврата в меню
            //ИСПРАВЛЕН
        } 

        public void Delete()
        {
            GetAll();
        OwnerIdDescription:
            ConsoleHelper.WriteWithCondition("Enter Owner's Id: ", ConsoleColor.Cyan);
            int id;
            bool IsValid = int.TryParse(Console.ReadLine(), out id);
            if (!IsValid)
            {
                ConsoleHelper.WriteWithColor($"Wrong Id Format! Press any key to try again...",ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerIdDescription;



            }

            var dbOwner = _ownerRepository.Get(id);
            if (dbOwner == null)
            {
                ConsoleHelper.WriteWithColor("No any Owner with this Id! Press any key to try again...");
                Console.ReadKey();
                goto OwnerIdDescription;
            }
            _ownerRepository.Delete(dbOwner);
            ConsoleHelper.WriteWithColor($"Owner Id: {dbOwner.Id},Owner Name: {dbOwner.Name},Owner Surname {dbOwner.Surname} is Successfully Deleted!", ConsoleColor.DarkGreen);
            Console.WriteLine();
            //add
        }

        public void Update()
        {
            GetAll();
           
        EnterOwnerIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Owner's Id:",ConsoleColor.Cyan);
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
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

            ConsoleHelper.WriteWithCondition("Enter new Owner name:", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithCondition("Enter new Owner surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            ConsoleHelper.WriteWithColor($"Owner Id: {owner.Id}, Owner Name:{owner.Name}, Surname: {owner.Surname} is successfully updated!", ConsoleColor.Green);
            owner.Name = name;
            owner.Surname = surname;
            _ownerRepository.Update(owner);
            Console.WriteLine("\n");
            ConsoleHelper.WriteWithColor("Press any key to back to Owner Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();



            //dbTeacher.Groups.Add(group);
            //dbGroupField.Groups.Add(group);
        }
    }









}


