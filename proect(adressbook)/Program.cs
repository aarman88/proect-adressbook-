using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace proect_adressbook_
{
    
    // Класс Program содержит точку входа в приложение.
    class Program
    {
        // Метод Main запускает менеджер адресной книги для взаимодействия с пользователем.
        static void Main()
        {
            // Создание и запуск менеджера адресной книги.
            AddressBookManager addressBookManager = new AddressBookManager();
            addressBookManager.Run();
        }
    }


}
