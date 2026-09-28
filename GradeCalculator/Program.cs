using System;

class Program
{
    public static void Main()
    {
        var system = new GradeCalculator();
        var student1 = new Student("E001", "Carlos Mendoza", "Ingeniería de Sistemas");
        var student2 = new Student("E002", "María Rodríguez", "Ingeniería de Sistemas");
        system.RegisterStudent(student1);
        system.RegisterStudent(student2);

        system.AddGradeToStudent("E001", new SubjectGrade("Matemáticas", 95m, 4m));
        system.AddGradeToStudent("E001", new SubjectGrade("Física", 88m, 3));
        system.AddGradeToStudent("E001", new SubjectGrade("Programación", 92m, 4));

        Console.WriteLine($"Promedio simple de {student1.Name}: {system.CalculateSimpleAverage("E001"):F2}");
        Console.WriteLine($"Promedio ponderado: {system.CalculateWeightedAverage("E001"):F2}");
        Console.WriteLine($"PA (escala 0-4): {system.CalculatePA("E001"):F2}");
        Console.WriteLine($"Elegible para beca: {system.IsStudentEligibleForScholarship("E001")}");
        Console.WriteLine($"Mejores estudiantes: {string.Join(", ", system.GetTopStudents(5))}");
    }
}