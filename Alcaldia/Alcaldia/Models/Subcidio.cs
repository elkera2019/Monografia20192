
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
    
public partial class Subcidio
{

    public int IdSubcidio { get; set; }

    public string Inss { get; set; }

    public Nullable<int> IdTipoSubcido { get; set; }

    public byte[] ImagenSubcidio { get; set; }

    public Nullable<System.DateTime> FechaIniciosubcidio { get; set; }

    public Nullable<double> Dias { get; set; }

    public Nullable<bool> Estado { get; set; }



    public virtual Empleado Empleado { get; set; }

    public virtual TipoSubcidio TipoSubcidio { get; set; }

}

}
