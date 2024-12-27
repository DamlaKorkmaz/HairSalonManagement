namespace HairSalonManagement.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Randevu
	{
		[Key]
		public int RandevuId { get; set; }

		[Required]
		public int KullaniciId { get; set; }
		public Kullanici Kullanici { get; set; }

		[Required]
		public int UzmanId { get; set; }
		public Uzman Uzman { get; set; }

		[Required]
		public DateTime Tarih { get; set; } // Randevu tarihi ve saati
	}
}
