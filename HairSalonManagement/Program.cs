using HairSalonManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSalonManagement
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Veritabanı bağlantısını ekle
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Session'u ekle
			builder.Services.AddSession();
			builder.Services.AddDistributedMemoryCache();

			builder.Services.AddDistributedMemoryCache(); // Bellek tabanlı cache
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi (30 dakika)
				options.Cookie.HttpOnly = true; // Güvenlik için yalnızca HTTP üzerinden erişim
				options.Cookie.IsEssential = true;
			});
			// MVC Servislerini ekle
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Session'u aktif et
			app.UseSession();

			// HTTP pipeline'ı yapılandır
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			// Controller route'u
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
