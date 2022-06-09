using lab4.Models;
using lab4.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab4.Controllers
{
    public class CoursesController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public CoursesController()
            {
                 _dbContext = new ApplicationDbContext();
            }

        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
            };
            return View(ViewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("create",viewModel);
            }
            var course = new Course()
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place,

            };
            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");

           
        }
    }
}