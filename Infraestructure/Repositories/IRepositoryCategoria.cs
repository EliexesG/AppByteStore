﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryCategoria
    {
        IEnumerable<Categoria> GetCategoria();
        Categoria GetCategoriaByID(int id);
    }
}
