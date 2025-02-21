using Data;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Zaliczenie.Models;
using Zaliczenie.Mappers;

namespace Zaliczenie.Services
{
    public class InstructorService : IInstructorService
    {
        private UniversityContext _context;

        public InstructorService(UniversityContext context)
        {
            _context = context;
        }

        public int Add(InstructorViewModel instructor)
        {
            var entity = InstructorMapper.ToEntity(instructor);
            var added = _context.Instructors.Add(entity);
            _context.SaveChanges();
            return added.Entity.Id;
        }

        public void Update(InstructorViewModel instructor)
        {
            var entity = InstructorMapper.ToEntity(instructor);
            _context.Instructors.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Instructors.Find(id);
            if (entity != null)
            {
                _context.Instructors.Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<InstructorViewModel> FindAll()
        {
            return _context.Instructors.Select(e => InstructorMapper.FromEntity(e)).ToList();
        }

        public InstructorViewModel? FindById(int id)
        {
            var entity = _context.Instructors.Find(id);
            return entity != null ? InstructorMapper.FromEntity(entity) : null;
        }
    }
}
