using System.ComponentModel.DataAnnotations;

namespace HairSalonManagement.Models
{
	public class Service
	{
		public int ServiceID { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[Range(0.01, 9999)]
		public decimal Price { get; set; }

		[Required]
		[Range(1, 480)]
		public int DurationMinutes { get; set; }
	}
}