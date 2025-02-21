using Zaliczenie.Models;
using System.Collections.Generic;

namespace Zaliczenie.Services
{
    public interface ICourseService
    {
        int Add(CourseViewModel course);
        void Update(CourseViewModel course);
        void Delete(int id);
        List<CourseViewModel> FindAll();
        CourseViewModel? FindById(int id);
    }
}
