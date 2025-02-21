using Zaliczenie.Models;

namespace Zaliczenie.Services
{
    public interface IStudentService
    {
        int Add(StudentViewModel item);
        void Delete(int id);
        void Update(StudentViewModel item);
        List<StudentViewModel> FindAll();
        StudentViewModel? FindById(int id);
    }
}
