using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Utils;
using Hotel.Repository.Configurations;

namespace Hotel.Repository.Impl
{
    public class HotelDbContext : DbContext
    {
        static HotelDbContext ()
        {
            //Database.SetInitializer( new DropCreateDatabaseAlways<HotelDbContext>() );
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BookingHolder> BookingHolders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPeriod> BookingPeriods { get; set; }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        {
            modelBuilder.Configurations.Add( new RoomConfiguration() );
            modelBuilder.Configurations.Add( new HallConfiguration() );

            modelBuilder.Configurations.Add( new DateConfiguration() );

            modelBuilder.Configurations.Add( new AccountConfiguration() );

            modelBuilder.Configurations.Add( new BookingConfiguration() );

            modelBuilder.Configurations.Add( new BookingHolderConfiguration() );

            modelBuilder.Configurations.Add( new BookingPeriodConfiguration() );

            base.OnModelCreating( modelBuilder );
        }
    }
}
