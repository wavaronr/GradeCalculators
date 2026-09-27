using System;
using System.Collections.Generic;
using System.Linq;

// ================================================================
// VERSIÓN CON ERRORES SOFISTICADOS - SISTEMA ACADÉMICO REAL
// ================================================================

public class GradeCalculator
{
    private List<Student> students = new List<Student>();

    // ERROR 1: No verifica si el estudiante existe
    public void AddGradeToStudent(string studentId, SubjectGrade grade)
    {
        Console.WriteLine($"DEBUG: Agregando calificación {grade.Grade} para {studentId} en {grade.SubjectName}");
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        student.Grades.Add(grade);  // NullReferenceException si no existe
        
    }

    // ERROR 2: Cálculo incorrecto del promedio ponderado
    public decimal CalculateWeightedAverage(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        if (student.Grades.Count == 0)
            return 0;

        decimal weightedSum = 0;
        decimal totalCredits = 0;

        foreach (var grade in student.Grades)
        {
            // ERROR: Multiplica por créditos incorrectamente
            weightedSum += grade.Grade * grade.CreditHours;
            totalCredits += grade.CreditHours;
        }

        // ERROR CRÍTICO: No normaliza el promedio
        // Debería dividir por 100 y luego por créditos
        return weightedSum / totalCredits;
    }

    // ERROR 3: Lógica de condición invertida en validación
    public bool IsStudentEligibleForScholarship(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            return false;

        decimal average = CalculateWeightedAverage(studentId);

        // ERROR: Condición invertida (debería ser >=, no <)
        if (average < 85)  // INCORRECTO - rechaza buenos estudiantes
        {
            return true;
        }

        return false;
    }

    // ERROR 4: Actualización sin validación de rango
    public void UpdateGrade(string studentId, string subjectName, decimal newGrade)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        var gradeRecord = student.Grades.FirstOrDefault(g => g.SubjectName == subjectName);
        
        if (gradeRecord != null)
        {
            // ERROR: No valida que la calificación esté entre 0 y 100
            gradeRecord.Grade = newGrade;
        }
    }

    // ERROR 5: Cálculo de PA incorrecto
    public decimal CalculatePA(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        if (student.Grades.Count == 0)
            throw new InvalidOperationException("El estudiante no tiene calificaciones");

        decimal sum = 0;
        
        foreach (var grade in student.Grades)
        {
            // ERROR: Conversión incorrecta de calificación a PA (escala 0-4)
            // La fórmula correcta es: (grade / 100) * 4
            // Pero aquí hace: grade * 4 / 100 (incorrecto en orden de operaciones)
            sum += (grade.Grade * 4) / 100;
        }

        // ERROR: No divide entre cantidad de calificaciones
        return sum;
    }

    // ERROR 6: Ranking de estudiantes con error de índice
    public List<string> GetTopStudents(int topCount)
    {
        if (students.Count == 0)
            throw new InvalidOperationException("No hay estudiantes registrados");

        var rankedStudents = students
            .OrderByDescending(s => 
            {
                try
                {
                    return CalculatePA(s.StudentId);
                }
                catch
                {
                    return 0;
                }
            })
            .ToList();

        List<string> topStudents = new List<string>();
        
        for (int i = 0; i < topCount; i++)
        {
            // ERROR: No valida si i está dentro del rango
            topStudents.Add(rankedStudents[i].Name);  // IndexOutOfRangeException posible
        }

        return topStudents;
    }

    // ERROR 7: Eliminación con índice sin validación
    public void RemoveGrade(string studentId, int gradeIndex)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        // ERROR: No valida si el índice existe
        student.Grades.RemoveAt(gradeIndex);
    }

    // ERROR 8: Cálculo de promedio simple sin considerar créditos
    public decimal CalculateSimpleAverage(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        decimal sum = 0;
        
        foreach (var grade in student.Grades)
        {
            sum += grade.Grade;
            Console.WriteLine($"DEBUG: Sumando calificación {grade.Grade} de {grade.SubjectName}, suma actual: {sum}");
        }

        // ERROR: Retorna suma en lugar de promedio
        return sum;
    }

    public void RegisterStudent(Student student)
    {
        students.Add(student);
    }

    public Student GetStudent(string id)
    {
        return students.FirstOrDefault(s => s.StudentId == id);
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }
}