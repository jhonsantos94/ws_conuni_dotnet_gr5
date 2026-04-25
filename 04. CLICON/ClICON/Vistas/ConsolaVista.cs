using System;
using System.Collections.Generic;
using System.Text;

namespace ClICON.Vistas
{
    internal class ConsolaVista
    {
        public void MostrarMensaje(string mensaje) => Console.WriteLine(mensaje);

        public void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {mensaje}");
            Console.ResetColor();
        }

        public void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[ÉXITO] {mensaje}");
            Console.ResetColor();
        }

        public string PedirDato(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine()?.Trim();
        }

        public double PedirNumero(string prompt)
        {
            double numero;
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (double.TryParse(Console.ReadLine(), out numero))
                {
                    return numero;
                }
                MostrarError("Por favor, ingresa un número válido.");
            }
        }

        public string MostrarMenuPrincipal()
        {
            Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Convertir Longitud");
            Console.WriteLine("2. Convertir Masa");
            Console.WriteLine("3. Convertir Temperatura");
            Console.WriteLine("4. Salir");
            return PedirDato("Seleccione una opción");
        }
    }
}
