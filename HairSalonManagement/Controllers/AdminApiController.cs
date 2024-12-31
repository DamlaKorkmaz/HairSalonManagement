using HairSalonManagement.Models;  // Modelin bulunduğu namespace
using Microsoft.AspNetCore.Mvc;   // API Controller için gerekli namespace
using Microsoft.EntityFrameworkCore;

namespace HairSalonManagement.Controllers
{
	// [ApiController] attribute'u, bu sınıfın API controller olduğunu belirtir
	[ApiController]
	// [Route] attribute'u ile endpoint yolu belirlenir
	[Route("api/[controller]")]
	public class AdminApiController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		// Constructor: Veritabanı bağlamını alıyoruz
		public AdminApiController(ApplicationDbContext context)
		{
			_context = context;
		}

		// API üzerinden randevuları listelemek için GET isteği
		[HttpGet("randevular")]
		public async Task<IActionResult> GetRandevular()
		{
			var randevular = await _context.Randevular.ToListAsync();  // Veritabanından randevuları alıyoruz
			return Ok(randevular);  // JSON formatında döndürüyoruz
		}

		// API üzerinden yeni randevu eklemek için POST isteği
		[HttpPost("randevu")]
		public async Task<IActionResult> AddRandevu([FromBody] Randevu randevu)
		{
			if (!ModelState.IsValid)  // Model validasyonu
			{
				return BadRequest(ModelState);  // Geçersiz model durumunda hata dönüyoruz
			}

			await _context.Randevular.AddAsync(randevu);  // Yeni randevuyu ekliyoruz
			await _context.SaveChangesAsync();  // Değişiklikleri kaydediyoruz

			return CreatedAtAction(nameof(GetRandevular), new { id = randevu.RandevuId }, randevu);  // Yeni oluşturulan randevuyu döndürüyoruz
		}
	}
}
