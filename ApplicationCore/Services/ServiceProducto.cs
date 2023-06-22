﻿using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        private IRepositoryProducto repository = new RepositoryProducto();

        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProducto()
        {
            return repository.GetProducto();
        }

        public IEnumerable<Producto> GetProductoByCategoria(int idCategoria)
        {
            return repository.GetProductoByCategoria(idCategoria);
        }

        public Producto GetProductoByID(int id)
        {
            return repository.GetProductoByID(id);
        }

        public IEnumerable<string> GetProductoNombres(int idVendedor = -1)
        {
            IEnumerable<string> lista = null;

            if (idVendedor != -1)
            {
               lista = repository.GetProducto().Where(producto => producto.Usuario.IdUsuario == idVendedor).Select(producto => producto.Nombre);
            }
            else
            {
                lista = repository.GetProducto().Select(producto => producto.Nombre);
            }

            return lista;
        }
         

        public IEnumerable<Producto> GetProductoPorNombre(string nombre, int idVendedor = -1)
        {

            return repository.GetProductoPorNombre(nombre).Where(producto => idVendedor != -1 ? producto.Usuario.IdUsuario == idVendedor : true);
        }

        public IEnumerable<Producto> GetProductoPorVendedor(int idVendedor)
        {
           return repository.GetProductoPorVendedor(idVendedor);
        }

        public Producto GuardarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Producto Save(Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
