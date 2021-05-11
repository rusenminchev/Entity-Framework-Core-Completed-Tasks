using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    using static DataValidations.Visitations;

    public class Visitation
    {
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(CommentsMaxLenght)]
        public string Comments { get; set; }

        public int? PatientId { get; set; }

        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
