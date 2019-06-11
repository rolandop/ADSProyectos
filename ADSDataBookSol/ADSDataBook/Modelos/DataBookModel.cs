using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSDataBook.Modelos
{
    public class ResponseDataBook
    {
        public Registros Registros { get; set; }
    }

    public class Registros
    {
        public Civil Civil { get; set; }
        public Actual Actual { get; set; }
        public PrimeroAnterior PrimeroAnterior { get; set; }
        public SegundoAnterior SegundoAnterior { get; set; }
        public Sri Sri { get; set; }
        public Contactabilidad Contactabilidad { get; set; }
        public Vehicular Vehicular { get; set; }
        public Hijos Hijos { get; set; }
        public Conyugue Conyugue { get; set; }
        public CounyugueCedula CounyugueCedula { get; set; }
        public Padres Padres { get; set; }
        public CedulasPadres CedulasPadres {get; set;}
        public ContactabilidadFamiliares ContactabilidadFamiliares { get; set; }
        public ArbolGenealogico ArbolGenealogico { get; set; }


    }

    public class Civil
    {
        public string Cedula { get; set; }
        public string PrimerNmbre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreConyugue { get; set; }
        public string NombrePadre { get; set; }
        public string NombreMadre { get; set; }
        public string profesion { get; set; }
        public string Nacionalidad { get; set; }
        public string DiaNacimiento { get; set; }
        public string MesNacimiento { get; set; }
        public string AnioNacimiento { get; set; }
        public string DiaDefuncion { get; set; }
        public string MesDefuncion { get; set; }
        public string AnioDefuncion { get; set; }
    }
    public class Actual
    {
        public string NombreEmpleador { get; set; }
        public string RucEmpleador { get; set; }
        public string DireccionEmpleador { get; set; }
        public string ActividadEmpleador { get; set; }
        public string Cargo { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string SalarioPromedio { get; set; }
        public string TelefonoEmpleador { get; set; }
        public string MesesPromedio { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public string DescripcionEmpleador { get; set; }
    }
    public class PrimeroAnterior
    {
        public string NombreEmpleador { get; set; }
        public string RucEmpleador { get; set; }
        public string DireccionEmpleador { get; set; }
        public string ActividadEmpleador { get; set; }
        public string CargoEmpleador { get; set; }
        public string ProvinciaEmpleador { get; set; }
        public string CantonEmpleador { get; set; }
        public string ParroquiaEmpleador { get; set; }
        public string SalarioPromedio { get; set; }
        public string TelefonoEmpleador { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public string DescripcionEmpleador { get; set; }
    }
    public class SegundoAnterior
    {
        public string NombreEmpleador { get; set; }
        public string RucEmpleador { get; set; }
        public string DireccionEmpleador { get; set; }
        public string ActividadEmpleador { get; set; }
        public string Cargo { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string SalarioPromedio { get; set; }
        public string TelefonoEmpleador { get; set; }
        public string MesesPromedio { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public string DescripcionEmpleador { get; set; }
    }
    public class Sri
    {
        public string RucPersonal { get; set; }
        public string RazonSocial { get; set; }
        public string Actividad { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Interseccion { get; set; }
        public string Referencia { get; set; }
        public string Obligado { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string FechaInicio { get; set; }
        public string FechaSuspension { get; set;}
        public string Reinicio { get; set; }
        public string Telefono { get; set; }
    }
    public class Contactabilidad
    {
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Tel4 { get; set; }
        public string Tel5 { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string Direccion { get; set; }

    }
    public class Vehicular
    {
        public string Placa { get; set; }
    }
    public class Hijos
    {
        public string Hijo1 { get; set; }
        public string Hijo2 { get; set; }
        public string Hijo3 { get; set; }
        public string Hijo4 { get; set; }
        public string Hijo5 { get; set; }
        public string Hijo6 { get; set; }
        public string Hijo7 { get; set; }
        public string Hijo8 { get; set; }

    }
    public class Conyugue
    {
        public string ConyugueNombre { get; set; }
    }
    public class CounyugueCedula
    {
        public string ConyugueCedula { get; set; }
    }
    public class Padres
    {
        public string NombrePadre { get; set; }
        public string NombreMadre { get; set; }
    }
    public class CedulasPadres
    {
        public string Tipo1 { get; set; }
        public string CedulaTipo1 { get; set; }
        public string Tipo2 { get; set; }
        public string CedulaTipo2 { get; set; }

    }
    public class ContactabilidadFamiliares
    {
        public List<Pariente> Pariente { get; set; }
    }
    public class ArbolGenealogico
    {
        public List<Arbol> Pariente { get; set; }
    }

    public class Pariente
    {
        public string CedulaPariente { get; set; }
        public string NombrePariente { get; set; }
        public string TipoPariente { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono8 { get; set; }
        public string Telefono2 { get; set; }
        public string Telefono3 { get; set; }
        public string Telefono4 { get; set; }
        public string Telefono5 { get; set; }
        public string Telefono6 { get; set; }
        public string Telefono7 { get; set; }


    }
    public class Arbol
    {
        public string CedulaPariente { get; set; }
        public string NombrePariente { get; set; }
        public string TipoPariente { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaMarimonio { get; set; }
        public string FechaFallecimiento { get; set; }
    }

}
