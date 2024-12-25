using System.ComponentModel.DataAnnotations;

namespace HairSalon.Models
{
	public class Service
	{
		public int ServiceID { get; set; }

		[Required]
		[MaxLength(150)]
		[Display(Name = "Hizmet Adı")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Süre (Dakika)")]
		public int Duration { get; set; }

		[Required]
		[Display(Name = "Ücret")]
		public decimal Price { get; set; }
	}
}
