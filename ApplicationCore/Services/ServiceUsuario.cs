using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private IRepositoryUsuario repository = new RepositoryUsuario();

        public IEnumerable<TipoPago> GetTipoPago()
        {
            return repository.GetTipoPago();
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            return repository.GetUsuario();
        }

        public Usuario GetUsuarioByID(int id)
        {
            return repository.GetUsuarioByID(id); ;
        }

        public IEnumerable<Usuario> GetUsuarioByRol(int IdRol)
        {
            return repository.GetUsuarioByRol(IdRol);
        }

        public Usuario Guardar(Usuario usuario)
        {
            return repository.Guardar(usuario);
        }

        public IEnumerable<Rol> GetRol()
        {
            return repository.GetRol();
        }

        public IEnumerable<Usuario> GetUsuarioByEstado(bool estado)
        {
            return repository.GetUsuarioByEstado(estado);
        }

        public IEnumerable<MetodoPago> GetMetodoPagoByUsuario(int idUsuario)
        {
            return repository.GetMetodoPagoByUsuario(idUsuario);   
        }

        public IEnumerable<Direccion> GetDireccionByUsuario(int idUsuario)
        {
            return repository.GetDireccionByUsuario(idUsuario);
        }

        public IEnumerable<Telefono> GetTelefonoByUsuario(int idUsuario)
        {
            return repository.GetTelefonoByUsuario(idUsuario);
        }

        public Usuario Login(string Correo, string contrasenna)
        {
            return repository.Login(Correo, contrasenna);
        }
    }
}
