using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTelefono : IServiceTelefono
    {

        IRepositoryTelefono repository = new RepositoryTelefono();

        public int DeleteTelefonoByID(int id)
        {
            return repository.DeleteTelefonoByID(id);
        }

        public Telefono GetTelefonoByID(int id)
        {
            return repository.GetTelefonoByID(id);
        }

        public IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario)
        {
            return repository.GetTelefonoByUsuario(idUsuario);
        }

        public Telefono SaveTelefono(Telefono pTelefono)
        {
            return repository.SaveTelefono(pTelefono);
        }
    }
}
