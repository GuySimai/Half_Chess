namespace Half_Checkmate.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace Half_Checkmate.Models
    {
        public class PhoneNumberValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                var phoneNumber = value as string;
                // Checking if Null Or Empty case
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    return new ValidationResult("phone number is required.");
                }

                // Checking if the phone number contain only digits
                foreach (var ch in phoneNumber)
                {
                    if (!char.IsDigit(ch))
                    {
                        return new ValidationResult("The phone number must contain only digits.");
                    }
                }

                // Checking if the phone number contain exactly 10 digits
                if (phoneNumber.Length != 10)
                {
                    return new ValidationResult("The phone number must contain exactly 10 digits.");
                }

                return ValidationResult.Success;
            }
        }
    }



}
