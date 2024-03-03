﻿using LoginApplication.Data;
using LoginApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginApplication.Controllers
{
	public class LoginController : Controller
	{
		private readonly ApplicationDbContext _db;

		public LoginController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Login login)
		{
			User user = _db.Users.FirstOrDefault(u => u.Username == login.Username);

			// Server-side validation
			if (user == null)
			{
				ModelState.AddModelError("Username", "Invalid Username");
			}
			else if (user.Password != login.Password)
			{
				ModelState.AddModelError("Password", "Invalid Password");
			}

			if (ModelState.IsValid)
			{
				user.IsLoggedIn = true;
				Login session = Login.LogIn(user);

				_db.Update(user);
				_db.SaveChanges();

				return RedirectToAction("Index", "Users");
			}
			else
			{
				return View();
			}
		}
	}
}
