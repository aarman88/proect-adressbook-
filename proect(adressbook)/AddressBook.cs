using NLog;
using proect_adressbook_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


// Класс AddressBook представляет адресную книгу и основные операции с контактами.
public class AddressBook
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    // Список контактов в адресной книге.
    private List<Contact> contacts = new List<Contact>();

    // Путь к файлу, в котором сохраняются данные адресной книги.
    private const string DatabaseFileName = "address_book_db.txt";

    // Метод LoadData загружает данные (контакты) из файла в адресную книгу.
    public void LoadData()
    {
        try
        {
            if (File.Exists(DatabaseFileName))
            {
                string[] lines = File.ReadAllLines(DatabaseFileName);
                contacts = lines.Select(line => Newtonsoft.Json.JsonConvert.DeserializeObject<Contact>(line)).ToList();
            }
        }
        catch (Exception ex)
        {
            // Обработка исключения: вывод сообщения об ошибке и запись в лог.
            Console.WriteLine("Произошла ошибка при загрузке данных.");
            Log.Error($"Ошибка при загрузке данных: {ex.Message}");
        }
    }

    /// Метод SaveData сохраняет данные (контакты) адресной книги в файл.
    public void SaveData()
    {
        try
        {
            string[] lines = contacts.Select(contact => Newtonsoft.Json.JsonConvert.SerializeObject(contact)).ToArray();
            File.WriteAllLines(DatabaseFileName, lines);
        }
        catch (Exception ex)
        {
            // Обработка исключения: вывод сообщения об ошибке и запись в лог.
            Console.WriteLine("Произошла ошибка при сохранении данных.");
            Log.Error($"Ошибка при сохранении данных: {ex.Message}");
        }
    }

    // Метод AddContact добавляет новый контакт в адресную книгу.
    public void AddContact(Contact contact)
    {
        if (!contacts.Any(c => c.PhoneNumber == contact.PhoneNumber))
        {
            contacts.Add(contact);
            Console.WriteLine("Контакт успешно добавлен.");
            SaveData();
            Log.Info($"Контакт {contact.FirstName} {contact.LastName} успешно добавлен.");
        }
        else
        {
            Console.WriteLine("Контакт с таким номером телефона уже существует.");
            Log.Warn($"Попытка добавления контакта с существующим номером телефона: {contact.PhoneNumber}");
        }
    }

    // Метод RemoveContact удаляет контакт по имени, фамилии или номеру телефона.
    public void RemoveContact(string query)
    {
        var contactToRemove = contacts.FirstOrDefault(c => c.FirstName == query || c.LastName == query || c.PhoneNumber == query);
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Контакт успешно удален.");
            SaveData();
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    // Метод EditContact редактирует существующий контакт в адресной книге.
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
            SaveData();

            // Аналогично для других свойств контакта (фамилия, номер телефона, электронная почта, адрес).

            Console.WriteLine("Контакт успешно отредактирован.");
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    // Метод SearchContact выполняет поиск контакта по имени, фамилии или номеру телефона.
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

    // Метод DisplayAllContacts выводит все контакты в адресной книге в алфавитном порядке.
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
    // Метод для валидации номера телефона.
    private bool IsPhoneNumberValid(string phoneNumber)
    {
        // Используем Regex для более гибкой валидации.
        // Пример: номер должен содержать только цифры, может начинаться с "+" и иметь от 10 до 15 символов.
        string pattern = @"^\+?[0-9]{10,15}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}
