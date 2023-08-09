using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMetodoPago
    {
        MetodoPago GetMetodoPagoByID(int id);
        IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario);
        IEnumerable<TipoPago> GetTipoPago();
        MetodoPago SaveMetodoPago(MetodoPago pMetodoPago);
        string mostradorNumeroTarjeta(byte[] numeroTarjeta);
        string mostradorCodigoTarjeta(string codigo);
    }
}
