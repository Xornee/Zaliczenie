using Data.Entities;
using Zaliczenie.Models;

namespace Zaliczenie.Mappers
{
    public class StudentMapper
    {
        public static StudentViewModel FromEntity(StudentEntity entity)
        {
            return new StudentViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                IndexNumber = entity.IndexNumber,
                Birth = entity.Birth,
            };
        }

        public static StudentEntity ToEntity(StudentViewModel model)
        {
            return new StudentEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Name = model.Name,
                Email = model.Email,
                IndexNumber = model.IndexNumber,
                Birth = model.Birth.Value
            };
        }
    }
}
