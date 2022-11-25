using System.ComponentModel.DataAnnotations;

namespace DutchTreat.Models
{
    public class ContactModel
    {
        [Required]
        [MinLength(2, ErrorMessage="First name needs 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Last name needs 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{3}-\d{3}-\d{4})$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Required]
        public string Topic{ get; set; }

        [Required]
        [MaxLength(250)]
        public string Message { get; set; }
        
    }
}
