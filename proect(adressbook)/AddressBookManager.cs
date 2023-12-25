using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proect_adressbook_
{
    public class AddressBookManager
    {
        private AddressBook addressBook = new AddressBook();

        public AddressBookManager()
        {
            addressBook.LoadData();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить контакт");
                Console.WriteLine("2. Удалить контакт");
                Console.WriteLine("3. Редактировать контакт");
                Console.WriteLine("4. Поиск контакта");
                Console.WriteLine("5. Вывести все контакты");
                Console.WriteLine("6. Сохранить и выйти");

                Console.Write("Выберите действие (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;

                    case "2":
                        RemoveContact();
                        break;

                    case "3":
                        EditContact();
                        break;

                    case "4":
                        SearchContact();
                        break;

                    case "5":
                        DisplayAllContacts();
                        break;

                    case "6":
                        addressBook.SaveData();
                        return;

                    default:
                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 6.");
                        break;
                }
            }
        }

        private void AddContact()
        {
            Console.WriteLine("Введите данные нового контакта:");
            Console.Write("Имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            Console.Write("Номер телефона: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Электронная почта: ");
            string email = Console.ReadLine();
            Console.Write("Адрес: ");
            string address = Console.ReadLine();

            Contact newContact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address
            };

            addressBook.AddContact(newContact);
        }

        private void RemoveContact()
        {
            Console.Write("Введите имя, фамилию или номер телефона контакта для удаления: ");
            string deleteQuery = Console.ReadLine();
            addressBook.RemoveContact(deleteQuery);
        }

        private void EditContact()
        {
            Console.Write("Введите имя, фамилию или номер телефона контакта для редактирования: ");
            string editQuery = Console.ReadLine();
            addressBook.EditContact(editQuery);
        }

        private void SearchContact()
        {
            Console.Write("Введите имя, фамилию или номер телефона для поиска: ");
            string searchQuery = Console.ReadLine();
            addressBook.SearchContact(searchQuery);
        }

        private void DisplayAllContacts()
        {
            addressBook.DisplayAllContacts();
        }
    }

}
