using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using System.Text.RegularExpressions;
using Core.Constants;
using Data.Contexts;
using Data.Contexts.Repositories.Abstract;

namespace Presentation.Services
{
    public class DrugstoreService
    {
        private readonly Admin admin;
        private readonly OwnerService _ownerService;
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly MenuServices _menuServices;
        private readonly DrugRepository _drugRepository;

        public DrugstoreService(Admin admin)
        {
            this.admin = admin;
            _drugstoreRepository = new DrugstoreRepository();
            _menuServices = new MenuServices();
            _ownerService = new OwnerService(admin);
            _ownerRepository = new OwnerRepository();
            _drugRepository = new DrugRepository();

        }
        
        public void Create()
        {

            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create Owner first!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to back to Owner Menu", ConsoleColor.Cyan);
                Console.ReadKey();
                _menuServices.OwnerMenu();
            }

        DrugstoreNameDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore name: ", ConsoleColor.Cyan);

            string name = Console.ReadLine();
            if (!name.CheckString())
            {
                ConsoleHelper.WriteWithColor("Drugstore name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugstoreNameDesc;

            }
            Console.Clear();
        DrugStoreAddrDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore address: ", ConsoleColor.Cyan);
            string address = Console.ReadLine();


            if (String.IsNullOrEmpty(address))
            {
                ConsoleHelper.WriteWithColor("Drugstore address can not be empty! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugStoreAddrDesc;
            }
            Console.Clear();

        //+
        ContactNumDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore contact number in international format: ", ConsoleColor.Cyan);
            string contactNumber = Console.ReadLine();
            if (String.IsNullOrEmpty(contactNumber) || String.IsNullOrWhiteSpace(contactNumber))
            {
                ConsoleHelper.WriteWithColor("Drugstore contact number can not be empty! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugStoreAddrDesc;
            }
            if (!contactNumber.IsPhoneNumber())
            {

                ConsoleHelper.WriteWithColor("Contact number is not correct international format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto ContactNumDesc;
            }
            Console.Clear();
        EmailDesc:
            Console.Clear();
            //+
            ConsoleHelper.WriteWithCondition("Enter new Drugstore Email address: ", ConsoleColor.Cyan);

            string email = Console.ReadLine();

            if (String.IsNullOrEmpty(email) || String.IsNullOrWhiteSpace(email))
            {
                Console.CursorVisible = false;
                ConsoleHelper.WriteWithColor("Email can not be empty! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EmailDesc;
            }

            if (!email.IsEmail())
            {
                Console.CursorVisible = false;
                ConsoleHelper.WriteWithColor("Email is not correct format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EmailDesc;
            }

            if (_drugstoreRepository.isDublicatedEmail(email))
            {
                Console.CursorVisible = false;
                ConsoleHelper.WriteWithColor("This email is already used! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                goto EmailDesc;
            }
            Console.Clear();
        GroupChooseDesc:
            Console.Clear();
            ConsoleHelper.WriteWithColor("Choose Owner from the list below: ", ConsoleColor.Cyan);
            Console.WriteLine();
            _ownerService.GetAll();
            Console.WriteLine();


            ConsoleHelper.WriteWithCondition("Enter Owner Id: ", ConsoleColor.Cyan);
            int ownerId;
            bool isValid = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isValid)
            {
                Console.CursorVisible = false;
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
            Console.CursorVisible = false;
            ConsoleHelper.WriteWithColor($"Drugstore: {drugStore.Name} Which located at: {drugStore.Address} is successfully added!", ConsoleColor.Green);
            ConsoleHelper.WriteWithColor("Press any key to go to the main menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();



        }

        public void GetAll() 
        {
            var drugstores = _drugstoreRepository.GetAll();
            ConsoleHelper.WriteWithColor("---- All Drugstores ----");
            if (drugstores.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Drugstore!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to go to the Drugstore...", ConsoleColor.Cyan);
                Console.ReadKey();
                _menuServices.DrugStoreMenu();
            }
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
        DrugstoreIdDesc:
            GetAll();

            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter Drugstore Id:", ConsoleColor.Cyan);
            ///////////////// возможно неверно
            int id;
            int.TryParse(Console.ReadLine(), out id);
            if (id.CheckInt())
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
        UpdateDSNameDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drugstore name for update:", ConsoleColor.Cyan);

            string name = Console.ReadLine();
            if (!name.CheckString())
            {
                ConsoleHelper.WriteWithColor("Drugstore name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UpdateDSNameDesc;

            }
        UpdateDSAdressDesc:
            Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Drugstore address: ", ConsoleColor.Cyan);
            string address = Console.ReadLine();
           
            if (String.IsNullOrEmpty(address) || String.IsNullOrWhiteSpace(address))
            {
                ConsoleHelper.WriteWithColor("Drugstore address can not be empty! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UpdateDSAdressDesc;
            }
        ContactNumDesc:
            Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Drugstore contact number: ", ConsoleColor.Cyan);
            string contactNumber = Console.ReadLine();
            if (String.IsNullOrEmpty(contactNumber) || String.IsNullOrWhiteSpace(contactNumber))
            {
                ConsoleHelper.WriteWithColor("Drugstore contact number can not be empty! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto ContactNumDesc;
            }
            if (!contactNumber.IsPhoneNumber())
            {

                ConsoleHelper.WriteWithColor("Contact number is not correct international format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto ContactNumDesc;
            }
        EmailDesc:
            Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Drugstore Email address: ", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EmailDesc;

            }
            Console.Clear();
            if (_drugstoreRepository.isDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EmailDesc;
            }

        OwnerIdDesc:
            Console.Clear();
            var owners = _ownerRepository.GetAll();
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner ID : {owner.Id} | Fullname : {owner.Name} {owner.Surname}", ConsoleColor.Cyan);
                Console.WriteLine();
            }
            Console.WriteLine();

            ConsoleHelper.WriteWithCondition("Enter Owner ID: ", ConsoleColor.Cyan);
            int dbId;
            int.TryParse(Console.ReadLine(), out dbId);

            if (dbId.CheckInt())
            {
                ConsoleHelper.WriteWithColor($"Wrong Id Format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
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
                Console.Clear();
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

        public void Delete()//+
        {
            GetAll();
        DrugStoreIdDesc:
            ConsoleHelper.WriteWithCondition("Enter Drugstore Id to delete:", ConsoleColor.Cyan);
            int dbId;
            int.TryParse(Console.ReadLine(), out dbId);

            if (dbId.CheckInt())
            {
                ConsoleHelper.WriteWithColor($"Wrong Id Format! Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugStoreIdDesc;

            }

            var drugStore = _drugstoreRepository.Get(dbId);
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
            Console.Clear();

        }

        public void GetAllDrugStoresByOwner()//+
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
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithCondition("Enter Owner's Id:", ConsoleColor.Cyan);
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Wrong Owner ID format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerIdDesc;
            }
            var ownerGet = _ownerRepository.Get(id);
            if (ownerGet == null)
            {

                ConsoleHelper.WriteWithColor("No any Owner with this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto OwnerIdDesc;
            }
            
            Console.Clear();
            ConsoleHelper.WriteWithColor("---- Drugstores by Owner ----", ConsoleColor.Cyan);
            if (ownerGet.Drugstores.Count == 0)
            {
                ConsoleHelper.WriteWithColor("No any Owner's Drugstore! | Press any key to return Drugstore Menu...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugStoreMenu();
            }
            foreach (var drugstore in ownerGet.Drugstores)
            {
                ConsoleHelper.WriteWithColor($"Selected Owner's Drugstore Id: {drugstore.Id} | Name {drugstore.Name} | Email {drugstore.Email} | Address: {drugstore.Address}", ConsoleColor.Cyan);
            }
            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("Press any key to go to Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
            _menuServices.MainMenu();

        }

        public void Sale()
        {
            Console.Clear();
            ConsoleHelper.WriteWithColor("SALE", ConsoleColor.Yellow);
            Console.WriteLine();

            var drugs = _drugRepository.GetAll();
            if (drugs.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There are no drugs to sell!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to go to the Drug Menu.", ConsoleColor.Cyan);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugMenu();
            }


            ConsoleHelper.WriteWithColor("Available drugs:", ConsoleColor.Cyan);
            Console.WriteLine();
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"ID: {drug.Id} | Name: {drug.Name} | Price: {drug.Price} | Count: {drug.Count}", ConsoleColor.Cyan);
            }


            ConsoleHelper.WriteWithCondition("Enter Drug ID: ", ConsoleColor.Yellow);
            int drugId;
            int.TryParse(Console.ReadLine(), out drugId);
            var chosenDrug = _drugRepository.Get(drugId);
            if (chosenDrug == null)
            {
                ConsoleHelper.WriteWithColor("Drug not found! Press any key to go back to the Drugstore Menu.", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugStoreMenu();
            }

            ConsoleHelper.WriteWithColor($"Price per {chosenDrug.Name}: {chosenDrug.Price}", ConsoleColor.Yellow);
            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter amount to buy: ", ConsoleColor.Yellow);
            int amout;
            int.TryParse(Console.ReadLine(), out amout);
            if (amout <= 0)
            {
                ConsoleHelper.WriteWithColor("Amount should be greater than 0! Press any key to go back to the Drugstore Menu.", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                return;
            }


            if (chosenDrug.Count < amout)
            {
                ConsoleHelper.WriteWithColor($"Only {chosenDrug.Count} {chosenDrug.Name} left in the stock! Press any key to go back to the Drugstore Menu.", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                return;
            }


            decimal totalAmount = amout * chosenDrug.Price;
            chosenDrug.Count -= amout;

            ConsoleHelper.WriteWithColor($"Total price: {totalAmount}", ConsoleColor.Green);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Sale completed successfully! Press any key to go back to the Drugstore Menu.", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }
    }









}
