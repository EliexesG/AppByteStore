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
        Rol GetRolByID(int id);
        Usuario ActualizarEstado(int idUsuario, bool estadoNuevo);
        Usuario Guardar(Usuario usuario, string[] selectedRol);
        IEnumerable<Usuario> GetVendedoresMejorEvaluados();
        IEnumerable<Usuario> GetVendedoresPeorEvaluados();
    }
}
