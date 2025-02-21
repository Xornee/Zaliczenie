using Data.Entities;
using Zaliczenie.Models;

namespace Zaliczenie.Mappers
{
    public class EnrollmentMapper
    {
        public static EnrollmentViewModel FromEntity(EnrollmentEntity entity)
        {
            return new EnrollmentViewModel
            {
                Id = entity.Id,
                CourseID = entity.CourseID,
                StudentID = entity.StudentID,
                Grade = entity.Grade
            };
        }

        public static EnrollmentEntity ToEntity(EnrollmentViewModel model)
        {
            return new EnrollmentEntity
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                CourseID = model.CourseID,
                StudentID = model.StudentID,
                Grade = model.Grade
            };
        }
    }
}
