using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryProductos
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        Producto GuardarProducto(Producto producto);
        IEnumerable<Producto> GetProductoPorNombre(String nombre);
        IEnumerable<Producto> GetProductoPorVendedor(int idProducto);
        IEnumerable<Producto> GetProductoByCategoria(int idCategoria);
        void DeleteProducto(int id);
        Producto Save(Producto producto);
    }
}
