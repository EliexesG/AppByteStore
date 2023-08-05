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
        IEnumerable<TipoPago> GetTipoPago();
        IEnumerable<Rol> GetRol();
        IEnumerable<Usuario> GetUsuarioByEstado(bool estado);
        IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario);
        IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario);
        IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario);
        Usuario Guardar(Usuario usuario);

    }
}
