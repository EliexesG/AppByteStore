using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryTelefono
    {
        Telefono GetTelefonoByID(int id);
        IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario);
        Telefono SaveTelefono(Telefono pTelefono);
        int DeleteTelefonoByID(int id);


    }
}