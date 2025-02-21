using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Zaliczenie.Models
{
    public class InstructorViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Academic Title is required.")]
        [MaxLength(50)]
        public string AcademicTitle { get; set; }
    }
}
