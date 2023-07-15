﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuarioByID(int id);
        IEnumerable<Usuario> GetUsuario();
        IEnumerable<Usuario> GetUsuarioByRol(int IdRol);
        Usuario Login(string Correo, string contrasenna);
        Usuario Guardar(Usuario usuario);

    }
}
