using Data;
using Data.Entities;
using System;
using Zaliczenie.Models;
using Zaliczenie.Mappers;

namespace Zaliczenie.Services
{
    public class StudentService : IStudentService
    {
        private UniversityContext _context;

        public StudentService(UniversityContext context)
        {
            _context = context;
        }

        public int Add(StudentViewModel contact)
        {
            var e = _context.Students.Add(StudentMapper.ToEntity(contact));
            _context.SaveChanges();
            return e.Entity.Id;
        }

        public void Delete(int id)
        {
            StudentEntity? find = _context.Students.Find(id);
            if (find != null)
            {
                _context.Students.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<StudentViewModel> FindAll()
        {
            return _context.Students.Select(e => StudentMapper.FromEntity(e)).ToList(); ;
        }

        public StudentViewModel? FindById(int id)
        {
            return StudentMapper.FromEntity(_context.Students.Find(id));
        }

        public void Update(StudentViewModel contact)
        {
            _context.Students.Update(StudentMapper.ToEntity(contact));
            _context.SaveChanges();
        }
    }
}
