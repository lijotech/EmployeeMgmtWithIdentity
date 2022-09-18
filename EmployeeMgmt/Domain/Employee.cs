using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMgmt.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        [NotMapped]
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
    }
}
