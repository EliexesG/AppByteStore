using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryEvaluacion
    {
        Evaluacion GetEvaluacionByID(int idEvaluacion);
        IEnumerable<Evaluacion> GetEvaluacionByPedidoForCliente(int idPedido, int idUsuario);
        Evaluacion GetEvaluacionByPedidoForVendedor(int idPedido, int idUsuario);
        Evaluacion SaveEvaluacion(Evaluacion evaluacion);
    }
}