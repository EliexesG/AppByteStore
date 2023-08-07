using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryDireccion
    {
        Direccion GetDireccionByID(int id);
        IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario);
        Direccion SaveDireccion(Direccion pDireccion);
    }
}