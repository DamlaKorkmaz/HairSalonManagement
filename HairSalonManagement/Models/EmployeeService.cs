using System.ComponentModel.DataAnnotations;

namespace HairSalonManagement.Models
{
	public class EmployeeService
	{
		public int ServiceID { get; set; }
		public Service Service { get; set; }

		public int EmployeeID { get; set; }
		public Employee Employee { get; set; }
	}
}
