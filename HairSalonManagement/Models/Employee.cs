using System.ComponentModel.DataAnnotations;

namespace NewHairSalon.Models
{
	public class Employee
	{

 public int EmployeeID { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Çalışan Adı")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Çalışan Soyadı")]
    public string LastName { get; set; }

    [Display(Name = "Uzmanlık Alanları")]
		public ICollection<Service>? Hizmetler { get; set; }
	}
}

