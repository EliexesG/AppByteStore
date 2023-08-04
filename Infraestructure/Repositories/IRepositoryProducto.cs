using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        IEnumerable<Producto> GetProductoPorNombre(String nombre);
        IEnumerable<Producto> GetProductoPorVendedor(int idVendedor);
        IEnumerable<Producto> GetProductoByCategoria(int idCategoria);
        void DeleteProducto(int id);
        IEnumerable<FotoProducto> GetFotosPorProducto(int idProducto);
        Producto Save(Producto producto);
        Producto ActualizarStock(int idProducto, int cantRebajada);
    }
}
