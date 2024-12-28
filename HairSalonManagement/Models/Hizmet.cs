using System.ComponentModel.DataAnnotations;

namespace HairSalonManagement.Models
{
	public class Hizmet
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Ad { get; set; }

		[StringLength(500)]
		public string Aciklama { get; set; }

		[Required]
		public string ResimUrl { get; set; }

		[Required]
		public decimal Fiyat { get; set; }
	}
}
