using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        Producto GuardarProducto(Producto producto);
        IEnumerable<Producto> GetProductoPorNombre(String nombre, int idVendedor = -1);
        IEnumerable<string> GetProductoNombres(int idVendedor = -1);
        IEnumerable<Producto> GetProductoPorVendedor(int idVendedor);
        IEnumerable<Producto> GetProductoByCategoria(int idCategoria);
        void DeleteProducto(int id);
        IEnumerable<FotoProducto> GetFotosPorProducto(int idProducto);
        Producto Save(Producto producto);
    }
}
