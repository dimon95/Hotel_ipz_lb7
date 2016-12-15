using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Utils.Validators;
using Hotel.Services.Validators;

namespace Hotel.Services
{
    public interface IAccountService : IDomainEntityService<AccountDto>
    {
        Guid CreateClient (
            [NonEmptyStringValidator] string name,
            [NonEmptyStringValidator] string surname,
            string middlename,
            [DateBirthValidator] Utils.DateOfBirth dateOfBirth,
            [EmailValidator] string email,
            [NonEmptyStringValidator] string passwordHash );

        Guid CreateAdmin (
            [NonEmptyStringValidator] string name,
            [NonEmptyStringValidator] string surname, 
            string middlename,
            [DateBirthValidator] Utils.DateOfBirth dateOfBirth,
            [EmailValidator] string email,
            [NonEmptyStringValidator] string passwordHash );

        void ChangeEmail ( 
            Guid id,
            [EmailValidator] string email );

        void ChangePassword ( 
            Guid id,
            [NonEmptyStringValidator] string oldPasswordHash,
            [NonEmptyStringValidator] string newPasswordHash );

        void ChangeName ( 
            Guid id,
            [NonEmptyStringValidator] string name,
            [NonEmptyStringValidator] string surname, 
            string middlename );

        AccountDto Indentify (
            [NonEmptyStringValidator] string password,
            [EmailValidator] string email );

        BookingHolderDto GetCartContent ( Guid userId );

        BookingHolderDto GetHistoryContent ( Guid userId );

        void OnPaymentMade ( Guid userId );
    }
}
