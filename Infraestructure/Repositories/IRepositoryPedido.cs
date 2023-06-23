﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryPedido
    {
        IEnumerable<Pedido> GetPedido();
        IEnumerable<Pedido> GetPedidoByCliente(int idCliente);
        IEnumerable<Pedido> GetPedidoByVendedor(int idVendedor);
        Pedido GetPedidoByID(int id);
        void DeletePedido(int id);
        Pedido Save(Pedido pedido);
    }
}
