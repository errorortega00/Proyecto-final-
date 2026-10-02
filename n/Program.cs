using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Muestra el título del programa.
        Console.WriteLine("=============================");
        Console.WriteLine("       PROBLEMA N-QUEENS");
        Console.WriteLine("=============================");
        Console.WriteLine();

        // Ejecuta pruebas con resultados conocidos.
        EjecutarPruebas();

        // Pide el tamaño del tablero.
        Console.Write("Ingresa el tamaño del tablero N (1-16): ");

        // Valida que la entrada sea un número entero.
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Error: debes escribir un número entero.");
            return;
        }

        // Valida que N esté dentro del rango permitido.
        if (n < 1 || n > 16)
        {
            Console.WriteLine("Error: N debe estar entre 1 y 16.");
            return;
        }

        // Crea la máscara de bits del tablero.
        uint mascaraTablero = (1U << n) - 1U;

        // Inicia la medición del tiempo.
        Stopwatch reloj = Stopwatch.StartNew();

        // Inicia la búsqueda sin posiciones ocupadas.
        ulong soluciones = Buscar(
            mascaraTablero,
            0,
            0,
            0
        );

        // Detiene la medición del tiempo.
        reloj.Stop();

        // Muestra los resultados.
        Console.WriteLine();
        Console.WriteLine("RESULTADOS");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Tamaño del tablero: " + n + " x " + n);
        Console.WriteLine("Soluciones encontradas: " + soluciones);
        Console.WriteLine(
            "Tiempo de ejecución: " +
            reloj.Elapsed.TotalMilliseconds +
            " ms"
        );
    }


    // Busca todas las soluciones usando recursividad y backtracking.
    static ulong Buscar(
        uint mascaraTablero,
        uint columnas,
        uint diagonalIzquierda,
        uint diagonalDerecha)
    {
        // Si todas las columnas están ocupadas, hay una solución.
        if (columnas == mascaraTablero)
        {
            return 1;
        }

        // Junta todas las posiciones bloqueadas.
        uint ocupadas =
            columnas |
            diagonalIzquierda |
            diagonalDerecha;

        // Obtiene las posiciones disponibles.
        uint disponibles =
            mascaraTablero & ~ocupadas;

        ulong soluciones = 0;

        // Prueba cada posición disponible.
        while (disponibles != 0)
        {
            // Selecciona una posición libre.
            uint posicion =
                disponibles & (~disponibles + 1);

            // Quita esa posición de las opciones pendientes.
            disponibles =
                disponibles & (disponibles - 1);

            // Avanza a la siguiente fila.
            soluciones += Buscar(
                mascaraTablero,
                columnas | posicion,
                (diagonalIzquierda | posicion) << 1,
                (diagonalDerecha | posicion) >> 1
            );
        }

        // Regresa el total de soluciones encontradas.
        return soluciones;
    }


    // Ejecuta pruebas para comprobar que el algoritmo funciona.
    static void EjecutarPruebas()
    {
        Console.WriteLine("PRUEBAS");
        Console.WriteLine("-----------------------------");

        Probar(1, 1);
        Probar(2, 0);
        Probar(3, 0);
        Probar(4, 2);
        Probar(5, 10);
        Probar(8, 92);

        Console.WriteLine();
    }


    // Compara el resultado obtenido con el esperado.
    static void Probar(int n, ulong esperado)
    {
        uint mascara = (1U << n) - 1U;

        ulong resultado = Buscar(
            mascara,
            0,
            0,
            0
        );

        if (resultado == esperado)
        {
            Console.WriteLine(
                "N = " + n +
                " -> CORRECTO"
            );
        }
        else
        {
            Console.WriteLine(
                "N = " + n +
                " -> ERROR"
            );
        }
    }
}
