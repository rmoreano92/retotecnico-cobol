using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Transaccion
{
    public string ID { get; set; }
    public string Tipo { get; set; }
    public decimal Monto { get; set; }
}

class Program
{
    static void Main(string[] args)
    {

        // Solicitar la ruta del archivo CSV al usuario
        Console.WriteLine("Por favor, ingrese la ruta del archivo CSV:");
        string archivoCsvTransacciones = Console.ReadLine();  // Leer la ruta desde la consola

        // Verificar si la ruta es válida
        if (string.IsNullOrEmpty(archivoCsvTransacciones))
        {
            Console.WriteLine("La ruta no puede estar vacía.");
            return;
        }

        // Verificar si el archivo existe
        if (!File.Exists(archivoCsvTransacciones))
        {
            Console.WriteLine("El archivo especificado no existe. Por favor, verifica la ruta.");
            return;
        }


        try
        {
            var transacciones = LeerTransacciones(archivoCsvTransacciones); // leemos el archivo csv
            if (transacciones.Any()) // verifico si hay transaccion 
            {
                GenerarReporte(transacciones); // procedo a generar el reporte
            }
            else
            {
                Console.WriteLine("No se encontraron transacciones en el archivo.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
        }
    }

    static List<Transaccion> LeerTransacciones(string archivoCsvTransacciones)
    {
        var transacciones = new List<Transaccion>();

        // Leer el archivo CSV línea por línea
        var lineas = File.ReadAllLines(archivoCsvTransacciones);

        // Si hay líneas en el archivo, procesarlas (ignorando la primera línea de cabecera)
        foreach (var linea in lineas.Skip(1))
        {
            var columnas = linea.Split(';');

            if (columnas.Length == 3)
            {
                if (decimal.TryParse(columnas[2].Trim(), out decimal monto))
                {
                    transacciones.Add(new Transaccion
                    {
                        ID = columnas[0].Trim(),
                        Tipo = columnas[1].Trim(),
                        Monto = monto
                    });
                }
            }
        }

        return transacciones;
    }

    static void GenerarReporte(List<Transaccion> transacciones)
    {
        // Calcular Balance Final
        decimal balanceFinal = CalcularBalanceFinal(transacciones);

        // Encontrar la transacción de mayor monto
        var transaccionMayor = ObtenerTransaccionMayorMonto(transacciones);

        // Contar las transacciones de cada tipo
        var conteoCreditos = transacciones.Count(t => t.Tipo == "Crédito");
        var conteoDebitos = transacciones.Count(t => t.Tipo == "Débito");

        // Imprimir el reporte
        Console.WriteLine("Reporte de Transacciones Bancarias");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"Balance Final: {balanceFinal:F2}");
        if (transaccionMayor != null)
        {
            Console.WriteLine($"Transacción de Mayor Monto: ID {transaccionMayor.ID} con monto {transaccionMayor.Monto:F2}");
        }
        Console.WriteLine("Conteo de Transacciones:");
        Console.WriteLine($"  Créditos: {conteoCreditos}");
        Console.WriteLine($"  Débitos: {conteoDebitos}");
    }

    static decimal CalcularBalanceFinal(List<Transaccion> transacciones)
    {
        var creditos = transacciones.Where(t => t.Tipo == "Crédito").Sum(t => t.Monto); // suma las transacciones de tipo Crédito
        var debitos = transacciones.Where(t => t.Tipo == "Débito").Sum(t => t.Monto); // suma las transacciones de Tipo Débito
        return creditos - debitos; // resta para sacar el balance 
    }

    static Transaccion ObtenerTransaccionMayorMonto(List<Transaccion> transacciones)
    {
        return transacciones.OrderByDescending(t => t.Monto).FirstOrDefault(); // obtiene el primer monto ordenado descendentemente para obtener el mayor
    }
}
