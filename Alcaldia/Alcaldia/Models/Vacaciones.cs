//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alcaldia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacaciones
    {
        public int IdVacaciones { get; set; }
        public string UnidadAdministrativa { get; set; }
        public string CodigoFuncional { get; set; }
        public string Dirigido { get; set; }
        public string Inss { get; set; }
        public Nullable<double> CantidadDias { get; set; }
        public Nullable<System.DateTime> FechaInicial { get; set; }
        public Nullable<System.DateTime> FechaRetorna { get; set; }
    
        public virtual Empleado Empleado { get; set; }
    }
}
