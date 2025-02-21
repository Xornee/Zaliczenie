using Data;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Zaliczenie.Models;
using Zaliczenie.Mappers;

namespace Zaliczenie.Services
{
    public class CourseService : ICourseService
    {
        private UniversityContext _context;

        public CourseService(UniversityContext context)
        {
            _context = context;
        }

        public int Add(CourseViewModel course)
        {
            var entity = CourseMapper.ToEntity(course);
            var added = _context.Courses.Add(entity);
            _context.SaveChanges();
            return added.Entity.Id;
        }

        public void Update(CourseViewModel course)
        {
            var entity = CourseMapper.ToEntity(course);
            _context.Courses.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Courses.Find(id);
            if (entity != null)
            {
                _context.Courses.Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<CourseViewModel> FindAll()
        {
            return _context.Courses.Select(e => CourseMapper.FromEntity(e)).ToList();
        }

        public CourseViewModel? FindById(int id)
        {
            var entity = _context.Courses.Find(id);
            return entity != null ? CourseMapper.FromEntity(entity) : null;
        }
    }
}
