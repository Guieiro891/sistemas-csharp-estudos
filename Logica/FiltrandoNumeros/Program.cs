using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

class Program
{
    static void Main()
    {
        // Lista de números para testar 
        List<int> numeros = new List<int> {5, 12, 3, 8, 21, 6, 10, 15, 4, 7 };

        Console.WriteLine("\n=== NÚMEROS ORIGINAIS ===");
        foreach (var n in numeros)
            Console.Write(n + " ");
        Console.WriteLine();

        /////////
        var resultado = FiltrarNumerosParesEOrdenados(numeros);

        Console.WriteLine("\n==== NÚMEROS PARES ORDENADOS ===");
        foreach (var n in resultado)
            Console.Write(n + " ");
        Console.WriteLine();


        var resultados = FiltrandoNumerosImparesEDecrescentes(numeros);
        Console.WriteLine("=== NÚMEROS ÍMPARES ===");
        foreach (var i in resultados)
            Console.Write(i + " ");
        Console.WriteLine();

        Console.WriteLine("\n=== NÚMEROS MAIORES ===");
        var maioresQue10 = FiltrarNumerosMaioresQue(numeros, 10);
        foreach (var n in maioresQue10)
            Console.Write( n + " ");
        Console.WriteLine();

        Console.WriteLine("\n=== NUMEROS MENORES ===");
        var menor = FiltrandoNumerosMenoresQue(numeros, 10);
        foreach(var n in menor)
            Console.Write(n + " ");
        Console.WriteLine();
    }





    static List<int> FiltrarNumerosParesEOrdenados(List<int> lista)
    {
        

        return lista
            .Where(n => n % 2 == 0)
            .OrderBy(n => n)
            .ToList();
    }
    static List<int> FiltrandoNumerosImparesEDecrescentes(List<int> lista)
    {
        return lista
            .Where(n => n % 2 != 0)
            .OrderByDescending(n => n)
            .ToList();
    }
    static List<int> FiltrarNumerosMaioresQue(List<int> lista, int minimo)
    {
        return lista
            .Where(n => n > minimo)
            .ToList();
    }
    static List<int> FiltrandoNumerosMenoresQue(List<int> lista, int minimo)
    {
        return lista
        .Where(n => n < minimo)
        .ToList();
    }
}