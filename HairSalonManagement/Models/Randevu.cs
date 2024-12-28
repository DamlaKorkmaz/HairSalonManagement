using System.ComponentModel.DataAnnotations;

namespace HairSalonManagement.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Randevu
	{
		[Key]
		public int RandevuId { get; set; }
		[Required]
		public string Isim { get; set; }
		[Required]
		public string Soyisim { get; set; }
		[Required]
		public string Telefon { get; set; }
		[Required]
		public string KullaniciEmail { get; set; }
		[Required]
		public string UzmanIsim { get; set; }  // Uzman adı
		[Required]
		public DateTime RandevuTarihi { get; set; }
		
	}
}

