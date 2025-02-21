using Data;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Zaliczenie.Models;
using Zaliczenie.Mappers;

namespace Zaliczenie.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private UniversityContext _context;

        public EnrollmentService(UniversityContext context)
        {
            _context = context;
        }

        public int Add(EnrollmentViewModel enrollment)
        {
            var entity = EnrollmentMapper.ToEntity(enrollment);
            var added = _context.Enrollments.Add(entity);
            _context.SaveChanges();
            return added.Entity.Id;
        }

        public void Update(EnrollmentViewModel enrollment)
        {
            var entity = EnrollmentMapper.ToEntity(enrollment);
            _context.Enrollments.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Enrollments.Find(id);
            if (entity != null)
            {
                _context.Enrollments.Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<EnrollmentViewModel> FindAll()
        {
            return _context.Enrollments
                .Select(e => EnrollmentMapper.FromEntity(e))
                .ToList();
        }

        public EnrollmentViewModel? FindById(int id)
        {
            var entity = _context.Enrollments.Find(id);
            return entity != null ? EnrollmentMapper.FromEntity(entity) : null;
        }
    }
}
