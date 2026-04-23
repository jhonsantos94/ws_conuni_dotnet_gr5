using System.ServiceModel;
using ec.edu.monster.Mapeadores;
using ec.edu.monster.Seguridad;
using ec.edu.monster.Servicios;
using ec.edu.monster.Utilidades;
using ec.edu.monster.Utilidades.Enums;

namespace ec.edu.monster
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WSConversorUnidades : IWSConversorUnidades
    {
        private readonly ServicioConversor<UnidadLongitud> servicioLongitud =
            new ServicioConversor<UnidadLongitud>(new ConversorLongitud());

        private readonly ServicioConversor<UnidadMasa> servicioMasa =
            new ServicioConversor<UnidadMasa>(new ConversorMasa());

        private readonly ServicioConversor<UnidadTemperatura> servicioTemperatura =
            new ServicioConversor<UnidadTemperatura>(new ConversorTemperatura());

        public string Login(string usuario, string contrasenia)
        {
            if (usuario == "Monster" && contrasenia == "Monster9")
            {
                return AdministradorTokens.GenerarToken();
            }

            throw new FaultException("Credenciales incorrectas");
        }

        public double ConvertirLongitud(double valor, string unidadInicial, string unidadFinal)
        {
            var origen = UnidadMapper.ToLongitud(unidadInicial);
            var destino = UnidadMapper.ToLongitud(unidadFinal);
            return servicioLongitud.Convertir(valor, origen, destino);
        }

        public double ConvertirMasa(double valor, string unidadInicial, string unidadFinal)
        {
            var origen = UnidadMapper.ToMasa(unidadInicial);
            var destino = UnidadMapper.ToMasa(unidadFinal);
            return servicioMasa.Convertir(valor, origen, destino);
        }

        public double ConvertirTemperatura(double valor, string unidadInicial, string unidadFinal)
        {
            var origen = UnidadMapper.ToTemperatura(unidadInicial);
            var destino = UnidadMapper.ToTemperatura(unidadFinal);
            return servicioTemperatura.Convertir(valor, origen, destino);
        }
    }
}

