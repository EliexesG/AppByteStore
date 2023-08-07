using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryMetodoPago
    {
        MetodoPago GetMetodoPagoByID(int id);
        IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario);
        IEnumerable<TipoPago> GetTipoPago();
        MetodoPago SaveMetodoPago(MetodoPago pMetodoPago);
    }
}