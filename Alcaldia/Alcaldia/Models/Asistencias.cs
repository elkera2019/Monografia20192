
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
    using System.ComponentModel.DataAnnotations;
    
public partial class Asistencias
{

    public int IdAsistencias { get; set; }

    public string Inss { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> FechaAsistencia { get; set; }

        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public Nullable<System.TimeSpan> HoraEntrada { get; set; }

    public Nullable<bool> FirmaEntrada { get; set; }

    public Nullable<bool> FirmaSalida { get; set; }

    public Nullable<bool> Estado { get; set; }

        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public Nullable<System.TimeSpan> HoraSalida { get; set; }



    public virtual Empleado Empleado { get; set; }

}

}
