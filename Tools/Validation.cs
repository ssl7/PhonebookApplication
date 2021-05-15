using FluentValidation;
using PhonebookApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApplication.Tools
{
    public class PhonebookRecordValidator : AbstractValidator<PhonebookRecordModel>
    {
        public PhonebookRecordValidator()
        {
            RuleFor(rec => rec.PhoneNumber).Must(IsValidPhoneNumber).WithMessage("Please specify a valid phone number");
            RuleFor(rec => rec.Email).EmailAddress().WithMessage("Please specify a valid email");
            RuleFor(rec => rec.ZipCode).Must(IsValidZipcode).WithMessage("Please specify a valid zipcode");
        }

        private bool IsValidZipcode(string zipCode)
        {
            if ((zipCode?.Length ?? 0) < 6)
                return false;
            return true;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.All(char.IsDigit))
                return true;
            return false;
        }
    }
}
