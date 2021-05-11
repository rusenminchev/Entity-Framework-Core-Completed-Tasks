using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    using static DataValidations.Diagnose;

    public class Diagnose
    {
        public int DiagnoseId { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CommentsMaxLenght)]
        public string Comments { get; set; }

        public int? PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
