using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proect_adressbook_
{
    public class Contact
    {
        // Свойство для хранения 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        // Переопределение метода ToString для удобного форматированного вывода контакта.
        public override string ToString()
        {
            return $"{FirstName} {LastName} - {PhoneNumber} - {Email} - {Address}";
        }
    }


}
