using System;
using System.Collections.Generic;
using System.Linq;

public class GradeCalculator
{
    private List<Student> students = new List<Student>();

    public void AddGradeToStudent(string studentId, SubjectGrade grade)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);

        if (student == null)
            throw new ArgumentException("Estudiante no encontrado", nameof(studentId));

        student.Grades.Add(grade);
    }

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
            if (grade.CreditHours <= 0)
                throw new ArgumentException("Los créditos deben ser mayores que cero", nameof(studentId));

            weightedSum += grade.Grade * grade.CreditHours;
            totalCredits += grade.CreditHours;
        }

        return weightedSum / totalCredits;
    }

    public bool IsStudentEligibleForScholarship(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            return false;

        decimal average = CalculateWeightedAverage(studentId);

        return average >= 85;
    }

    public void UpdateGrade(string studentId, string subjectName, decimal newGrade)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        if (newGrade < 0 || newGrade > 100)
            throw new ArgumentOutOfRangeException(nameof(newGrade), "La calificación debe estar entre 0 y 100");

        var gradeRecord = student.Grades.FirstOrDefault(g => g.SubjectName == subjectName);
        
        if (gradeRecord != null)
            gradeRecord.Grade = newGrade;
    }

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
            sum += (grade.Grade * 4) / 100;
        }

        return sum / student.Grades.Count;
    }

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

        return rankedStudents
            .Take(topCount)
            .Select(student => student.Name)
            .ToList();
    }

    public void RemoveGrade(string studentId, int gradeIndex)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        if (gradeIndex < 0 || gradeIndex >= student.Grades.Count)
            throw new ArgumentOutOfRangeException(nameof(gradeIndex));

        student.Grades.RemoveAt(gradeIndex);
    }

    public decimal CalculateSimpleAverage(string studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        
        if (student == null)
            throw new ArgumentException("Estudiante no encontrado");

        if (student.Grades.Count == 0)
            throw new ArgumentException("El estudiante no tiene calificaciones", nameof(studentId));

        decimal sum = 0;
        
        foreach (var grade in student.Grades)
        {
            if (grade.Grade < 0 || grade.Grade > 100)
                throw new ArgumentException("Las calificaciones deben estar entre 0 y 100", nameof(studentId));

            sum += grade.Grade;
        }

        return sum / student.Grades.Count;
    }

    public void RegisterStudent(Student student)
    {
        students.Add(student);
    }

    public Student? GetStudent(string id)
    {
        return students.FirstOrDefault(s => s.StudentId == id);
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }
}