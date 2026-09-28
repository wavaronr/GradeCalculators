using System;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[TestCaseOrderer("GradeCalculatorTestCaseOrderer", "GradeCalculator.Tests")]
public class GradeCalculatorTests
{
    private static readonly SortedSet<string> ApprovedTestNames = new();
    private readonly ITestOutputHelper _output;

    public GradeCalculatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void AddGradeToStudent_UnknownStudent_ThrowsArgumentException()
    {
        var calc = new GradeCalculator();

        Assert.Throws<ArgumentException>(() =>
            calc.AddGradeToStudent("S999", new SubjectGrade("Materia", 80m, 1m)));
        LogApproved();
    }

    [Fact]
    public void CalculateWeightedAverage_GradesWithDifferentCredits_ReturnsWeightedAverage()
    {
        var calc = CreateCalculatorWithCredits((100m, 3m), (80m, 1m));

        Assert.Equal(95m, calc.CalculateWeightedAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculateWeightedAverage_ZeroCredits_ThrowsArgumentException()
    {
        var calc = CreateCalculatorWithCredits((80m, 0m));

        Assert.Throws<ArgumentException>(() => calc.CalculateWeightedAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void IsStudentEligibleForScholarship_AverageAtThreshold_ReturnsTrue()
    {
        var calc = CreateCalculator(85m, 85m);

        Assert.True(calc.IsStudentEligibleForScholarship("S001"));
        LogApproved();
    }

    [Fact]
    public void IsStudentEligibleForScholarship_AverageBelowThreshold_ReturnsFalse()
    {
        var calc = CreateCalculator(84m, 84m);

        Assert.False(calc.IsStudentEligibleForScholarship("S001"));
        LogApproved();
    }

    [Fact]
    public void UpdateGrade_ValidGrade_UpdatesExistingGrade()
    {
        var calc = CreateCalculator(80m);

        calc.UpdateGrade("S001", "Materia", 95m);

        Assert.Equal(95m, calc.GetStudent("S001")!.Grades[0].Grade);
        LogApproved();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void UpdateGrade_GradeOutsideRange_ThrowsAndDoesNotUpdate(decimal newGrade)
    {
        var calc = CreateCalculator(80m);

        Assert.Throws<ArgumentOutOfRangeException>(() => calc.UpdateGrade("S001", "Materia", newGrade));
        Assert.Equal(80m, calc.GetStudent("S001")!.Grades[0].Grade);
        LogApproved();
    }

    [Fact]
    public void CalculateSimpleAverage_ValidGrades_ReturnsCorrectAverage()
    {
        var calc = CreateCalculator(80m, 85m, 90m);
        Assert.Equal(85m, calc.CalculateSimpleAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculateSimpleAverage_SingleGrade_ReturnsSameGrade()
    {
        var calc = CreateCalculator(92m);
        Assert.Equal(92m, calc.CalculateSimpleAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculateSimpleAverage_NegativeGrade_Throws()
    {
        var calc = CreateCalculator(80m, -10m, 90m);
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculateSimpleAverage_GradeGreaterThan100_Throws()
    {
        var calc = CreateCalculator(80m, 150m, 90m);
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculateSimpleAverage_EmptyList_Throws()
    {
        var calc = CreateCalculator();
        Assert.Throws<ArgumentException>(() => calc.CalculateSimpleAverage("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculatePA_SingleGrade_ReturnsConvertedGrade()
    {
        var calc = CreateCalculator(80m);
        Assert.Equal(3.2m, calc.CalculatePA("S001"));
        LogApproved();
    }

    [Fact]
    public void CalculatePA_MultipleGrades_ReturnsAverageOnFourPointScale()
    {
        var calc = CreateCalculator(80m, 100m);

        Assert.Equal(3.6m, calc.CalculatePA("S001"));
        LogApproved();
    }

    [Fact]
    public void GetTopStudents_RequestedCountExceedsRegisteredStudents_ReturnsAvailableStudentsInOrder()
    {
        var calc = new GradeCalculator();
        calc.RegisterStudent(new Student("S001", "Promedio", "Sistemas"));
        calc.RegisterStudent(new Student("S002", "Alto", "Sistemas"));
        calc.RegisterStudent(new Student("S003", "Bajo", "Sistemas"));
        calc.AddGradeToStudent("S001", new SubjectGrade("Materia", 80m, 1m));
        calc.AddGradeToStudent("S002", new SubjectGrade("Materia", 95m, 1m));
        calc.AddGradeToStudent("S003", new SubjectGrade("Materia", 70m, 1m));

        Assert.Equal(new[] { "Alto", "Promedio", "Bajo" }, calc.GetTopStudents(5));
        LogApproved();
    }

    [Fact]
    public void GetTopStudents_NegativeCount_ReturnsEmptyList()
    {
        var calc = CreateCalculator(80m);

        Assert.Empty(calc.GetTopStudents(-1));
        LogApproved();
    }

    [Fact]
    public void RemoveGrade_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        var calc = CreateCalculator(80m);

        Assert.Throws<ArgumentOutOfRangeException>(() => calc.RemoveGrade("S001", 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => calc.RemoveGrade("S001", -1));
        LogApproved();
    }

    [Fact]
    public void RemoveGrade_ValidIndex_RemovesGrade()
    {
        var calc = CreateCalculator(80m, 90m);

        calc.RemoveGrade("S001", 0);

        Assert.Single(calc.GetStudent("S001")!.Grades);
        Assert.Equal(90m, calc.GetStudent("S001")!.Grades[0].Grade);
        LogApproved();
    }

    [Fact]
    public void ZZZ_ResumenTestsAprobados()
    {
        LogApproved();
        _output.WriteLine("Resumen de tests aprobados:");

        foreach (string testName in ApprovedTestNames)
        {
            _output.WriteLine($"{testName}() aprobado");
        }
    }

    private void LogApproved([CallerMemberName] string testName = "")
    {
        ApprovedTestNames.Add(testName);
    }

    private static GradeCalculator CreateCalculatorWithCredits(params (decimal Grade, decimal Credits)[] grades)
    {
        var calc = new GradeCalculator();
        calc.RegisterStudent(new Student("S001", "Estudiante de prueba", "Sistemas"));

        foreach (var (grade, credits) in grades)
        {
            calc.AddGradeToStudent("S001", new SubjectGrade("Materia", grade, credits));
        }

        return calc;
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

public class GradeCalculatorTestCaseOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        return testCases
            .OrderBy(testCase => testCase.TestMethod.Method.Name == nameof(GradeCalculatorTests.ZZZ_ResumenTestsAprobados) ? 1 : 0)
            .ThenBy(testCase => testCase.TestMethod.Method.Name);
    }
}