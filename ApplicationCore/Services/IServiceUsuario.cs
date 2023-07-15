using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        IEnumerable<Usuario> GetUsuarios(string Correo, string contrasenna);
        Usuario GetUsuarioByID(int id);
        Usuario Guardar(Usuario usuario);

    }
}
