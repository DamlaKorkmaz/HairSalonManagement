
using HairSalonManagement.Models;
using Microsoft.EntityFrameworkCore;


	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Salon> Salons { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<Appointment> Appointments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Appointment -> Salon
			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Salon)
				.WithMany()
				.HasForeignKey(a => a.SalonID)
				.OnDelete(DeleteBehavior.Restrict); // Cascade Delete'i önler

			// Appointment -> Employee
			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Employee)
				.WithMany()
				.HasForeignKey(a => a.EmployeeID)
				.OnDelete(DeleteBehavior.Restrict); // Cascade Delete'i önler

			// Employee -> Salon
			modelBuilder.Entity<Employee>()
				.HasOne(e => e.Salon)
				.WithMany(s => s.Employees)
				.HasForeignKey(e => e.SalonID)
				.OnDelete(DeleteBehavior.Cascade); // Sadece Salon -> Employee ilişkisi cascade kalabilir
		}

	}

