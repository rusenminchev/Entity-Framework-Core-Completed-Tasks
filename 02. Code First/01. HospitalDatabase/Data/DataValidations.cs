using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public static class DataValidations
    {
        public static class Patient
        {
            public const int NameMaxLenght = 50;
            public const int AddressMaxLenght = 250;
            public const int EmailMaxLenght = 80;
        }

        public static class Visitations
        {
            public const int CommentsMaxLenght = 250;
        }

        public static class Diagnose
        {
            public const int NameMaxLenght = 50;
            public const int CommentsMaxLenght = 250;
        }

        public static class Medicament
        {
            public const int NameMaxLenght = 50;
        }

        public static class Doctor
        {
            public const int NameMaxLenght = 100;
            public const int SpecialtyMaxLenght = 100;
        }
    }
}
