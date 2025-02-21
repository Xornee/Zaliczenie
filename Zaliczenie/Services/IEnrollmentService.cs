using System.Collections.Generic;
using Zaliczenie.Models;

namespace Zaliczenie.Services
{
    public interface IEnrollmentService
    {
        int Add(EnrollmentViewModel enrollment);
        void Update(EnrollmentViewModel enrollment);
        void Delete(int id);
        List<EnrollmentViewModel> FindAll();
        EnrollmentViewModel? FindById(int id);
    }
}
