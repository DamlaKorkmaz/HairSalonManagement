using System.ComponentModel.DataAnnotations.Schema;

namespace HairSalonManagement.Models
{
	public class Appointment
	{
		public int AppointmentID { get; set; } // Primary Key
		public int SalonID { get; set; } // Foreign Key
		public int EmployeeID { get; set; } // Foreign Key
		public DateTime AppointmentDate { get; set; }
		public string ServiceName { get; set; }

		// Navigation Properties
		[ForeignKey("SalonID")]
		public Salon Salon { get; set; } // Salon ile ilişki

		[ForeignKey("EmployeeID")]
		public Employee Employee { get; set; } // Employee ile ilişki
	}
}
