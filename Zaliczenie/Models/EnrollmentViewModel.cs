using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Data.Entities; // To reference the Grade enum

namespace Zaliczenie.Models
{
    public class EnrollmentViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Student is required.")]
        public int StudentID { get; set; }

        // Grade is optional
        public Grade? Grade { get; set; }
    }
}
