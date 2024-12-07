using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMVC.Models.Domain
{
    public class Employee
    {
        [Required]
         public Guid Id { get; set; }
        [Required]
         public string Name { get; set; }
        [Required]
         public string Email { get; set; }
        [Required]
         public long Salary { get; set; }
        [Required]
         public DateTime DateOfBirth { get; set; }
        [Required]
         public string Department { get; set; }

    }
}
