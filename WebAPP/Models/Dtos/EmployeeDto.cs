using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPP.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company is required")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime? BirthDate { get; set; }
    }
}