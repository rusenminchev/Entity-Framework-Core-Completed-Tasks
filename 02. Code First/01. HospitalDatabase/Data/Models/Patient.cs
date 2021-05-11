namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataValidations.Patient;

    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(AddressMaxLenght)]
        public string Address { get; set; }

        [Required]
        [MaxLength(EmailMaxLenght)]
        public string Email { get; set; }

        [Required]
        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; } = new HashSet<Visitation>();

        public ICollection<Diagnose> Diagnoses { get; set; } = new HashSet<Diagnose>();

        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
