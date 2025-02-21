using Microsoft.AspNetCore.Mvc;
using Zaliczenie.Models;
using Zaliczenie.Services;

namespace Zaliczenie.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // GET: /Instructor/
        [HttpGet]
        public IActionResult Index()
        {
            var instructors = _instructorService.FindAll();
            return View(instructors);
        }

        // GET: /Instructor/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Instructor/Create
        [HttpPost]
        public IActionResult Create(InstructorViewModel instructor)
        {
            if (ModelState.IsValid)
            {
                _instructorService.Add(instructor);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: /Instructor/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = _instructorService.FindById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: /Instructor/Edit
        [HttpPost]
        public IActionResult Edit(InstructorViewModel instructor)
        {
            if (ModelState.IsValid)
            {
                _instructorService.Update(instructor);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: /Instructor/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var instructor = _instructorService.FindById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: /Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _instructorService.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Instructor/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var instructor = _instructorService.FindById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
    }
}
