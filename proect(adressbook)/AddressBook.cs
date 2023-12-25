using proect_adressbook_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AddressBook
{
    private List<Contact> contacts = new List<Contact>();
    private const string FilePath = "address_book.json";

    public void LoadData()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            contacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }

    public void SaveData()
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(contacts);
        File.WriteAllText(FilePath, json);
    }

    public void AddContact(Contact contact)
    {
        if (!contacts.Any(c => c.PhoneNumber == contact.PhoneNumber))
        {
            contacts.Add(contact);
            Console.WriteLine("Контакт успешно добавлен.");
        }
        else
        {
            Console.WriteLine("Контакт с таким номером телефона уже существует.");
        }
    }

    public void RemoveContact(string query)
    {
        var contactToRemove = contacts.FirstOrDefault(c => c.FirstName == query || c.LastName == query || c.PhoneNumber == query);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Контакт успешно удален.");
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    public void EditContact(string query)
    {
        var contactToEdit = contacts.FirstOrDefault(c => c.FirstName == query || c.LastName == query || c.PhoneNumber == query);
        if (contactToEdit != null)
        {
            Console.WriteLine("Введите новые данные для редактирования (если не нужно изменять, нажмите Enter):");

            Console.Write("Имя: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
                contactToEdit.FirstName = newName;

            Console.Write("Фамилия: ");
            string newLastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLastName))
                contactToEdit.LastName = newLastName;

            Console.Write("Номер телефона: ");
            string newPhoneNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPhoneNumber))
                contactToEdit.PhoneNumber = newPhoneNumber;

            Console.Write("Электронная почта: ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail))
                contactToEdit.Email = newEmail;

            Console.Write("Адрес: ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAddress))
                contactToEdit.Address = newAddress;

            Console.WriteLine("Контакт успешно отредактирован.");
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    public void SearchContact(string query)
    {
        var searchResults = contacts.Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query) || c.PhoneNumber.Contains(query)).ToList();
        if (searchResults.Any())
        {
            Console.WriteLine("Результаты поиска:");
            foreach (var result in searchResults)
            {
                Console.WriteLine(result);
            }
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    public void DisplayAllContacts()
    {
        if (contacts.Any())
        {
            Console.WriteLine("Все контакты в адресной книге:");
            foreach (var contact in contacts.OrderBy(c => c.FirstName))
            {
                Console.WriteLine(contact);
            }
        }
        else
        {
            Console.WriteLine("Адресная книга пуста.");
        }
    }
}
