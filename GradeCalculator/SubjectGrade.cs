using System;

public class SubjectGrade
{
    public string SubjectName { get; set; }
    public decimal Grade { get; set; }
    public decimal CreditHours { get; set; }
    public DateTime DateRecorded { get; set; }

    public SubjectGrade(string subject, decimal grade, decimal credits)
    {
        SubjectName = subject;
        Grade = grade;
        CreditHours = credits;
        DateRecorded = DateTime.Now;
    }
}