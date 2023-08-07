using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDireccion : IServiceDireccion
    {
        IRepositoryDireccion repository = new RepositoryDireccion();

        public Direccion GetDireccionByID(int id)
        {
            return repository.GetDireccionByID(id);
        }

        public IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario)
        {
            return repository.GetDireccionByUsuario(idUsuario);
        }

        public Direccion SaveDireccion(Direccion pDireccion)
        {
            return repository.SaveDireccion(pDireccion);
        }
    }
}
