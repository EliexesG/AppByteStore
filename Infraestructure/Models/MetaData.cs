﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{

    internal partial class LoginMetadata
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} inserte un correo válido")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Contraseña")]
        public string Contrasenna { get; set; }
    }

    internal partial class CategoriaMetadata
    {
        public int IdCategoria { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Productos")]
        public virtual ICollection<Producto> Producto { get; set; }
    }

    internal partial class CompraDetalleMetadata
    {
        public int IdCompraEncabezado { get; set; }
        public int IdProducto { get; set; }
        public Nullable<int> Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<double> Precio { get; set; }

        [Display(Name = "Estado de Entrega")]
        public Nullable<bool> EstadoEntrega { get; set; }

        [Display(Name = "Encabezado de Compra")]
        public virtual CompraEncabezado CompraEncabezado { get; set; }
        public virtual Producto Producto { get; set; }
    }

    internal partial class CompraEncabezadoMetadata
    {
        [DisplayFormat(DataFormatString = "{0:000000}")]
        [Display(Name = "Número de Factura")]
        public int IdCompraEncabezado { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd d 'de' MMMM yyyy, hh:mm tt}")]
        [Display(Name = "Fecha y Hora de Compra")]
        public Nullable<System.DateTime> FechaHora { get; set; }

        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name = "Sub-total")]
        public Nullable<double> SubTotal { get; set; } 

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<double> Impuesto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<double> Total { get; set; }

        [Required(ErrorMessage = "{0} son datos requeridos")]
        [Display(Name = "Detalles de Compra")]
        public virtual ICollection<CompraDetalle> CompraDetalle { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Dirección")]
        public virtual Direccion Direccion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Método de Pago")]
        public virtual MetodoPago MetodoPago { get; set; }

        [Display(Name = "Cliente")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Evaluación")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }
        public virtual Pedido Pedido { get; set; }
    }

    internal partial class DireccionMetadata
    {
        public int IdDireccion { get; set; }
        public Nullable<int> Provincia { get; set; }

        [Display(Name = "Cantón")]
        public Nullable<int> Canton { get; set; }
        public Nullable<int> Distrito { get; set; }

        [Display(Name = "Señas")]
        public string Sennas { get; set; }

        [DisplayFormat(DataFormatString = "{0:####-####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        [Display(Name = "Encabezado de Compra")]
        public virtual ICollection<CompraEncabezado> CompraEncabezado { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class EvaluacionMetadata
    {
        public int IdEvaluacion { get; set; }
        public Nullable<int> Escala { get; set; }
        public string Comentario { get; set; }

        [Display(Name = "Encabezado de Compra")]
        public virtual CompraEncabezado CompraEncabezado { get; set; }

        [Display(Name = "Evaluador")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Evaluado")]
        public virtual Usuario Usuario1 { get; set; }
    }

    internal partial class MetodoPagoMetadata
    {
        public int IdMetodoPago { get; set; }
        public string Proveedor { get; set; }

        [Display(Name = "Número de Tarjeta")]
        public byte[] NumeroTarjeta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yy}")]
        [Display(Name = "Fecha de Expiración")]
        public Nullable<System.DateTime> FechaExpiracion { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Encabezado de Compra")]
        public virtual ICollection<CompraEncabezado> CompraEncabezado { get; set; }

        [Display(Name = "Tipo de Pago")]
        public virtual TipoPago TipoPago { get; set; }

        [Display(Name = "Cliente")]
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class PedidoMetadata
    {
        public int IdCompraEncabezado { get; set; }

        [DisplayFormat(DataFormatString = "{0}")]
        [Display(Name = "Estado de Entrega")]
        public Nullable<int> EstadoEntrega { get; set; }

        [Display(Name = "Encabezado de Compra")]
        public virtual CompraEncabezado CompraEncabezado { get; set; }
    }

    internal partial class PreguntaMetadata
    {
        public int IdPregunta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd d 'de' MMMM yyyy, hh:mm tt}")]
        [Display(Name = "Fecha y Hora de Pregunta")]
        public Nullable<System.DateTime> FechaHora { get; set; }

        [Display(Name = "Pregunta")]
        public string Pregunta1 { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }

    internal partial class ProductoMetadata
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Nullable<double> Precio { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<int> Stock { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Estado")]
        public Nullable<int> Estado { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Categoría")]
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Detalles de Compra")]
        public virtual ICollection<CompraDetalle> CompraDetalle { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Fotos del Producto")]
        public virtual ICollection<FotoProducto> FotoProducto { get; set; }

        [Display(Name = "Preguntas")]
        public virtual ICollection<Pregunta> Pregunta { get; set; }

        [Display(Name = "Proveedor")]
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class RespuestaMetadata
    {
        public int IdRespuesta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd d 'de' MMMM yyyy, hh:mm tt}")]
        [Display(Name = "Fecha y Hora de Respuesta")]
        public Nullable<System.DateTime> FechaHora { get; set; }

        [Display(Name = "Respuesta")]
        public string Respuesta1 { get; set; }

        public virtual Pregunta Pregunta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    internal partial class RolMetadata
    {
        public int IdRol { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    } 

    internal partial class TelefonoMetadata
    {
        public int IdTelefono { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd-dddd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Tipo de Teléfono")]
        public Nullable<bool> Tipo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    internal partial class TipoPagoMetadata
    {
        public int IdTipoPago { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Método de Pago")]
        public virtual ICollection<MetodoPago> MetodoPago { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Nombre del Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression("^\\d{9,10}$", ErrorMessage = "Debe ingresar un mínimo de 8 y un máximo de 10 números")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} inserte un correo válido")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Contraseña")]
        public byte[] Contrasenna { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Calificación del Proveedor")]
        public Nullable<int> PromedioEvaluaciones { get; set; }
        public Nullable<bool> Estado { get; set; }

        [Display(Name = "Compras")]
        public virtual ICollection<CompraEncabezado> CompraEncabezado { get; set; }

        [Display(Name = "Direcciones")]
        public virtual ICollection<Direccion> Direccion { get; set; }

        [Display(Name = "Evaluaciones Hechas")]
        public virtual ICollection<Evaluacion> Evaluacion { get; set; }

        [Display(Name = "Evaluaciones Recibidas")]
        public virtual ICollection<Evaluacion> Evaluacion1 { get; set; }

        [Display(Name = "Métodos de Pago")]
        public virtual ICollection<MetodoPago> MetodoPago { get; set; }

        [Display(Name = "Preguntas Realizadas")]
        public virtual ICollection<Pregunta> Pregunta { get; set; }

        [Display(Name = "Productos")]
        public virtual ICollection<Producto> Producto { get; set; }

        [Display(Name = "Respuestas Realizadas")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        [Display(Name = "Teléfonos")]
        public virtual ICollection<Telefono> Telefono { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Roles")]
        public virtual ICollection<Rol> Rol { get; set; }
    }
}
