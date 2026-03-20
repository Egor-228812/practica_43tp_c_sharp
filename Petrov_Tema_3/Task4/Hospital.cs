using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class Hospital
    {
        public Patient[] Patients { get; set; }

        public Hospital(Patient[] patients)
        {
            Patients = patients;
        }

        public Patient[] GetPatientsByDiagnosis(string diagnosis)
        {
            int count = 0;
            for (int i = 0; i < Patients.Length; i++)
                if (Patients[i].Diagnosis == diagnosis) count++;

            Patient[] result = new Patient[count];
            int index = 0;
            for (int i = 0; i < Patients.Length; i++)
                if (Patients[i].Diagnosis == diagnosis) result[index++] = Patients[i];

            return result;
        }

        public Patient GetOldestPatient()
        {
            if (Patients.Length == 0) return null;
            Patient oldest = Patients[0];
            for (int i = 1; i < Patients.Length; i++)
                if (Patients[i].Age > oldest.Age) oldest = Patients[i];
            return oldest;
        }
    }
}
