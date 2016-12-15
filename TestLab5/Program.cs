using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Utils;
using Hotel.Repository;
using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Repository.Impl;
using Microsoft.Practices.Unity;
using Hotel.Dependencies;
using Hotel.Services;
using Hotel.Model;

namespace TestLab5
{
    class Program
    {
        static string MakeReport<TDto> ( IDomainEntityService<TDto> service, string title )
            where TDto : DomainDto
        {
            string res = "=== " + title + " ===\r\n";

            foreach ( Guid id in service.ViewAll() )
            {
                res += service.View( id ).ToString();
            }

            return res;
        }

        static void Main ( string [ ] args )
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            string model1Str = "";
            //string model2Str = "";

            using ( HotelDbContext dbContext = new HotelDbContext() )
            using ( var unityContainer = new UnityContainer() )
            {
                ContainerBoostraper.RegisterTypes( unityContainer, dbContext );

                IAccountRepository accRepo = unityContainer.Resolve<IAccountRepository>();
                IBookingHolderRepository bhRepo = unityContainer.Resolve<IBookingHolderRepository>();
                IBookingRepository  bRepo = unityContainer.Resolve<IBookingRepository>();
                IRoomRepository rRepo = unityContainer.Resolve<IRoomRepository>();
                IHallRepository hRepo = unityContainer.Resolve<IHallRepository>();

                IAccountService accServ = unityContainer.Resolve<IAccountService>();
                ICartService cartServ = unityContainer.Resolve<ICartService>();
                IHistoryService histServ = unityContainer.Resolve<IHistoryService>();
                IRoomService rServ = unityContainer.Resolve<IRoomService>();
                IHallService hallServ = unityContainer.Resolve<IHallService>();

                Guid cl1Id = accServ.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth(01, 01, 1990),
                                        "pupkin@example.com", hashProvider.GetHashCode( "1111" ) );

                Guid cl2Id = accServ.CreateClient( "Innokentiy", "Omarov", "", new DateOfBirth(20, 10, 1989),
                                        "omarov@example.com", hashProvider.GetHashCode( "1111" ) );

                Guid cl3Id = accServ.CreateClient( "Aleksandr", "Bezrukov", "", new DateOfBirth(20, 10, 1989),
                                        "bezrukov@example.com", hashProvider.GetHashCode( "1111" ) );

                Guid ad1Id = accServ.CreateAdmin( "Dmitriy", "Nagiev", "", new DateOfBirth(05, 10, 1989),
                                        "nagiev@example.com", hashProvider.GetHashCode( "1111" ) );

                Guid r1 = rServ.CreateRoom( 1, "Room number 1", 2, 1, 1000.01m );
                Guid r2 = rServ.CreateRoom( 2, "Room number 2", 2, 2, 1500.01m );
                Guid r3 = rServ.CreateRoom( 3, "Room number 2", 1, 1, 800.01m );

                Guid h1 = hallServ.CreateHall( 1, "Hall munber 1", 100, 500.01m );

                rServ.SetCriteria( r1, SearchCriteria.Freedge );
                rServ.SetCriteria( r1, SearchCriteria.TV );

                AccountDto acc1 = accServ.Indentify( hashProvider.GetHashCode( "1111" ), "pupkin@example.com" );
                AccountDto acc2 = accServ.Indentify( hashProvider.GetHashCode( "1111" ), "omarov@example.com" );
                AccountDto acc3 = accServ.Indentify( hashProvider.GetHashCode( "1111" ), "bezrukov@example.com" );



                PeriodDto p1 = new PeriodDto(Guid.NewGuid(),
                    Date.GetToday().AddDays(1).Day, Date.GetToday().AddDays(1).Month, Date.GetToday().AddDays(1).Year,
                    Date.GetToday().AddDays(10).Day, Date.GetToday().AddDays(10).Month, Date.GetToday().AddDays(10).Year);

                PeriodDto p2 = new PeriodDto(Guid.NewGuid(),
                    Date.GetToday().AddDays(1).Day, Date.GetToday().AddDays(1).Month, Date.GetToday().AddDays(1).Year,
                    Date.GetToday().AddDays(10).Day, Date.GetToday().AddDays(10).Month, Date.GetToday().AddDays(10).Year);

                PeriodDto p3 = new PeriodDto(Guid.NewGuid(),
                    Date.GetToday().AddDays(5).Day, Date.GetToday().AddDays(5).Month, Date.GetToday().AddDays(5).Year,
                    Date.GetToday().AddDays(15).Day, Date.GetToday().AddDays(15).Month, Date.GetToday().AddDays(15).Year);

                PeriodDto p4 = new PeriodDto(Guid.NewGuid(),
                    Date.GetToday().AddDays(1).Day, Date.GetToday().AddDays(1).Month, Date.GetToday().AddDays(1).Year,
                    Date.GetToday().AddDays(10).Day, Date.GetToday().AddDays(10).Month, Date.GetToday().AddDays(10).Year);

                BookingDto b1 = new BookingDto(Guid.NewGuid(), p1, r1, acc1.Name, acc1.Surname, acc1.Middlename);
                BookingDto b2 = new BookingDto(Guid.NewGuid(), p2, r2, acc1.Name, acc1.Surname, acc1.Middlename);
                BookingDto b3 = new BookingDto(Guid.NewGuid(), p3, r2, acc2.Name, acc2.Surname, acc2.Middlename);
                BookingDto b4 = new BookingDto(Guid.NewGuid(), p4, r3, acc3.Name, acc3.Surname, acc3.Middlename);



                cartServ.AddItem( acc1.Cart.Id, b1 );
                cartServ.AddItem( acc1.Cart.Id, b2 );
                cartServ.AddItem( acc2.Cart.Id, b3 );
                cartServ.AddItem( acc3.Cart.Id, b4 );

                accServ.OnPaymentMade( acc1.Id );
            }

            using ( HotelDbContext dbContext2 = new HotelDbContext() )
            using ( var unityContainer = new UnityContainer() )
            {
                ContainerBoostraper.RegisterTypes( unityContainer, dbContext2 );

                IAccountRepository accRepo = unityContainer.Resolve<IAccountRepository>();
                IBookingHolderRepository bhRepo = unityContainer.Resolve<IBookingHolderRepository>();
                IBookingRepository  bRepo = unityContainer.Resolve<IBookingRepository>();
                IRoomRepository rRepo = unityContainer.Resolve<IRoomRepository>();
                IHallRepository hRepo = unityContainer.Resolve<IHallRepository>();

                IAccountService accServ = unityContainer.Resolve<IAccountService>();
                ICartService cartServ = unityContainer.Resolve<ICartService>();
                IHistoryService histServ = unityContainer.Resolve<IHistoryService>();
                IRoomService rServ = unityContainer.Resolve<IRoomService>();
                IHallService hallServ = unityContainer.Resolve<IHallService>();


                model1Str += MakeReport( accServ, "Accounts" );
                model1Str += MakeReport( rServ, "Rooms" );
                model1Str += MakeReport( hallServ, "Halls" );
                model1Str += MakeReport( cartServ, "Carts" );
                model1Str += MakeReport( histServ, "Histories" );

                Console.WriteLine( model1Str );
            }
        }
    }
}
