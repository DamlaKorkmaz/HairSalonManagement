namespace HairSalonManagement.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Uzman
	{
		[Key]
		public int UzmanId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Isim { get; set; }

		[Required]
		[MaxLength(50)]
		public string Soyisim { get; set; }

		[Required]
		public string UzmanlikAlani { get; set; }

		[Required]
		[MaxLength(20)]
		public string CalismaSaatAraligi { get; set; } // Örnek: "09:00-18:00"

		// Uzmanın aldığı randevular
		//public ICollection<Randevu> Randevular { get; set; }
	}
}
