using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public Usuario GetUsuarioByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            throw new NotImplementedException();
        }

        public Usuario Guardar(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
