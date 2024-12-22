using System.ComponentModel.DataAnnotations.Schema;

namespace HairSalonManagement.Models
{
	public class Employee
	{
		public int EmployeeID { get; set; } // Primary Key
		public int SalonID { get; set; } // Foreign Key
		public string Name { get; set; }
		public string Specialization { get; set; }
		public string AvailabilityHours { get; set; }

		// Navigation Property
		[ForeignKey("SalonID")]
		public Salon Salon { get; set; } // Salon ile ilişki

		public ICollection<EmployeeService> EmployeeServices { get; set; }

	}
}
