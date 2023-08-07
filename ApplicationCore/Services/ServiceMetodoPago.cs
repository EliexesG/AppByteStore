using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMetodoPago : IServiceMetodoPago
    {

        IRepositoryMetodoPago repository = new RepositoryMetodoPago();

        public MetodoPago GetMetodoPagoByID(int id)
        {
            return repository.GetMetodoPagoByID(id);
        }

        public IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario)
        {
            return repository.GetMetodoPagoByUsuario(idUsuario);
        }

        public IEnumerable<TipoPago> GetTipoPago()
        {
            return repository.GetTipoPago();
        }

        public MetodoPago SaveMetodoPago(MetodoPago pMetodoPago)
        {
            return repository.SaveMetodoPago(pMetodoPago);
        }

        public string mostradorNumeroTarjeta(byte[] numeroTarjeta)
        {
            return repository.mostradorNumeroTarjeta(numeroTarjeta);
        }
    }
}
