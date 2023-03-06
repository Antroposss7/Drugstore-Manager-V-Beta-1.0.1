using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Contexts.Repositories.Abstract;
using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class DruggistService
    {
        private readonly DrugstoreService _drugStoreService;
        private readonly DrugstoreRepository _drugStoreRepository;
        private readonly DruggistRepository _druggistRepository;
        private readonly Admin admin;
        private readonly MenuServices _menuServices;

        public DruggistService()
        {
            _drugStoreRepository = new DrugstoreRepository();
            _druggistRepository = new DruggistRepository();
            _drugStoreService = new DrugstoreService(admin);
            _menuServices = new MenuServices();
        }


        public void GetAll() //-
        {
            var druggists = _druggistRepository.GetAll();
            ConsoleHelper.WriteWithColor(" ---- All Druggists ---- ");
            if (druggists.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any Druggists! | Press any key to return Druggists Menu... ",
                    ConsoleColor.Red);
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
                _menuServices.DruggistMenu();
            }

            foreach (var druggist in druggists)
            {
                ConsoleHelper.WriteWithColor(
                    $" Druggist ID: {druggist.Id}, Druggist Name: {druggist.Name}, Druggist Surname: {druggist.Surname}.",
                    ConsoleColor.DarkCyan);
            }

            ConsoleHelper.WriteWithColor("-------------------------------", ConsoleColor.Cyan);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to continue", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }
        public void GetAllDruggistsByDrugstore() 
        {
            var drugstores = _drugStoreRepository.GetAll();

            if (drugstores.Count == 0)
            {
                ConsoleHelper.WriteWithColor(
                    "There are no drugstores in the database. Press any key to go back to the Main Menu...",
                    ConsoleColor.Red);
                Console.ReadKey();
                _menuServices.MainMenu();
            }

            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor(
                    $"Drugstore Id: {drugstore.Id} | Name: {drugstore.Name} | Email: {drugstore.Email} | Address: {drugstore.Address}",
                    ConsoleColor.Cyan);
            }

            DrugstoreIdDesc:
            Console.WriteLine("\n\n");
            ConsoleHelper.WriteWithCondition("Enter Drugstore Id: ", ConsoleColor.Cyan);
            int id;
            bool IsValid = int.TryParse(Console.ReadLine(), out id);

            if (!IsValid)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Invalid Drugstore ID format. Press any key to try again...");
            }
            var drugstoreGet = _drugStoreRepository.Get(id);

            if (drugstoreGet == null)
            {
                ConsoleHelper.WriteWithColor("No drugstore with this ID. Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DrugstoreIdDesc;
            }

            Console.Clear();
            ConsoleHelper.WriteWithColor("---- Druggists by Drugstore ----", ConsoleColor.Cyan);
            Console.WriteLine();
            foreach (var druggist in drugstoreGet.Druggists)
            {
                ConsoleHelper.WriteWithColor($"Selected Drugstore's Druggist Id: {druggist.Id} | Name {druggist.Name} | Surname {druggist.Surname} | Age {druggist.Age} | Experience {druggist.Experience}", ConsoleColor.Cyan);
            }

            Console.WriteLine();
            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("Press any key to go to Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
            _menuServices.MainMenu();
        }

        public void Create() //+//call
        {
            if (_drugStoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create Drug Store first! | Press any key to return Drug Store Menu...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                _menuServices.DrugStoreMenu();
            }

            DruggistNameDesc:
            ConsoleHelper.WriteWithCondition("Enter new Druggist name:", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            if (!name.CheckString()) // + 
            {

                ConsoleHelper.WriteWithColor(
                    "New druggist name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DruggistNameDesc;
            }

            Console.Clear();
            ConsoleHelper.WriteWithCondition("Enter Druggist surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            if (!surname.CheckString()) //+
            {

                ConsoleHelper.WriteWithColor(
                    "New druggist surname is not in correct format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DruggistNameDesc;
            }

            Console.Clear();
            EnterAgeDesc:
            ConsoleHelper.WriteWithCondition("Enter Druggist age:", ConsoleColor.Cyan);
            int age;
            bool isValid = int.TryParse(Console.ReadLine(), out age);
            if (!isValid) 
            {
                ConsoleHelper.WriteWithColor("Druggist age is not correct format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterAgeDesc;
            }

            if (age > 101)
            {
                ConsoleHelper.WriteWithColor("Please enter valid age!! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterAgeDesc;
            }
            if (!(age > 0))
            {
                ConsoleHelper.WriteWithColor("Druggist age can not be null or empty! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterAgeDesc;
            }

            Console.Clear();
            EnterExperienceDesc:
            ConsoleHelper.WriteWithCondition("Enter Druggist experience: ", ConsoleColor.Cyan);
            int experience;
            isValid = int.TryParse(Console.ReadLine(), out experience);
            if (!isValid)

            {
                ConsoleHelper.WriteWithColor("Druggist experience is not correct format", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterExperienceDesc;
            }

            if (experience > age - 18)
            {
                ConsoleHelper.WriteWithColor(
                    "Druggist experience can not be bigger than age! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterExperienceDesc;
            }

            EnterIdDesc:
            Console.Clear();
            _drugStoreService.GetAll();
            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter Drug Store Id for adding:"); /////////////////////////
            int drugStoreid;
            int.TryParse(Console.ReadLine(), out drugStoreid);
            if (drugStoreid.CheckInt())
            {
                ConsoleHelper.WriteWithColor("Drug Store Id is not correct format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterIdDesc;
            }

            var drugStore = _drugStoreRepository.Get(drugStoreid);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("Drug Store in this id does not exist! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterIdDesc;
            }

            var druggist = new Druggist
            {
                Name = name,
                Surname = surname,
                Age = age,
                Experience = experience,
                Drugstore = drugStore,
            };
            Console.Clear();
            drugStore.Druggists.Add(druggist);
            _druggistRepository.Add(druggist);
            ConsoleHelper.WriteWithColor(
                $"{druggist.Name} {druggist.Surname} | Who works in {drugStore.Name} is successfully created!",
                ConsoleColor.Green);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Press any key to go to the Main Menu", ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
            _menuServices.MainMenu();

        }

        public void Update() //+
        {

            UDruggistDesc:
            ConsoleHelper.WriteWithCondition("Enter Druggist Id: ", ConsoleColor.Cyan);
            int id; //+
            int.TryParse(Console.ReadLine(), out id);
            if (id.CheckInt())
            {
                ConsoleHelper.WriteWithColor("Druggist Id is wrong format! | Press any key to try again... ",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UDruggistDesc;
            }

            var druggist = _druggistRepository.Get(id);
            if (druggist is null)
            {
                ConsoleHelper.WriteWithColor("There is no any Druggist in this Id! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UDruggistDesc;
            }

            Console.Clear();
            UDruggistName:
            ConsoleHelper.WriteWithCondition("Enter new Druggist name:", ConsoleColor.Cyan);

            string name = Console.ReadLine();
            if (!name.CheckString()) // + 
            {

                ConsoleHelper.WriteWithColor(
                    "New druggist name is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UDruggistName;
            }

            UDruggistSurname:
            Console.Clear();
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Enter new Druggist surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            if (!name.CheckString()) // + 
            {
                ConsoleHelper.WriteWithColor(
                    "New druggist surname is not in correct format! Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto UDruggistSurname;

            } //+

            DruggistAgeDesc:
            Console.Clear();
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Enter new Druggist age:", ConsoleColor.Cyan);
            int age;
           bool isValid = int.TryParse(Console.ReadLine(), out age);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("New druggist age is not correct format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DruggistAgeDesc;
            }

            DruggistExperienceDesc:
            Console.Clear();
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Enter new Druggist experience:", ConsoleColor.Cyan);
            int experience;
            int.TryParse(Console.ReadLine(), out experience);
            if (!experience.CheckInt())
            {
                ConsoleHelper.WriteWithColor(
                    "New druggist experience is not correct format! | Press any key to try again...", ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DruggistExperienceDesc;
            }

            if (experience > age - 18)
            {
                ConsoleHelper.WriteWithColor(
                    "Druggist experience can not be bigger than his age! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DruggistExperienceDesc;
            }

            EnterIdDesc:
            Console.Clear();
            _drugStoreService.GetAll();
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("Enter Drug Store Id for adding Druggist to:");
            int drugStoreId;
            int.TryParse(Console.ReadLine(), out drugStoreId);
            if (drugStoreId.CheckInt())
            {
                ConsoleHelper.WriteWithColor("Drug Store Id is not correct format! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterIdDesc;
            }

            var drugStore = _drugStoreRepository.Get(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("Drug Store in this Id does not exist! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto EnterIdDesc;
            }
            Console.Clear();

            druggist.Name = name;
            druggist.Surname = surname;
            druggist.Age = age;
            druggist.Experience = experience;
            druggist.Drugstore = drugStore;

            _druggistRepository.Update(druggist);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor(
                $"Druggist {druggist.Name} {druggist.Surname} is successfully updated! | Press any key to continue...",
                ConsoleColor.Green);
            Console.ReadKey();
            Console.Clear();
        }

        public void Delete() //+
        {

            

            DelDruggistIdDesc:
            Console.Clear();
            GetAll(); 
            Console.WriteLine();
            ConsoleHelper.WriteWithCondition("Enter Druggist id for deleting:", ConsoleColor.DarkCyan);
            int id; //checkint
            bool isValid = int.TryParse(Console.ReadLine(), out id);
            if (!isValid)
            {
                ConsoleHelper.WriteWithColor("Druggist Id is wrong format! | Press any key to try again... ",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DelDruggistIdDesc;
            }

            var dbDruggist = _druggistRepository.Get(id);
            if (dbDruggist is null)
            {
                ConsoleHelper.WriteWithColor("There is no any druggist in this Id! | Press any key to try again...",
                    ConsoleColor.Red);
                Console.ReadKey();
                Console.Clear();
                goto DelDruggistIdDesc;
            }

            Console.Clear();

            _druggistRepository.Delete(dbDruggist);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor(
                $"Druggist {dbDruggist.Name} {dbDruggist.Surname} is successfully deleted! | Press any key to continue...",
                ConsoleColor.Green);
            Console.ReadKey();
            Console.Clear();
            _menuServices.DruggistMenu();
        }

    }
}


