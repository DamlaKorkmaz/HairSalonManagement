namespace HairSalonManagement.Models
{
	using System.ComponentModel.DataAnnotations;

	public class Kullanici
	{
		[Key]
		public int KullaniciId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Isim { get; set; }

		[Required]
		[MaxLength(50)]
		public string Soyisim { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
		public string Sifre { get; set; }

		
	}
}
