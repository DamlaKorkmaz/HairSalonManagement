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
	public DbSet<EmployeeService> EmployeeServices { get; set; } // Ara tablo

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// PostgreSQL'de varsayılan şema
		modelBuilder.HasDefaultSchema("public");

		// Many-to-Many Relationship: Employee <-> Service
		modelBuilder.Entity<EmployeeService>()
			.HasKey(es => new { es.EmployeeID, es.ServiceID }); // Composite Key

		modelBuilder.Entity<EmployeeService>()
			.HasOne(es => es.Employee)
			.WithMany(e => e.EmployeeServices)
			.HasForeignKey(es => es.EmployeeID);

		modelBuilder.Entity<EmployeeService>()
			.HasOne(es => es.Service)
			.WithMany(s => s.EmployeeServices)
			.HasForeignKey(es => es.ServiceID);

	}
}
