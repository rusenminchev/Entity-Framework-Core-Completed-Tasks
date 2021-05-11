using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var hospitalDatabase = new HospitalContext();

            //var patient = new Patient
            //{
            //    FirstName = "Ivan",
            //    LastName = "Goshov",
            //    Address = "Kazanka 19",
            //    Email = "2132qwe1@sto.bg",
            //    HasInsurance = true
            //};

            //var patient2 = new Patient
            //{
            //    FirstName = "Arnol",
            //    LastName = "Geshov",
            //    Address = "Brezit 123",
            //    Email = "21321ewq@sto.bg",
            //    HasInsurance = true
            //};

            //hospitalDatabase.Patients.Add(patient);
            //hospitalDatabase.Patients.Add(patient2);

            //var doctor = new Doctor
            //{
            //    Name = "House MD",
            //    Specialty = "Random Medicine",
            //};

            //hospitalDatabase.Doctors.Add(doctor);

            //var visitation = new Visitation
            //{
            //    Date = DateTime.UtcNow,
            //    Comments = "Everything is super fine! The patient is healthy AF!",
            //    PatientId = 2,
            //    DoctorId = 1
            //};

            //hospitalDatabase.Visitations.Add(visitation);

            //hospitalDatabase.SaveChanges();

            hospitalDatabase.Database.Migrate();
        }
    }
}
