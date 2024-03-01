using LoginApplication.Data;
using LoginApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
	public class UsersController : Controller
	{
		private ApplicationDbContext _db;
		public UsersController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			Login login = Login.GetInstance();

			if (login != null)
			{
				return View();
			}
			return RedirectToAction("Index", "Login");
		}

		public IActionResult LogOut()
		{
			Login login = Login.GetInstance();
			login.IsLoggedIn = false;
			Login.LogOut();

			_db.Update(login);
			_db.SaveChanges();
			return RedirectToAction("Index", "Login");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user, string confirmPassword)
		{
			User existingUser = _db.Users.FirstOrDefault(u => u.Username == user.Username);
			if (existingUser != null)
			{
				ModelState.AddModelError("Username", "Username already exists");
			}

			if (user.Password != confirmPassword)
			{
				ModelState.AddModelError("", "Passwords do not match");
			}

			if (ModelState.IsValid)
			{
				user.IsAdmin = false;
				user.IsLoggedIn = false;

				_db.Users.Add(user);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}
	}
}
