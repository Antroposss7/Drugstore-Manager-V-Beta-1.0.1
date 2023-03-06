using Core.Entities;
using Core.Helpers;
using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using System.Text.RegularExpressions;
using Core.Extensions;
using Data.Contexts.Repositories.Abstract;

namespace Presentation.Services
{
    public class DrugService
    {
        private readonly Admin admin;
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly DrugRepository _drugRepository;
        private readonly MenuServices _menuServices;
        private readonly DrugstoreService _drugstoreService;
        private readonly OwnerRepository _ownerRepository;

        public DrugService()
        {
            this.admin = admin;
            _menuServices = new MenuServices();
            _drugstoreRepository = new DrugstoreRepository();
            _drugRepository = new DrugRepository();
            _drugstoreService = new DrugstoreService(admin);
            _ownerRepository = new OwnerRepository();
        }



        public void Create()
        {


            if (_drugstoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There are no Drugstores to add drugs to! Please create a Drugstore first!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to go back to the Drug Store.", ConsoleColor.Cyan);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugStoreMenu();
            }
            Console.Clear();
        DrugNameDesc: //+
        Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter new Drug name: ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            if (!name.CheckString())
            {

                ConsoleHelper.WriteWithColor("Drug name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugNameDesc;
            }
            Console.Clear();
        DrugDescDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drug description: ", ConsoleColor.Cyan);
            string description = Console.ReadLine();
            if (!description.CheckString()) //+
            {

                ConsoleHelper.WriteWithColor("Drug name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugDescDesc;
            }
            Console.Clear();
        PriceDesc:
            ConsoleHelper.WriteWithCondition("Enter new Drug price: ", ConsoleColor.Cyan);
            int price; //+
            bool isValid = int.TryParse(Console.ReadLine(), out price);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Price is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto PriceDesc;
            }
            Console.Clear();
            if (!(price > 0))
            {

                ConsoleHelper.WriteWithColor("Price can not be negative or 0! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto PriceDesc;
            }
            CountDesc:
            ConsoleHelper.WriteWithCondition("Enter Drug count:",ConsoleColor.Cyan);
            int count; //+
            isValid = int.TryParse(Console.ReadLine(), out count);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Count is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto CountDesc;
            }
            Console.Clear();
            if (!(count > 0))
            {

                ConsoleHelper.WriteWithColor("Count can not be negative or 0! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto CountDesc;
            }

            Console.Clear();



            ConsoleHelper.WriteWithColor("Choose a Drugstore to add the new drugs to: ", ConsoleColor.Cyan);
            Console.WriteLine();
            var drugStores = _drugstoreRepository.GetAll();
        DrugStoreIdDesc:
            foreach (var drugStore in drugStores)
            {
                ConsoleHelper.WriteWithColor(
                    $"| Drugstore ID: {drugStore.Id} | Name:{drugStore.Name} | Owner: {drugStore.Owner.Name} {drugStore.Owner.Surname} |",
                    ConsoleColor.Cyan);
            }

            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter Drugstore ID: ", ConsoleColor.Cyan);
            int drugStoreId; // is null
           isValid = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Drugstore ID is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugStoreIdDesc;
            }
            Console.Clear();
            var drugstore = _drugstoreRepository.Get(drugStoreId);
            if (drugstore == null)
            {
                ConsoleHelper.WriteWithColor("No Drugstore with this ID found! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugStoreIdDesc;
            }

            var drug = new Drug
            {
                Count = count,
                Name = name,
                Description = description,
                Price = price,
                Drugstore = drugstore
            };

            _drugRepository.Add(drug);
            drugstore.Drugs.Add(drug);
            Console.Clear();
            ConsoleHelper.WriteWithColor($"Drug [{drug.Name}] with Price [{drug.Price}] and Count [{drug.Count}] added to {drugstore.Name}.",
                ConsoleColor.Green);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to the main menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }

        public void GetAll()
        {
            ConsoleHelper.WriteWithColor("---- All Drugs ---");
            var drugs = _drugRepository.GetAll();
            if (drugs.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any drugs!", ConsoleColor.Red);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("Press any key to go to the Drug Menu...", ConsoleColor.Cyan);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugMenu();
            }

            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor(
                    $"| Drug ID: {drug.Id} | Name: {drug.Name} | Description: {drug.Description} | Price: {drug.Price} | Count: {drug.Count} | Drugstore: {drug.Drugstore.Name} |",
                    ConsoleColor.Cyan);
            }

            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to continue...", ConsoleColor.Green);
            Console.ReadKey();
            Console.Clear();
            
        }

        public void Update()//+
        {
            var drugs = _drugRepository.GetAll();
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithColor("---- All Drugs ----");
            foreach (var dr in drugs)
            {
                ConsoleHelper.WriteWithColor(
                    $"| Drugstore ID: {dr.Id} | Name:{dr.Name} | Description {dr.Description} | Price: {dr.Price} | Drugstore: {dr.Drugstore.Name}",
                    ConsoleColor.Cyan);
            }
            Console.WriteLine();

        DrugIdDesc:
            ConsoleHelper.WriteWithCondition("Enter drug ID to update: ", ConsoleColor.Cyan);
            int id;
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("Drug ID is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugIdDesc;
            }

            if (!id.IsPositiveInt())
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("Id number must be positive! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugIdDesc;
            }

            var drug = _drugRepository.Get(id);
            if (drug == null)
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("No drug with this ID found! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugIdDesc;
            }
            Console.Clear();
        UpDrugName:
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithCondition($"Enter new name for {drug.Name}:", ConsoleColor.Cyan);
            string name = Console.ReadLine(); //+
            if (!name.CheckString())
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("New drug name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UpDrugName;
            }
            Console.Clear();
        //if (!string.IsNullOrWhiteSpace(name))

        UpDrugDescDesc:
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithCondition($"Enter new description for {drug.Name}:", ConsoleColor.Cyan);
            string description = Console.ReadLine();
            if (!description.CheckString()) //+
            {
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("Drug description is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UpDrugDescDesc;
            }
            Console.Clear();

        UpDrugPriceDesc:
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithCondition($"Enter new price for {drug.Name}: ", ConsoleColor.Cyan);

            int price; //+
            isValid = int.TryParse(Console.ReadLine(), out price);
            {

                if (!isValid)
                {
                    ConsoleHelper.WriteWithColor("Price is not in correct format! Press any key to try again...",
                        ConsoleColor.Red);
                    Console.ReadKey();
                    Console.Clear();
                    goto UpDrugDescDesc;
                }
                Console.Clear();
                if (!(price > 0))
                {

                    ConsoleHelper.WriteWithColor("Price can not be negative or 0! Press any key to try again...",
                        ConsoleColor.Red);
                    Console.ReadKey();
                    Console.Clear();
                    goto UpDrugDescDesc;
                }

                UCountDesc:
                ConsoleHelper.WriteWithCondition("Enter new Drug count:",ConsoleColor.Cyan);
                int count; //+
                isValid = int.TryParse(Console.ReadLine(), out count);
                if (!isValid)
                {
                    ConsoleHelper.WriteWithColor("Count is not in correct format! Press any key to try again...",
                        ConsoleColor.Red);
                    Console.ReadKey();
                    Console.Clear();
                    goto UCountDesc;
                }
                Console.Clear();
                if (!(count > 0))
                {

                    ConsoleHelper.WriteWithColor("Count can not be negative or 0! Press any key to try again...",
                        ConsoleColor.Red);
                    Console.ReadKey();
                    Console.Clear();
                    goto UCountDesc;
                }
                Console.Clear();
                drug.Description = description;
                drug.Name = name;
                drug.Price = price;

                var newPrice = price;
                var newName = name;

                _drugRepository.Update(drug);
                Console.Clear();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor($"▼▼▼ Old Drug [{drug.Name}] with ID [{drug.Id}], Price [{drug.Price}] Count[{drug.Count}] and Drugstore: {drug.Drugstore.Name} has been updated to ▼▼▼:",
                    ConsoleColor.DarkGreen);
                Console.WriteLine("\n");
                ConsoleHelper.WriteWithColor(
                    $"Drug [{newName}] with the same ID [{drug.Id}] and Price [{newPrice}] Drugstore: {drug.Drugstore.Name}:",
                    ConsoleColor.Green);
                // нужно красиво сделать
                Console.WriteLine();
                Console.WriteLine("\n\n");
                ConsoleHelper.WriteWithColor("Press any key to go to the main menu", ConsoleColor.Cyan);
                Console.ReadKey();

            }
        }

        public void Delete()//+
        {

            GetAll();
            DrugIdDesc:
            ConsoleHelper.WriteWithCondition("Enter drug ID to delete: ", ConsoleColor.Cyan);
            int id; //+
            int.TryParse(Console.ReadLine(), out id);
            if (!id.CheckInt())
            {
                ConsoleHelper.WriteWithColor("Wrong Drug ID format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugIdDesc;
            }

            var drug = _drugRepository.Get(id);
            if (drug == null)
            {
                ConsoleHelper.WriteWithColor("No drug with this ID found! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugIdDesc;
            }
            Console.Clear();
            _drugRepository.Delete(drug);
            ConsoleHelper.WriteWithColor(
                $"| Drug ID: {drug.Id} | Name:{drug.Name} | Description {drug.Description} is successfully deleted!",
                ConsoleColor.Green);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key continue...", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        public void GetAllDrugsByDrugstore() //+
        {
            var drugStores = _drugstoreRepository.GetAll();
            if (drugStores.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Drugstore in database! | Press any key to go to Drug Store Menu...", ConsoleColor.Red);
                Console.ReadKey();
                _menuServices.DrugStoreMenu();
            }
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithColor("---- All Drugstores ----", ConsoleColor.Cyan);
            foreach (var drugStore in drugStores)
            {
                ConsoleHelper.WriteWithColor($"| Drugstore Id: {drugStore.Id} | Name {drugStore.Name} | Email {drugStore.Email} | Address: {drugStore.Address} |", ConsoleColor.Cyan);
            }
            Console.WriteLine();


        DSIdDesc:
        ConsoleHelper.WriteWithCondition("Enter Drug Store Id:",ConsoleColor.Cyan);    
        int id; // +

            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Wrong Drug Store Id format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DSIdDesc;
            }
            Console.Clear();
            var dbDrugStore = _drugstoreRepository.Get(id);

            if (dbDrugStore == null)
            {
                ConsoleHelper.WriteWithColor("There is no any Drugstore in this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DSIdDesc;
            }
            Console.Clear();

            if (dbDrugStore.Drugs == null || dbDrugStore.Drugs.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Owner in this Id! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DSIdDesc;
            }
            Console.Clear();
            ConsoleHelper.WriteWithColor("---- Drugs by Drugstore ----", ConsoleColor.Cyan);
            foreach (var drug in dbDrugStore.Drugs)
            {
                ConsoleHelper.WriteWithColor(
                    $"| Drug ID: {drug.Id} | Name: {drug.Name} | Description: {drug.Description} | Price: {drug.Price}  |",
                    ConsoleColor.Cyan);
            }
            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("Press any key to go to Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
            _menuServices.MainMenu();
        }// call

        public void FilterDrugs()//+
        {
            var druge = _drugRepository.GetAll();
            if (druge.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Drug in database! | Press any key to go to Drug Menu...", ConsoleColor.Red);
                Console.ReadKey();
                
                _menuServices.DrugMenu();
            }

        DrugstoreIdDesc:
            _drugstoreService.GetAll();

            ConsoleHelper.WriteWithColor("----------------------------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithCondition("Select Drugstore by ID:", ConsoleColor.Cyan);
            int id; //+
            
            bool IsValid = int.TryParse(Console.ReadLine(), out id);
            if (!IsValid)
            {
                ConsoleHelper.WriteWithColor("Wrong Id format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugstoreIdDesc;
            }
            
            var drugStoreId = _drugstoreRepository.Get(id);
            if (drugStoreId == null)
            {
                ConsoleHelper.WriteWithColor("There is no any Drug Store in this id! | Press any key to go to continue...", ConsoleColor.Red);
                Console.ReadKey();
            Console.Clear();    
                goto DrugstoreIdDesc;
            }
            
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithColor($"---- Filter Drugs by Price ----", ConsoleColor.Cyan);
            Console.WriteLine("\n");
            _drugRepository.GetAll();
        DrugPriceDesc:
            ConsoleHelper.WriteWithCondition($"Enter the maximum price to see the drugs under this price: ", ConsoleColor.Cyan);
            int price; //+
            bool isValid = int.TryParse(Console.ReadLine(), out price);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Invalid max price format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugPriceDesc;
            }

            if (!(price > 0))
            {
                ConsoleHelper.WriteWithColor("Max price should be bigger than 0| Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugPriceDesc;
            }
            Console.Clear();
            var filteredDrugs = _drugRepository.Filter(price);

            if (filteredDrugs.Count() == 0)
            {
                ConsoleHelper.WriteWithColor($"There is no drug under the price of {price} in the drugstore.",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugPriceDesc;
            }
            Console.Clear();
            ConsoleHelper.WriteWithColor($"---- Drugs under the Price of {price} ----", ConsoleColor.Cyan);

            foreach (var drug in filteredDrugs)
            {
                ConsoleHelper.WriteWithColor(
                    $"| Drug ID: {drug.Id} | Name: {drug.Name} | Description: {drug.Description} | Price: {drug.Price}  |",
                    ConsoleColor.Cyan);
            }

            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("Press any key to go to Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
            _menuServices.MainMenu();
        } // call
    }
}

