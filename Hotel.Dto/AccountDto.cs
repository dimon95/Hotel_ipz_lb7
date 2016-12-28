using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public enum Role { Client, Admin }

    public class AccountDto : DomainDto
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Middlename { get; private set; }

        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        //Date of Birth
        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }  

        public BookingHolderDto Cart { get; private set; }
        public BookingHolderDto BookingHystory { get; private set; }

        public Role Role { get; private set; }

        public AccountDto ( Guid id, string name, string surname, string middlename, string email, string passwordHash, 
                int day, int month, int year,
                BookingHolderDto cart, BookingHolderDto hystory, Role r) 
            : base( id )
        {
            Name = name;
            Surname = surname;
            Middlename = middlename;

            Email = email;
            PasswordHash = passwordHash;

            Day = day;
            Month = month;
            Year = year;

            Cart = cart;
            BookingHystory = hystory;

            Role = r;
        }

        public override string ToString ()
        {
            return "Name: " + Name + "\r\n" + "Surname: " + Surname + "\r\n" + "Middlename: " + Middlename + "\r\n" + 
                   "Email: " + Email + "\r\n" + "Date of Birth: " + Day + "." + Month + "." + Year + "\r\n\r\n";
        }
    }
}
