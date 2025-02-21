using Data.Entities;
using Zaliczenie.Models;

namespace Zaliczenie.Mappers
{
    public class CourseMapper
    {
        public static CourseViewModel FromEntity(CourseEntity entity)
        {
            return new CourseViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Credits = entity.Credits,
                InstructorId = entity.InstructorId
            };
        }

        public static CourseEntity ToEntity(CourseViewModel model)
        {
            return new CourseEntity
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Title = model.Title,
                Credits = model.Credits,
                InstructorId = model.InstructorId
            };
        }
    }
}
