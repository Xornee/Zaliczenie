using Microsoft.AspNetCore.Mvc;
using Zaliczenie.Models;
using Zaliczenie.Services;

namespace Zaliczenie.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: /Course/
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _courseService.FindAll();
            return View(courses);
        }

        // GET: /Course/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Course/Create
        [HttpPost]
        public IActionResult Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Add(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: /Course/Edit
        [HttpPost]
        public IActionResult Edit(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Update(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: /Course/Delete/5 (confirmation view)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Course/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _courseService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
