using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Repository.Abstract;
using Hotel.Utils;
using Hotel.Repository;
using Hotel.Repository.Concrete;
using Hotel.Model.Entities.Abstract;


namespace SimpleTestApp
{
    class Program
    {
        static void Main ( string [ ] args )
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            string model1Str;
            string model2Str;

            using ( HotelDbContext dbContext = new HotelDbContext() )
            {

                AccountRepository accounts = new AccountRepository(dbContext, dbContext.Accounts);
                RoomRepository rooms = new RoomRepository(dbContext, dbContext.Rooms);
                HallRepository halls = new HallRepository(dbContext, dbContext.Halls);

                Client cl1 = new Client(Guid.NewGuid(), "Ivan", "Ivanov", "",
                "ivanov@example.com", hashProvider.GetHashCode("1111"), new DateOfBirth(12,03,1990));

                Client cl2 = new Client(Guid.NewGuid(), "Petr", "Petrov", "",
                "petrov@example.com", hashProvider.GetHashCode("1111"), new DateOfBirth(24,05,1989));

                Client cl3 = new Client(Guid.NewGuid(), "Vasya", "Pupkin", "",
                "pupkin@example.com", hashProvider.GetHashCode("1111"), new DateOfBirth(01,10,1970));

                Admin ad1 = new Admin(Guid.NewGuid(), "Artem", "Arbuzov", "",
                "arbuzov@example.com", hashProvider.GetHashCode("1111"), new DateOfBirth(30,09,1987));

                accounts.StartTransaction();

                accounts.Add( cl1 );
                accounts.Add( cl2 );
                accounts.Add( cl3 );
                accounts.Add( ad1 );

                accounts.Commit();

                Room rm1 = new Room(Guid.NewGuid(), 1, 2, (decimal)1200.01, "First room", 1);
                Room rm2 = new Room(Guid.NewGuid(), 2, 1, (decimal)1000.01, "Second room", 1);
                Room rm3 = new Room(Guid.NewGuid(), 3, 2, (decimal)1500.01, "Third room", 2);
                Room rm4 = new Room(Guid.NewGuid(), 4, 2, (decimal)2000.01, "Fourth room", 2);
                Room rm5 = new Room(Guid.NewGuid(), 5, 1, (decimal)2500.01, "Fifth room", 1);

                rooms.StartTransaction();

                rooms.Add( rm1 );
                rooms.Add( rm2 );
                rooms.Add( rm3 );
                rooms.Add( rm4 );
                rooms.Add( rm5 );

                rooms.Commit();

                Hall h1 = new Hall(Guid.NewGuid(), 1, 120, (decimal)7500.01, "Small hall");

                halls.StartTransaction();

                halls.Add( h1 );

                halls.Commit();

                BookingPeriod bp1 = new BookingPeriod(Guid.NewGuid(),
                    new BookingDate (Date.GetToday().AddDays(1)), new BookingDate(Date.GetToday().AddDays(10)));

                BookingPeriod bp2 = new BookingPeriod(Guid.NewGuid(),
                   new BookingDate (Date.GetToday().AddDays(5)), new BookingDate(Date.GetToday().AddDays(12)));

                BookingPeriod bp3 = new BookingPeriod(Guid.NewGuid(), 
                    new BookingDate(28, 10, 2016), new BookingDate(10, 11, 2016));

                BookingPeriod bp4 = new BookingPeriod(Guid.NewGuid(), 
                    new BookingDate(28, 10, 2016), new BookingDate(10, 11, 2016));

                Booking b1 = new Booking(Guid.NewGuid(), bp1, rm1, cl1.Name, cl1.Surname, cl1.Middlename);
                Booking b2 = new Booking(Guid.NewGuid(), bp2, rm2, cl2.Name, cl2.Surname, cl2.Middlename);
                Booking b3 = new Booking(Guid.NewGuid(), bp3, rm1, cl3.Name, cl3.Surname, cl3.Middlename);
                Booking b4 = new Booking(Guid.NewGuid(), bp4, rm5, "Anton", "Pavlovich", "Chehov");

                accounts.StartTransaction();
                
                cl1.AddToCart( b1 );
                cl2.AddToCart( b2 );
                cl3.AddToCart( b3 );
                cl3.AddToCart( b4 );

                cl3.PaymentMade();

                accounts.Commit();

               
                /*accounts.StartTransaction();

                accounts.Delete( cl3 );

                accounts.Commit();

                /*rooms.StartTransaction();
                rooms.Commit();*/

                model1Str = "===Accounts=== \r\n" + ad1 + cl1 + cl2 + cl3 + "\r\n";
                model1Str += "===Rooms=== \r\n" + rm1 + rm2 + rm3 + rm4 + rm5 + "\r\n";
                model1Str += "===Halls=== \r\n" + h1 + "\r\n";

                Console.WriteLine( model1Str );
            }

            using ( HotelDbContext dbContext2 = new HotelDbContext() )
            {
                RoomRepository rooms2 = new RoomRepository(dbContext2, dbContext2.Rooms);
                HallRepository halls2 = new HallRepository(dbContext2, dbContext2.Halls);
                AccountRepository accounts2 = new AccountRepository(dbContext2, dbContext2.Accounts);
                BookingHolderRepository bhs = new BookingHolderRepository(dbContext2, dbContext2.BookingHolders);

                List<BookingHolder> dhsList = bhs.LoadAll().ToList();

                model2Str = "===Accounts=== \r\n";

                foreach ( Account ac in accounts2.LoadAll().OrderBy( a => a.Surname ) )
                {
                    model2Str += ac;
                }

                model2Str += "\r\n";

                model2Str += "===Rooms=== \r\n";

                foreach ( Room room in rooms2.LoadAll().OrderBy( r => r.Number ) )
                {
                    model2Str += room;
                }

                model2Str += "\r\n";

                model2Str += "===Halls=== \r\n";

                foreach ( Hall hall in halls2.LoadAll().OrderBy( h => h.Number ) )
                {
                    model2Str += hall;
                }

                model2Str += "\r\n";  
            }

            Console.WriteLine( model2Str );

            if ( model1Str == model2Str )
                Console.WriteLine( "Models are equals" );
            else
                Console.WriteLine( "Models are not equals" );

        }
    }
}
