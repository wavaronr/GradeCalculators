using System;
using System.Collections.Generic;

public class Student
{
    public string StudentId { get; set; }
    public string Name { get; set; }
    public List<SubjectGrade> Grades { get; set; }
    public string Major { get; set; }

    public Student(string id, string name, string major)
    {
        StudentId = id;
        Name = name;
        Major = major;
        Grades = new List<SubjectGrade>();
    }
}