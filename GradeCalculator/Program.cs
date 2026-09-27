using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║   SISTEMA DE GESTIÓN ACADÉMICA - VERSIÓN DEFECTUOSA           ║");
        Console.WriteLine("║              Demostración de Errores Sofisticados             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

        var system = new GradeCalculator();

        // Registrar estudiantes
        var student1 = new Student("E001", "Carlos Mendoza", "Ingeniería de Sistemas");
        var student2 = new Student("E002", "María Rodríguez", "Ingeniería de Sistemas");

        system.RegisterStudent(student1);
        system.RegisterStudent(student2);

        // Agregar calificaciones a student1
       system.AddGradeToStudent("E001", new SubjectGrade("Matemáticas", 150m, 4));
        system.AddGradeToStudent("E001", new SubjectGrade("Física", 88m, 3));
        system.AddGradeToStudent("E001", new SubjectGrade("Programación", 92m, 4));

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 1: NullReferenceException - Agregar calificación a estudiante inexistente");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            system.AddGradeToStudent("E999", new SubjectGrade("Inglés", 80m, 2));
            Console.WriteLine("✗ Operación completada (debería fallar)");
        } 
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"✗ EXCEPCIÓN NO CONTROLADA: {ex.GetType().Name}");
            Console.WriteLine($"   Causa: Intento de agregar calificación a estudiante inexistente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 2: Cálculo incorrecto del promedio ponderado");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            decimal weighted = system.CalculateWeightedAverage("E001");
            decimal simple = (95 + 88 + 92) / 3m;
            
            Console.WriteLine($"Calificaciones: Matemáticas(95,4cr), Física(88,3cr), Programación(92,4cr)");
            Console.WriteLine($"Promedio simple esperado: {simple:F2}");
            Console.WriteLine($"Promedio ponderado obtenido: {weighted:F2}");
            Console.WriteLine($"✗ PROBLEMA: No normaliza correctamente (multiplica sin dividir por 100)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 3: Condición invertida en validación de beca");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            bool eligible = system.IsStudentEligibleForScholarship("E001");
            Console.WriteLine($"Promedio del estudiante: ~91.67 (excelente)");
            Console.WriteLine($"¿Elegible para beca?: {eligible}");
            Console.WriteLine($"✗ PROBLEMA: Lógica invertida - estudiante con promedio alto es rechazado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 4: Actualización sin validación de rango");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            system.UpdateGrade("E001", "Matemáticas", 150m);
            var student = system.GetStudent("E001");
            Console.WriteLine($"Intento de actualizar Matemáticas a 150");
            Console.WriteLine($"✗ Valor aceptado sin validación");
            Console.WriteLine($"   Nueva calificación: {student.Grades[0].Grade} (INVÁLIDA - > 100)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 5: Cálculo de PA incorrecto");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            decimal pa = system.CalculatePA("E001");
            decimal expectedPA = ((80 + 85 + 90) / 3m) / 25m;
            
            Console.WriteLine($"Calificaciones: 80, 85, 90");
            Console.WriteLine($"PA esperado (escala 0-4): ~3.67");
            Console.WriteLine($"PA obtenido: {pa:F2}");
            Console.WriteLine($"✗ PROBLEMA: Fórmula de conversión incorrecta y suma sin promediar");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 6: IndexOutOfRangeException - Obtener top estudiantes");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            Console.WriteLine($"Total de estudiantes registrados: 2");
            Console.WriteLine($"Intento de obtener top 5 estudiantes...");
            var top = system.GetTopStudents(5);
            Console.WriteLine($"✗ Operación completada (debería fallar)");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"✗ EXCEPCIÓN NO CONTROLADA: {ex.GetType().Name}");
            Console.WriteLine($"   Causa: Intento de acceder a índice fuera del rango de la colección");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 7: RemoveAt sin validación de índice");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            Console.WriteLine($"Estudiante E001 tiene {system.GetStudent("E001").Grades.Count} calificaciones");
            Console.WriteLine($"Intento de eliminar calificación en índice 10...");
            system.RemoveGrade("E001", 10);
            Console.WriteLine($"✗ Operación completada (debería fallar)");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"✗ EXCEPCIÓN NO CONTROLADA: {ex.GetType().Name}");
            Console.WriteLine($"   Causa: Índice fuera del rango válido");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("ERROR 8: CalculateSimpleAverage retorna suma en lugar de promedio");
        Console.WriteLine("───────────────────────────────────────────────────────────────────────────────");
        try
        {
            decimal simple = system.CalculateSimpleAverage("E001");
            decimal expected = (95 + 88 + 92) / 3m;

            Console.WriteLine($"Calificaciones: 95, 88, 92");
            Console.WriteLine($"Promedio esperado: {expected:F2}");
            Console.WriteLine($"Valor obtenido: {simple:F2}");
            Console.WriteLine($"✗ PROBLEMA: Retorna suma {simple:F2} en lugar de promedio {expected:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        Console.WriteLine();

        // ═══════════════════════════════════════════════════════════════════════════════
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                  RESUMEN DE DEFECTOS ENCONTRADOS              ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.WriteLine("\n✗ Error 1: NullReferenceException (sin validación de existencia)");
        Console.WriteLine("✗ Error 2: Cálculo de promedio ponderado incorrecto");
        Console.WriteLine("✗ Error 3: Lógica invertida en validación de becas");
        Console.WriteLine("✗ Error 4: Sin validación de rango (0-100) en actualización");
        Console.WriteLine("✗ Error 5: Fórmula de PA incorrecta");
        Console.WriteLine("✗ Error 6: IndexOutOfRangeException sin validación");
        Console.WriteLine("✗ Error 7: RemoveAt sin validación de índice");
        Console.WriteLine("✗ Error 8: Promedio simple retorna suma en lugar de promedio");
        Console.WriteLine("\n Estado del sistema: ❌ COMPLETAMENTE INESTABLE\n");
   
    }
}