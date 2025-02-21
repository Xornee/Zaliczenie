using System.Collections.Generic;
using Zaliczenie.Models;

namespace Zaliczenie.Services
{
    public interface IInstructorService
    {
        int Add(InstructorViewModel instructor);
        void Update(InstructorViewModel instructor);
        void Delete(int id);
        List<InstructorViewModel> FindAll();
        InstructorViewModel? FindById(int id);
    }
}
