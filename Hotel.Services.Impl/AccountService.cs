using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Dto;
using Hotel.Model.Entities.Concrete;
using Hotel.Exceptions;
using Hotel.Services;
using Hotel.Repository;

namespace Hotel.Services.Impl
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepo;
       // private IBookingHolderRepository _bhRepo;
        
        public AccountService ( IAccountRepository aRepo)
        {
            _accountRepo = aRepo;
        }

        public void ChangeEmail ( Guid id, string email )
        {
            if ( _accountRepo.Find( email ) != null )
                throw new DuplicateNamedEntityException( typeof( Account ), email );

            

            Account ac = ServiceUtils.GetEntity( _accountRepo, id );

            ac.Email = email;

           
        }

        public void ChangeName (Guid id, string name, string surname, string middlename )
        {
            

            Account ac = ServiceUtils.GetEntity(_accountRepo, id);

            ac.Name = name;
            ac.Surname = surname;
            ac.Middlename = middlename;

            
        }

        public void ChangePassword ( Guid id, string oldPasswordHash, string newPasswordHash )
        {
           

            Account ac = ServiceUtils.GetEntity(_accountRepo, id);

            if ( ac.PasswordHash != oldPasswordHash )
                throw new ArgumentException("wrong password");

            ac.PasswordHash = newPasswordHash;

           
        }

        public Guid CreateAdmin ( string name, string surname, string middlename,
                                    Utils.DateOfBirth dateOfBirth, 
                                    string email, string passwordHash )
        {
            if ( _accountRepo.Find( email ) != null )
                throw new ArgumentException( "Account alredy exists" );


            Admin ad = new Admin( Guid.NewGuid(), name, surname, middlename, email, passwordHash,
                dateOfBirth );

            _accountRepo.Add( ad );


            return ad.Id;
        }

        public Guid CreateClient ( string name, string surname, string middlename,
                                       Utils.DateOfBirth dateOfBirth, string email, string passwordHash )
        {
            if ( _accountRepo.Find( email ) != null )
                throw new ArgumentException( "Account alredy exists" );


            Client cl = new Client( Guid.NewGuid(), name, surname, middlename, email, passwordHash,
                dateOfBirth );

            _accountRepo.Add( cl );


            return cl.Id;
        }

        public BookingHolderDto GetCartContent ( Guid userId )
        {
            Account ac = ServiceUtils.GetEntity(_accountRepo, userId);

            return ac.Cart.toDto();
        }

        public BookingHolderDto GetHistoryContent ( Guid userId )
        {
            Account ac = ServiceUtils.GetEntity(_accountRepo, userId);

            return ac.History.toDto();
        }

        public AccountDto Indentify ( string password, string email )
        {
            Account ac = _accountRepo.Find(email, password);

            if ( ac == null )
                return null;

            return ac.toDto();
        }

        public void OnPaymentMade ( Guid userId )
        {
            Account acc = ServiceUtils.GetEntity(_accountRepo, userId);

            

            acc.PaymentMade();

            
        }

        public AccountDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( _accountRepo, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return _accountRepo.SelectAllDomainIds().ToList();
        }
    }
}
