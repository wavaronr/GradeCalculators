using System;
using Xunit;

public class GradeCalculatorTests
{
    [Fact]
    public void CalculateSimpleAverage_ValidGrades_ReturnsCorrectAverage()
    {
        var calc = CreateCalculator(80m, 85m, 90m);
        Assert.Equal(85m, calc.CalculateSimpleAverage("S001"));
    }

    [Fact]
    public void CalculateSimpleAverage_SingleGrade_ReturnsSameGrade()
    {
        var calc = CreateCalculator(92m);
        Assert.Equal(92m, calc.CalculateSimpleAverage("S001"));
    }

    [Fact]
    public void CalculateSimpleAverage_NegativeGrade_Throws()
    {
        var calc = CreateCalculator(80m, -10m, 90m);
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
    }

    [Fact]
    public void CalculateSimpleAverage_GradeGreaterThan100_Throws()
    {
        var calc = CreateCalculator(80m, 150m, 90m);
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
    }

    [Fact]
    public void CalculateSimpleAverage_EmptyList_Throws()
    {
        var calc = CreateCalculator();
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
    }

    [Fact]
    public void CalculatePA_SingleGrade_ReturnsConvertedGrade()
    {
        var calc = CreateCalculator(80m);
        Assert.Equal(3.2m, calc.CalculatePA("S001"));
    }

    private static GradeCalculator CreateCalculator(params decimal[] grades)
    {
        var calc = new GradeCalculator();
        calc.RegisterStudent(new Student("S001", "Estudiante de prueba", "Sistemas"));

        foreach (decimal grade in grades)
        {
            calc.AddGradeToStudent("S001", new SubjectGrade("Materia", grade, 1m));
        }

        return calc;
    }
}