using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("student")]
    public class StudentEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MinLength(5)]
        [MaxLength(6)]
        [Column("index")]
        [Required]
        public string IndexNumber { get; set; }

        [Column("birth_date")]
        public DateTime Birth { get; set; }

        // One student can enroll in many courses.
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
    }
}
