using Microsoft.EntityFrameworkCore;

namespace HairSalonManagement.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Kullanici> Kullanicilar { get; set; }
		public DbSet<Uzman> Uzmanlar { get; set; }
		public DbSet<Randevu> Randevular { get; set; }
		public DbSet<Hizmet> Hizmetler { get; set; }


		// Parametreli yapıcı eklendi
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)  // DbContextOptions ile base sınıfı başlatıyoruz
		{
		}

		public ApplicationDbContext()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KuaforSalonDb;Trusted_Connection=True;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			/*// Randevu ile Kullanıcı ilişkisi
			modelBuilder.Entity<Randevu>()
				.HasOne(r => r.Kullanici)
				.WithMany(k => k.Randevular)
				.HasForeignKey(r => r.KullaniciId);*/

			// Randevu ile Uzman ilişkisi
			/*modelBuilder.Entity<Randevu>()
				.HasOne(r => r.Uzman)
				.WithMany(u => u.Randevular)
				.HasForeignKey(r => r.UzmanId);*/
		}
	}
}
