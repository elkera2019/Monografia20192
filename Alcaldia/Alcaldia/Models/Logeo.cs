
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
    
public partial class Logeo
{

    public string Usuario { get; set; }

    public string Contraseña { get; set; }

    public string Inss { get; set; }

    public string Permisos { get; set; }



    public virtual Empleado Empleado { get; set; }

}

}