using Infraestructure.Models;
using System.Collections.Generic;

namespace Infraestructure.Repositories
{
    public interface IRepositoryDireccion
    {
        int DeleteDireccionByID(int id);
        Direccion GetDireccionByID(int id);
        IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario);
        Direccion SaveDireccion(Direccion pDireccion);
    }
}