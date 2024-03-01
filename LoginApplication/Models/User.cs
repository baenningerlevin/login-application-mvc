using System.ComponentModel.DataAnnotations;

namespace LoginApplication.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[MaxLength(25, ErrorMessage = "Username is too long")]
		[MinLength(3, ErrorMessage = "Username is too short")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "This field is required")]
		[MinLength(4, ErrorMessage = "Password is too short")]
		[MaxLength(55, ErrorMessage = "Password is too long")]
		public string Password { get; set; } = string.Empty;

		public bool IsAdmin { get; set; }
		public bool IsLoggedIn { get; set; }
	}
}
