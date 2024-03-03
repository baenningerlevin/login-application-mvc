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
			Login session = Login.GetSession();

			if (session != null)
			{
				if (session.IsAdmin)
				{
					return RedirectToAction("Admin");
				}
				else
				{
					return View();
				}
			}
			return RedirectToAction("Index", "Login");
		}

		public IActionResult LogOut()
		{
			Login session = Login.GetSession();
			session.IsLoggedIn = false;
			Login.LogOut();

			_db.Update(session);
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

			// Server-side validation
			if (existingUser != null)
			{
				ModelState.AddModelError("Username", "Username already exists");
			}

			if (user.Password != confirmPassword)
			{
				ModelState.AddModelError("", "Passwords do not match");
			}

			if (user.Password.Length < 4)
			{
				ModelState.AddModelError("Password", "Password must be at least 4 characters long");
			}

			if (user.Username.Length < 3)
			{
				ModelState.AddModelError("Username", "Username must be at least 3 characters long");
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

		public IActionResult Admin()
		{
			Login session = Login.GetSession();

			if (session != null)
			{
				if (session.IsAdmin)
				{
					return View();
				}
				else
				{
					return RedirectToAction("Index", "Users");
				}
			}
			else
			{
				return RedirectToAction("Index", "Users");
			}
		}
	}
}
