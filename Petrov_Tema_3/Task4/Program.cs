using Task4;

Patient[] patients = new Patient[]
{
    new Patient("Иванов Иван", 45, "Грипп"),
    new Patient("Петрова Анна", 60, "Гипертония"),
    new Patient("Сидоров Петр", 30, "Грипп"),
    new Patient("Козлова Елена", 70, "Артрит")
};

Hospital hospital = new Hospital(patients);

Console.WriteLine("Пациенты с диагнозом 'Грипп':");
Patient[] withFlu = hospital.GetPatientsByDiagnosis("Грипп");
for (int i = 0; i < withFlu.Length; i++)
    withFlu[i].DisplayInfo();

Patient oldest = hospital.GetOldestPatient();
Console.WriteLine("\nСамый старший пациент:");
oldest.DisplayInfo();