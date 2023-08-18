using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDireccion
    {
        int DeleteDireccionByID(int id);
        Direccion GetDireccionByID(int id);
        IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario);
        Direccion SaveDireccion(Direccion pDireccion);
    }
}
