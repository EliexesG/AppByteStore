using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEvaluacion : IServiceEvaluacion
    {
        IRepositoryEvaluacion repository = new RepositoryEvaluacion();

        public Evaluacion GetEvaluacionByID(int idEvaluacion)
        {
            return repository.GetEvaluacionByID(idEvaluacion);
        }

        public IEnumerable<Evaluacion> GetEvaluacionByPedidoForCliente(int idPedido, int idUsuario)
        {
            return repository.GetEvaluacionByPedidoForCliente(idPedido, idUsuario);
        }

        public Evaluacion GetEvaluacionByPedidoForVendedor(int idPedido, int idUsuario)
        {
            return repository.GetEvaluacionByPedidoForVendedor(idPedido, idUsuario);
        }

        public Evaluacion SaveEvaluacion(Evaluacion evaluacion)
        {
            return repository.SaveEvaluacion(evaluacion);
        }

        public IEnumerable<object> CantEvaluacionesPorEscalaVendedor(int idVendedor)
        {
            return repository.CantEvaluacionesPorEscalaVendedor(idVendedor);
        }
    }
}
