using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Zaliczenie.Models
{
    public class CourseViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Credits are required.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "Instructor is required.")]
        public int InstructorId { get; set; }
    }
}
