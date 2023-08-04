using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;

namespace Web.Utils
{
    public class Carrito
    {
        public List<CompraDetalle> Items { get; private set; }

        //Implementación Singleton
        public static readonly Carrito Instancia;

        static Carrito()
        {
            if (HttpContext.Current.Session["Carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<CompraDetalle>();
                HttpContext.Current.Session["Carrito"] = Instancia;
            }
            else
            {
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        public String AgregarItem(int IdProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = _ServiceProducto.GetProductoByID(IdProducto);

            String mensaje = "";

            CompraDetalle nuevoItem = new CompraDetalle()
            {
                IdProducto = IdProducto,
                Producto = producto,
            };

            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdProducto == IdProducto))
                {
                    CompraDetalle item = Items.Find(x => x.IdProducto == IdProducto);
                    item.Cantidad++;
                    item.Precio = item.Cantidad * producto.Precio;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    nuevoItem.Precio = producto.Precio;
                    nuevoItem.EstadoEntrega = false;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Orden Productos", "Producto agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Orden Productos", "El Producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }

        public String SetItemCantidad(int IdProducto, int Cantidad)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = _ServiceProducto.GetProductoByID(IdProducto);
            String mensaje = "";

            if (Cantidad == 0)
            {
                EliminarItem(IdProducto);
                mensaje = SweetAlertHelper.Mensaje("Orden Productos", "Producto eliminado", SweetAlertMessageType.success);

            }
            else if (Items.Exists(x => x.IdProducto == IdProducto))
            {
                CompraDetalle item = Items.Find(x => x.IdProducto == IdProducto);
                item.Cantidad = Cantidad;
                item.Precio = item.Cantidad * producto.Precio;
                mensaje = SweetAlertHelper.Mensaje("Orden Producto", "Cantidad actualizada", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        public String EliminarItem(int IdProducto)
        {
            String mensaje = "El producto no existe";
            if (Items.Exists(x => x.IdProducto == IdProducto))
            {
                var itemEliminar = Items.Single(x => x.IdProducto == IdProducto);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Orden Libro", "Libro eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        public double GetTotal()
        {
            double total = 0;
            total = Items.Sum(x => (double)x.Precio);

            return total;
        }

        public double GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => (int)x.Cantidad);

            return total;
        }

        public void EliminarCarrito()
        {
            Items.Clear();

        }
    }
}