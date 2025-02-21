using Microsoft.AspNetCore.Mvc;
using Zaliczenie.Models;
using Zaliczenie.Services;

namespace Zaliczenie.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // GET: /Enrollment/
        [HttpGet]
        public IActionResult Index()
        {
            var enrollments = _enrollmentService.FindAll();
            return View(enrollments);
        }

        // GET: /Enrollment/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Enrollment/Create
        [HttpPost]
        public IActionResult Create(EnrollmentViewModel enrollment)
        {
            if (ModelState.IsValid)
            {
                _enrollmentService.Add(enrollment);
                return RedirectToAction("Index");
            }
            return View(enrollment);
        }

        // GET: /Enrollment/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var enrollment = _enrollmentService.FindById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: /Enrollment/Edit
        [HttpPost]
        public IActionResult Edit(EnrollmentViewModel enrollment)
        {
            if (ModelState.IsValid)
            {
                _enrollmentService.Update(enrollment);
                return RedirectToAction("Index");
            }
            return View(enrollment);
        }

        // GET: /Enrollment/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var enrollment = _enrollmentService.FindById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: /Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _enrollmentService.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Enrollment/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var enrollment = _enrollmentService.FindById(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }
    }
}
