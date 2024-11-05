using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("Company")]
        [StringLength(50)]
        [Required]
        public string Company { get; set; } = string.Empty;

        [Column("Position")]
        [StringLength(50)]
        [Required]
        public string Position { get; set; } = string.Empty;

        [Column("BirthDate")]
        [Required]
        public DateTime BirthDate { get; set; }

        [Column("Salary")]
        [Required]
        public double Salary { get; set; }
    }
}