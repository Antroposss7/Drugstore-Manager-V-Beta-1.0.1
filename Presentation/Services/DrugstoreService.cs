using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using System.Text.RegularExpressions;
using Core.Constants;
using Data.Contexts;

namespace Presentation.Services
{
    public class DrugstoreService
    {
        private readonly Admin admin;
        private readonly OwnerService1 _ownerService;
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly MenuServices _menuServices;

        public DrugstoreService(Admin admin)
        {
            this.admin = admin;

            _menuServices = new MenuServices();
            _ownerService = new OwnerService1(admin);
            _drugstoreRepository = new DrugstoreRepository();
            _ownerRepository = new OwnerRepository();

        }

        public void Create()
        {
            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create Owner fist!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to back to Main Menu", ConsoleColor.Cyan);
                Console.ReadKey();
                _menuServices.MainMenu();
            }
            ConsoleHelper.WriteWithCondition("Enter new Drugstore name: ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithCondition("Enter new Drugstore address: ", ConsoleColor.Cyan);
            string address = Console.ReadLine();
        ContactNumDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore contact number: ", ConsoleColor.Cyan);
            int contactNumber;
            bool isValid = int.TryParse(Console.ReadLine(), out contactNumber);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Contact number is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto ContactNumDesc;
            }
        EmailDesc: ConsoleHelper.WriteWithCondition("Enter new Drugstore Email address: ", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto EmailDesc;
            }
            if (_drugstoreRepository.isDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto EmailDesc;
            }

        GroupChooseDesc:
            Console.Clear();
            ConsoleHelper.WriteWithColor("Choose Owner from the list below: ", ConsoleColor.Cyan);
            Console.WriteLine();
            _ownerService.GetAll();
            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter Owner Id: ", ConsoleColor.Cyan);
            int ownerId;
            isValid = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Owner Id is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto GroupChooseDesc;
            }

            var owner = _ownerRepository.Get(ownerId);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("There is no Owner in this id! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto GroupChooseDesc;
            }

            var drugStore = new Drugstore
            {
                Name = name,
                Address = address,
                ContactNumber = contactNumber,
                Email = email,
                Owner = owner
            };

            _drugstoreRepository.Add(drugStore);
            owner.Drugstores.Add(drugStore);
            Console.Clear();
            ConsoleHelper.WriteWithColor($"Drugstore: {drugStore.Name} Which located at: {drugStore.Address} is successfully added!", ConsoleColor.Green);
            ConsoleHelper.WriteWithColor("Press any key to continue to back to menu", ConsoleColor.Cyan);
            Console.ReadKey();



        }

        public void GetAll()
        {
            var drugstores = _drugstoreRepository.GetAll();
            ConsoleHelper.WriteWithColor("---- All Drugstores ---");
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"| Drugstore ID: {drugstore.Id} | Name: {drugstore.Name} | Owner: {drugstore.Owner.Name} {drugstore.Owner.Surname} |", ConsoleColor.Cyan);
            }

            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to continue...", ConsoleColor.Cyan);
            Console.ReadKey();



        }

        public void Update()
        {
            GetAll();
        DrugstoreIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Drugstore Id:", ConsoleColor.Cyan);
            ///////////////// возможно неверно
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {

                ConsoleHelper.WriteWithColor("Wrong Drugstore Id format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugstoreIdDesc;
            }

            var drugStore = _drugstoreRepository.Get(id);
            if (drugStore is null)
            {


                ConsoleHelper.WriteWithColor("No any Drugstore with this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugstoreIdDesc;


            }
            Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Drugstore name:", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithCondition("Enter new Drugstore address: ", ConsoleColor.Cyan);
            string address = Console.ReadLine();
        ContactNumDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore contact number: ", ConsoleColor.Cyan);
            int contactNumber;
            isValid = int.TryParse(Console.ReadLine(), out contactNumber);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Contact number is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto ContactNumDesc;
            }
        EmailDesc: ConsoleHelper.WriteWithCondition("Enter new Drugstore Email address: ", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto EmailDesc;


            }
            if (_drugstoreRepository.isDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto EmailDesc;
            }

        OwnerIdDesc:
            var owners = _ownerRepository.GetAll();
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} | Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine();
            }
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Enter Owner ID: ", ConsoleColor.Cyan);
            int dbId;
            isValid = int.TryParse(Console.ReadLine(), out dbId);
            if (!isValid)
            {

                ConsoleHelper.WriteWithColor("Wrong Input! Enter Owner ID:  ", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to try again...", ConsoleColor.Cyan);
                Console.ReadKey();
                goto OwnerIdDesc;
            }
            var dbOwner = _ownerRepository.Get(dbId);
            if (dbOwner == null)
            {
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("There is no owner with this ID number.", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to try again...", ConsoleColor.Cyan);
                Console.ReadKey();
                goto OwnerIdDesc;
            }

            drugStore.Name = name;
            drugStore.Address = address;
            drugStore.Email = email;
            drugStore.ContactNumber = contactNumber;
            drugStore.Owner = dbOwner;
            drugStore.OwnerId = dbId;
            ConsoleHelper.WriteWithColor($"| Drugstore ID: {drugStore.Id} | Name: {drugStore.Name} | Owner: {drugStore.Name} {drugStore.Owner.Surname} is successfully updated! |", ConsoleColor.Green);

            _drugstoreRepository.Update(drugStore);

            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to back to Owner Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();



        }

        public void Delete()
        {
            GetAll();
        DrugStoreIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Drugstore Id to delete:", ConsoleColor.Cyan);
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Wrong Id format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto DrugStoreIdDesc;
            }

            var drugStore = _drugstoreRepository.Get(id);
            if (drugStore == null)
            {
                ConsoleHelper.WriteWithColor("There is no any Drugstore in this Id!", ConsoleColor.Red);
                Console.ReadKey();
                goto DrugStoreIdDesc;
            }
            _drugstoreRepository.Delete(drugStore);
            ConsoleHelper.WriteWithColor($"Drugstore :{drugStore.Name} | Address: {drugStore.Address} is successfully deleted!", ConsoleColor.Green);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key continue...", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        public void GetAllDrugStoresByOwner()
        {
            var owners = _ownerRepository.GetAll();
            if (owners.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Owner in database! | Press any key to go to Owner Menu...", ConsoleColor.Red);
                Console.ReadKey();
                _menuServices.OwnerMenu();
            }

            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner Id: {owner.Id} | Owner Name: {owner.Name} | Owner Surname: {owner.Surname}", ConsoleColor.Cyan);
            }
        OwnerIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Owner's Id:", ConsoleColor.Cyan);
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Wrong Id format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto OwnerIdDesc;
            }

            var dbOwner = _ownerRepository.Get(id);
            if (dbOwner == null)
            {
                ConsoleHelper.WriteWithColor("There is no any Owner in this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto OwnerIdDesc;
            }
            else
            {
                ConsoleHelper.WriteWithColor("---- Drugstores by Owner ----", ConsoleColor.Cyan);
                foreach (var drugstore in dbOwner.Drugstores)
                {
                    ConsoleHelper.WriteWithColor($"Selected Owner's Drugstore Id: {drugstore.Id} | Name {drugstore.Name} | Email {drugstore.Email} | Address: {drugstore.Address}", ConsoleColor.Cyan);
                }
                ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("Press any key to go to Main Menu", ConsoleColor.Cyan);
                Console.ReadKey();
                _menuServices.MainMenu();
            }












        }

        public void Sale()
        {

        }
    }









}
