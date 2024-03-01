namespace LoginApplication.Models
{
	public class Login : User
	{
		private static Login? _instance = null;

		public static Login GetInstance()
		{
			return _instance;
		}

		public static Login LogIn(User user)
		{
			if (_instance == null)
			{
				_instance = new Login();
			}

			_instance.Id = user.Id;
			_instance.Username = user.Username;
			_instance.Password = user.Password;
			_instance.IsAdmin = user.IsAdmin;
			_instance.IsLoggedIn = true;

			return _instance;
		}

		public static void LogOut()
		{
			_instance = null;
		}
	}
}
