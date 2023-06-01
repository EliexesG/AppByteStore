//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MetodoPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetodoPago()
        {
            this.FacturaEncabezado = new HashSet<FacturaEncabezado>();
        }
    
        public int IdMetodoPago { get; set; }
        public string Proveedor { get; set; }
        public byte[] NumeroTarjeta { get; set; }
        public Nullable<System.DateTime> FechaExpiracion { get; set; }
        public string Codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; }
        public virtual TipoPago TipoPago { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
