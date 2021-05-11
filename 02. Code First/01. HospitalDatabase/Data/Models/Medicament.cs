using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    using static DataValidations.Medicament;

    public class Medicament
    {
        public int MedicamentId { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        string Name { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }

    }
}
