using Microsoft.AspNetCore.Mvc;
using Data;
using Data.Entities;
using Zaliczenie.Mappers;
using Zaliczenie.Models;
using Zaliczenie.Services;
using System.Linq;

namespace Zaliczenie.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: /Student/
        [HttpGet]
        public IActionResult Index()
        {
            var students = _studentService.FindAll();
            return View(students);
        }

        // GET: /Student/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        public IActionResult Create(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Student/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentService.FindById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: /Student/Edit
        [HttpPost]
        public IActionResult Edit(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Student/Delete/5 (shows confirmation view)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.FindById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5 (confirmation form posts here)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Student/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _studentService.FindById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: /Student/Search?query=...
        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            var filtered = _studentService.FindAll()
                .Where(s =>
                    (!string.IsNullOrEmpty(s.Name) && s.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.Email) && s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.IndexNumber) && s.IndexNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                ).ToList();

            return View("Index", filtered);
        }
    }
}
