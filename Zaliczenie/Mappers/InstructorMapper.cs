using Data.Entities;
using Zaliczenie.Models;

namespace Zaliczenie.Mappers
{
    public class InstructorMapper
    {
        public static InstructorViewModel FromEntity(InstructorEntity entity)
        {
            return new InstructorViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                AcademicTitle = entity.AcademicTitle
            };
        }

        public static InstructorEntity ToEntity(InstructorViewModel model)
        {
            return new InstructorEntity
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Name = model.Name,
                AcademicTitle = model.AcademicTitle
            };
        }
    }
}
