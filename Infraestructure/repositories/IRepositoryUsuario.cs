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
        Usuario GetUsuarioByID(int id);
        IEnumerable<Usuario> GetUsuario();
        IEnumerable<Usuario> GetUsuarioByRol(int IdRol);
        Usuario Login(string Correo, string contrasenna);
        IEnumerable<Rol> GetRol();
        IEnumerable<Usuario> GetUsuarioByEstado(bool estado);
        IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario);
        IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario);
        Rol GetRolByID(int id);
        Usuario Guardar(Usuario usuario, string[] selectedRol);

    }
}
