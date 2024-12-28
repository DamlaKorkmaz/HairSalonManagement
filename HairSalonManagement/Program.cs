using HairSalonManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Authentication ve Authorization yapılandırması
		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Varsayılan scheme: "Cookies"
			.AddCookie(options =>
			{
				options.LoginPath = "/Auth/Login"; // Giriş sayfası
				options.LogoutPath = "/Auth/Logout"; // Çıkış sayfası
				options.AccessDeniedPath = "/Auth/AccessDenied"; // Yetkisiz erişim
				options.Cookie.Name = "HairSalonCookie"; // Cookie'nin adı
			});

		builder.Services.AddAuthorization(); // Yetkilendirme

		// DbContext ve MVC yapılandırması
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services.AddControllersWithViews();

		var app = builder.Build();

		// Middleware sırası önemli
		app.UseRouting();
		app.UseAuthentication(); // Authentication middleware'i ekle
		app.UseAuthorization();  // Authorization middleware'i ekle
		app.UseStaticFiles(); // Bu, wwwroot altındaki dosyaların sunulmasına olanak sağlar.

		// Default route
		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}
