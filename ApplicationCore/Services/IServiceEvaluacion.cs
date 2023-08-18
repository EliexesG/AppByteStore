using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEvaluacion
    {
        Evaluacion GetEvaluacionByID(int idEvaluacion);
        IEnumerable<Evaluacion> GetEvaluacionByPedidoForCliente(int idPedido, int idUsuario);
        Evaluacion GetEvaluacionByPedidoForVendedor(int idPedido, int idUsuario);
        Evaluacion SaveEvaluacion(Evaluacion evaluacion);
        IEnumerable<object> CantEvaluacionesPorEscalaVendedor(int idVendedor);
    }
}
