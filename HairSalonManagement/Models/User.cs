namespace HairSalonManagement.Models
{
	public class User
	{
		public int UserID { get; set; } // Kullanıcı için benzersiz ID
		public string FullName { get; set; } // Kullanıcının adı ve soyadı
		public string Email { get; set; } // Kullanıcının e-posta adresi
		public string Password { get; set; } // Şifre (Hashlenmiş olarak saklanmalı)
		public string Role { get; set; } // Rol: "Admin" veya "User"
	}
}
