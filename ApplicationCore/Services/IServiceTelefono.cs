using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceTelefono
    {
        Telefono GetTelefonoByID(int id);
        IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario);
        Telefono SaveTelefono(Telefono pTelefono);
    }
}
