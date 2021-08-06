using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is Must")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Must")]
        public string Password { get; set; }
    }

    public class RegisterUser
    {
        [Required(ErrorMessage = "Email is Must")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Must")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be minimum 8 characters and must be string password with uppercase character, number and sepcial character")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class ResponseData
    {
        public string Message { get; set; }
    }
}
