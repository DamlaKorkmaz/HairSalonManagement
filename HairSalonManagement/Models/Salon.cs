using System.Collections.Generic;

namespace HairSalonManagement.Models
{
	public class Salon
	{
		public int SalonID { get; set; } // Primary Key
		public string Name { get; set; }
		public string Address { get; set; }
		public string OpeningHours { get; set; }
		public string ClosingHours { get; set; }

		// Navigation Property
		public ICollection<Employee> Employees { get; set; } // Employee ile ilişki
	}
}
