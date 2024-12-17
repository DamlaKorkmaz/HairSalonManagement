
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
		modelBuilder.HasDefaultSchema("public"); // PostgreSQL'de şemayı belirtmek için

	}
}


