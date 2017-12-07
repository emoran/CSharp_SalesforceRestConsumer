using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceRESTServerConsumer
{
    public class DatosOrigen
    {
        public string origen { get; set; }
        public string areaDepartamento { get; set; }
        public string contacto { get; set; }
        public string telefonoExtencion { get; set; }
        public string contacto2 { get; set; }
        public string telefonoExtencion2 { get; set; }
        public string calle { get; set; }
        public string numeroExterior { get; set; }
        public string numeroInterior { get; set; }
        public string colonia { get; set; }
        public string codigoPostal { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string horarioAtencion { get; set; }
    }

    public class DatosDestino
    {
        public string dependencia { get; set; }
        public string areaDepartamento { get; set; }
        public string contacto { get; set; }
        public string telefonoExtencion { get; set; }
        public string contacto2 { get; set; }
        public string telefonoExtencion2 { get; set; }
        public string calle { get; set; }
        public string numeroExterior { get; set; }
        public string numeroInterior { get; set; }
        public string colonia { get; set; }
        public string codigoPostal { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string horarioAtencion { get; set; }
    }

    public class LstPerfile
    {
        public int numPerfil { get; set; }
        public string clabePerfil { get; set; }
        public int cantClabePerfil { get; set; }
    }

    public class LstEquipos
    {
        public int numEquip { get; set; }
        public string Nparte { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public string condicionEquipo { get; set; }
        public string clonado { get; set; }
    }

    public class Solicitud
    {
        public string nombre { get; set; }
        public string numeroTiket { get; set; }
        public string fechaServicio { get; set; }
        public string tiempoResp { get; set; }
        public DatosOrigen datosOrigen { get; set; }
        public DatosDestino datosDestino { get; set; }
        public List<LstPerfile> lstPerfiles { get; set; }
        public List<LstEquipos> lstEquipos { get; set; }
        public string condicionesEspeciales { get; set; }
        public string tipoServicio { get; set; }
    }
}
