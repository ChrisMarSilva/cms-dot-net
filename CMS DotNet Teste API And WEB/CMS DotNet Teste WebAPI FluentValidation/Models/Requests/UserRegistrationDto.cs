namespace CMS_DotNet_Teste_WebAPI_FluentValidation.Models.Requests;

public record UserRegistrationDto(
    string? Email = null,
    string? Password = null,
    string? ConfirmPassword = null,
    PersonalInfo? PersonalInfo = null,
    AddressInfo? Address = null,
    List<string>? Interests = null,
    DateTime? DateOfBirth = null,
    string? PhoneNumber = null,
    bool? AcceptTerms = null)
{
    public IEnumerable<string> Validate()
    {
        var errors = new List<string>();

        // Email Validation
        if (string.IsNullOrEmpty(Email))
            errors.Add("Email is required");
        else if (!Email.Contains('@') || !Email.Contains('.'))
            errors.Add("Invalid email format");

        // Password Validation
        if (string.IsNullOrEmpty(Password))
            errors.Add("Password is required");
        else if (Password.Length < 8)
            errors.Add("Password must be at least 8 characters long");
        else if (Password != ConfirmPassword)
            errors.Add("Passwords do not match");

        // Personal Info Validation
        if (PersonalInfo is null)
            errors.Add("Personal Info is required");
        else
        {
            if (string.IsNullOrEmpty(PersonalInfo.FirstName))
                errors.Add("First Name is required");
            if (string.IsNullOrEmpty(PersonalInfo.LastName))
                errors.Add("Last Name is required");
        }

        // Address Info Validation
        if (Address is null)
            errors.Add("Address is required");
        else
        {
            if (string.IsNullOrEmpty(Address.Street))
                errors.Add("Street is required");
            if (string.IsNullOrEmpty(Address.City))
                errors.Add("City is required");
            if (string.IsNullOrEmpty(Address.State))
                errors.Add("State is required");
            if (string.IsNullOrEmpty(Address.PostalCode))
                errors.Add("Postal Code is required");
            if (string.IsNullOrEmpty(Address.Country))
                errors.Add("Country is required");
        }

        // Interests Validation
        if (Interests is null)
            errors.Add("Interests is required");
        else if (Interests.Count == 0)
            errors.Add("Interests is required");
        else
        {
            foreach (var interest in Interests)
            {
                if (string.IsNullOrEmpty(interest))
                    errors.Add("Interests cannot be empty.");
            }
        }

        //age validation
        if (DateOfBirth is null)
            errors.Add("Date of Birth is required");
        else
        {
            var age = DateTime.Now.Year - DateOfBirth.Value.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.Value.DayOfYear)
                age--;

            var minAge = 18;
            if (DateOfBirth.Value > DateTime.Today)
                errors.Add("Date of Birth cannot be in the future");
            else
            if (age < minAge)
                errors.Add("You must be at least 18 years old to register");
        }

        // terms validation
        if (!AcceptTerms!.Value)
            errors.Add("You must accept the terms and conditions");
        
        return errors;
    }
}

public record PersonalInfo(
    string? FirstName = null,
    string? LastName = null,
    string? MiddleName = null,
    string? PreferredName = null);

public record AddressInfo(
    string? Street = null,
    string? City = null,
    string? State = null,
    string? PostalCode = null,
    string? Country = null);