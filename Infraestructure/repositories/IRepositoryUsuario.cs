using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(int id);
        Usuario Guardar(Usuario usuario);

    }
}
