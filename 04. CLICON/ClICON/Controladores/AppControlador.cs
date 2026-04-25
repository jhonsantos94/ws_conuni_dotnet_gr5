using System;
using System.ServiceModel;
using System.Threading.Tasks;
using ClICON.Modelos;
using ClICON.Vistas;

namespace ClICON.Controladores
{
    internal class AppControlador
    {
        private readonly ConversorModelo _modelo;
        private readonly ConsolaVista _vista;

        public AppControlador(ConversorModelo modelo, ConsolaVista vista)
        {
            _modelo = modelo;
            _vista = vista;
        }

        public async Task IniciarAppAsync()
        {
            _vista.MostrarMensaje("=== CLIENTE CONVERSOR MONSTER (WCF) ===");

            bool autenticado = false;
            while (!autenticado)
            {
                string usuario = _vista.PedirDato("Usuario");
                string contrasenia = _vista.PedirDato("Contraseña");

                autenticado = await _modelo.IniciarSesionAsync(usuario, contrasenia);

                if (autenticado)
                    _vista.MostrarExito("Login exitoso. Token obtenido.");
                else
                    _vista.MostrarError("Credenciales incorrectas o servidor no disponible.");
            }

            bool salir = false;
            while (!salir)
            {
                string opcion = _vista.MostrarMenuPrincipal();

                switch (opcion)
                {
                    case "1": await EjecutarConversionAsync("Longitud"); break;
                    case "2": await EjecutarConversionAsync("Masa"); break;
                    case "3": await EjecutarConversionAsync("Temperatura"); break;
                    case "4":
                        salir = true;
                        _vista.MostrarMensaje("¡Hasta luego!");
                        break;
                    default: _vista.MostrarError("Opción no válida."); break;
                }
            }
        }

        private async Task EjecutarConversionAsync(string tipo)
        {
            _vista.MostrarMensaje($"\n--- Conversión de {tipo} ---");
            double valor = _vista.PedirNumero("Ingrese el valor a convertir");
            string origen = _vista.PedirDato("Unidad de origen (ej. METRO, MILIMETRO)").ToUpper();
            string destino = _vista.PedirDato("Unidad de destino").ToUpper();

            try
            {
                double resultado = 0;

                if (tipo == "Longitud")
                    resultado = await _modelo.ConvertirLongitudAsync(valor, origen, destino);
                else if (tipo == "Masa")
                    resultado = await _modelo.ConvertirMasaAsync(valor, origen, destino);
                else if (tipo == "Temperatura")
                    resultado = await _modelo.ConvertirTemperaturaAsync(valor, origen, destino);

                _vista.MostrarExito($"Resultado: {valor} {origen} = {resultado} {destino}");
            }
            catch (FaultException ex)
            {
                // Captura excepciones de negocio o validación del token devueltas por WCF
                _vista.MostrarError($"Error del servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                _vista.MostrarError($"Error inesperado: {ex.Message}");
            }
        }
    }
}
